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

        public static string ScaleVideo(string inputFilePath, string outputFilePath)
        {
            // 视频转成9：16： 1080x1920
            string ffmpegArgs = $" -i \"{inputFilePath}\" -vf \"scale=1080:1920,format=yuv420p\" -y \"{outputFilePath}\"";
            return ffmpegArgs;
        }

        public static string DeleteFrameFromIndex(string inputFilePath, string outputFilePath, int nthFrame=6)
        {
            string ffmpegArgs = $" -i \"{inputFilePath}\" -vf \"select='mod(n,{nthFrame})'\" -y \"{outputFilePath}\"";
            return ffmpegArgs;
        }

        public static string ConcatMultiVideo(List<string> videoPaths, string outputFile)
        {
            /**
             *  
             *  合并在一起，使用叠化转场；在这之前要将视频处理成统一的分辨率
             *  ffmpeg -vsync 0 -i "D:\workspace\VideoAutoClip\VideoAutoClip\bin\Debug\net7.0-windows\tmp\scale_176.WhydoIwannaeatit#satisfying.mp4" -i "D:\workspace\VideoAutoClip\VideoAutoClip\bin\Debug\net7.0-windows\tmp\scale_177.IthinkIneedanewlipstick#satisfying.mp4" -i "D:\workspace\VideoAutoClip\VideoAutoClip\bin\Debug\net7.0-windows\tmp\scale_179.Lookslikeapimple#satisfying.mp4" -filter_complex "[0]settb=AVTB[0:v];[1]settb=AVTB[1:v];[2]settb=AVTB[2:v];[0]atrim=0:10[0:a];[1]atrim=0:22[1:a];[2]atrim=0:12[2:a];[0:v][1:v]xfade=transition=fade:duration=1:offset=5[v1];[v1][2:v]xfade=transition=fade:duration=1:offset=13[video];[0:a][1:a]acrossfade=d=1:c1=tri:c2=tri[a1];[a1][2:a]acrossfade=d=1:c1=tri:c2=tri[audio]" -b:v 10M -map "[audio]" -map "[video]" -y "D:\workspace\VideoAutoClip\VideoAutoClip\bin\Debug\net7.0-windows\tmp\concat.mp4"
             */
            string ffmpeg_cmd = "";
         
            string videoDiehua = "";
            string audioDiehua = "";
            string timeBased = "";
            string audioTrim = "";
            int totalSeconds = 0;
            int startSecond = 0;
            for (int i = 0; i < videoPaths.Count; i++)
            {
                ffmpeg_cmd += " -i \"" + videoPaths[i] +"\"";
                TimeSpan duration = GetVideoDuration(ffmpegPath, videoPaths[i]);
                totalSeconds = (int)Math.Round(duration.TotalSeconds);
                startSecond = startSecond + totalSeconds - 1;

                timeBased += $"[{i}]settb=AVTB[{i}:v];";
                audioTrim += $"[{i}]atrim=0:{totalSeconds}[{i}:a];";
                if (i == 1)
                {
                    videoDiehua += $"[{i - 1}:v][{i}:v]xfade=transition=fade:duration=1:offset={startSecond}[v{i}];";
                    audioDiehua += $"[{i - 1}:a][{i}:a]acrossfade=d=1:c1=tri:c2=tri[a{i}];";
                }
                else if ((i > 1) && (i < videoPaths.Count - 1))
                {
                    videoDiehua += $"[v{i - 1}][{i}:v]xfade=transition=fade:duration=1:offset={startSecond}[v{i}];";
                    audioDiehua += $"[a{i - 1}][{i}:a]acrossfade=d=1:c1=tri:c2=tri[a{i}];";
                }
                else if (i == videoPaths.Count - 1)
                {
                    videoDiehua += $"[v{i - 1}][{i}:v]xfade=transition=fade:duration=1:offset={startSecond},format=yuv420p[video];";
                    audioDiehua += $"[a{i - 1}][{i}:a]acrossfade=d=1:c1=tri:c2=tri[a{i}],format=fltp[audio];";
                }
            }
            
            ffmpeg_cmd += $" -filter_complex \"{timeBased}{videoDiehua}{audioDiehua} -b:v 10M -map \"[audio]\" -map \"[video]\" \"";

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
