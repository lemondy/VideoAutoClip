using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VideoAutoClip.Helpers
{
    public static class FFmpegHelper
    {
/*        static string ffmpegPath = AppDomain.CurrentDomain.BaseDirectory + "ffmpeg\\ffmpeg.exe"; // 替换为你的FFmpeg可执行文件的路径
*/
        private static readonly string ffmpegPath = GetPathFromEnv("PATH", "ffmpeg.exe");

        static string? GetPathFromEnv(string varName, string filename)
        {
            string[] paths = Environment.GetEnvironmentVariable(varName).Split(';');
            foreach (string path in paths)
            {
                if (File.Exists(Path.Combine(path, filename)))
                    return Path.Combine(path, filename);
            }
            return null;
        }

        public static string VideoCutDuration(string inputFilePath, string outputFilePath)
        {
            /**
             * 生成ffmpeg处理的命令，在当前目录下建立tmp目录
             * 
             */
            TimeSpan duration = GetVideoDuration(ffmpegPath, inputFilePath);
            Console.WriteLine("视频时长: " + duration);
            // 从第一秒开始
            string start_time = "00:00:01";
            if (duration.TotalSeconds < 1)
            {
                Log4Net.WriteLog("videoCutDuration", string.Format("Input video {0} duration too short!!!!", inputFilePath));
                return "";
            }
            // 去掉视频最后一秒
            string end_time = duration.Subtract(TimeSpan.FromSeconds(1)).ToString();
            string ffmpegArgs = $"-ss {start_time} -t {end_time} -i \"{inputFilePath}\" -y \"{outputFilePath}\"";
            // runFFmpeg(ffmpegArgs);
            return ffmpegArgs;
        }
            
        public static string UpdateVideoScaleAndPlayRate(string inputFilePath, string outputFilePath, double scale, double playRate)
        {
            string ffmpegArgs = $" -i \"{inputFilePath}\" -vf \"setpts=PTS/{playRate},scale=iw*{scale}:ih*{scale},crop=trunc(iw/2)*2:trunc(ih/2)*2\" -af \"atempo={playRate}\" -y \"{outputFilePath}\"";
            return ffmpegArgs;
        }

        public static string ConcatMultiVideo(List<string> videoPaths, string outputFile)
        {
            /**
             *  
             *  合并在一起，使用叠化转场；
             */
            string ffmpeg_cmd = "";
            string filter_param = "";
            string diehua = "";
            for (int i = 0; i < videoPaths.Count; i++)
            {
                ffmpeg_cmd += " -i \"" + videoPaths[i] +"\"";
                TimeSpan duration = GetVideoDuration(ffmpegPath, videoPaths[i]);
                double totalSeconds = duration.TotalSeconds;
                filter_param += $"[{i}:v]fade=t=out:st={totalSeconds}:d=1[v{i}];";
            }
            for (int i = 1; i < videoPaths.Count; i++)
            {
                if (i == 1)
                {
                    diehua += $"[v{i - 1}][v{i}]overlay=shortest=1[out{i}];";
                }
                else
                {
                    diehua += $"[out{i - 1}][v{i}]overlay=shortest=1[out{i}];";
                }
                   
            }
            ffmpeg_cmd += $" -filter_complex \"{filter_param}{diehua}\"";

            ffmpeg_cmd += $" -c:v libx264 -c:a libmp3lame -y \"{outputFile}\"";
           
            /*runFFmpeg(ffmpeg_cmd);*/
            return ffmpeg_cmd;
        }

        public static string VideoAddText(string videoPath, string videoText, string outputFile)
        {
            /*** 给视频底部居中增加文字
             * drawtext: 使用drawtext过滤器添加文字
             * text: 指定添加的文字内容
             * x: 指定x坐标,这里计算视频宽度w减去文字长度text_w再除以2,实现水平居中
             * y: 指定y坐标,这里计算视频高度h减去字体高度th再减5像素,实现水平距离底部5像素
             * fontcolor: 指定字体颜色为白色,透明度0.2实现20%透明
             * fontsize: 指定字体大小为30像素
            */
            string ffmpeg_cmd = $" -i {videoPath} -vf \"drawtext=text='{videoText}':x=(w-text_w)/2:y=h-th-5:fontcolor=white@0.2:fontsize=15\"";
            ffmpeg_cmd += " -y " + outputFile;
            /*runFFmpeg(ffmpeg_cmd);*/
            return ffmpeg_cmd;
        }

     
        static TimeSpan GetVideoDuration(string ffmpegPath, string videoPath)
        {
            Process process = new Process();
            process.StartInfo.FileName = ffmpegPath;
            process.StartInfo.Arguments = "-i " + videoPath;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;

            process.Start();
            string output = process.StandardError.ReadToEnd();
            process.WaitForExit();

            Regex regex = new Regex(@"Duration: (\d+):(\d+):(\d+)");
            Match match = regex.Match(output);
            if (match.Success)
            {
                Log4Net.WriteLog("GetVideoDuration", videoPath+ " GetVideoDuration: " + match.Groups[1].Value + ":" + match.Groups[2].Value + ":" + match.Groups[3].Value);
                string hours = match.Groups[1].Value;
                string minutes = match.Groups[2].Value;
                string seconds = match.Groups[3].Value;

                TimeSpan duration = new TimeSpan(int.Parse(hours), int.Parse(minutes), int.Parse(seconds));
                
                return duration;
            }

            return new TimeSpan();
        }

        public static void RunFFmpeg(string ffmpegArgs)
        {
            Log4Net.WriteLog("runFFmpeg", $"ffmpeg run path:{ffmpegPath}, parameters:{ffmpegArgs}");
            if (string.IsNullOrEmpty(ffmpegPath))
            {
                Log4Net.WriteError("runFFmpeg", "ffmpeg not found!", new Exception("ffmpeg not found!"));
                return;
            }
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = ffmpegPath;
            startInfo.Arguments = ffmpegArgs;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = startInfo;

            process.OutputDataReceived += Process_OutputDataReceived;
            process.ErrorDataReceived += Process_ErrorDataReceived;

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();
            process.Close();
            Log4Net.WriteLog("runFFmpeg", "FFmpeg execution completed.");
        }

        static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                Log4Net.WriteLog("Process_OutputDataReceived", "FFmpeg Output: " + e.Data);
            }
        }

        static void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                Log4Net.WriteLog("Process_ErrorDataReceived", "FFmpeg Error: " + e.Data);
            }
        }
    }
   

}
