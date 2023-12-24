using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VideoAutoClip.Helpers
{
    public class FFmpegHelper
    {
        static string ffmpegPath = AppDomain.CurrentDomain.BaseDirectory + "ffmpeg\\ffmpeg.exe"; // 替换为你的FFmpeg可执行文件的路径
        public FFmpegHelper() { }

        public void videoCutDuration(string inputFilePath, string outputFilePath)
        {
            /**
             * 
             * 
             */
            TimeSpan duration = GetVideoDuration(ffmpegPath, inputFilePath);
            Console.WriteLine("视频时长: " + duration);
            string start_time = "00:00:01";
            if (duration.TotalSeconds < 1)
            {
                Console.WriteLine("video duration too short!!!!");

                return ;
            }
            string end_time = duration.Subtract(TimeSpan.FromSeconds(1)).ToString();
            string ffmpegArgs = $"-ss {start_time} -t {end_time} -c copy -i \"{inputFilePath}\" \"{outputFilePath}\"";
            runFFmpeg(ffmpegArgs);
        }

        public void runFFmpeg(string ffmpegArgs)
        {
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

            Console.WriteLine("FFmpeg execution completed.");
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

            Regex regex = new Regex(@"Duration: (\d+):(\d+):(\d+\.\d+)");
            Match match = regex.Match(output);
            if (match.Success)
            {
                string hours = match.Groups[1].Value;
                string minutes = match.Groups[2].Value;
                string seconds = match.Groups[3].Value;

                TimeSpan duration = new TimeSpan(int.Parse(hours), int.Parse(minutes), int.Parse(seconds));
                
                return duration;
            }

            return new TimeSpan();
        }
        static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                Console.WriteLine("FFmpeg Output: " + e.Data);
            }
        }

        static void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                Console.WriteLine("FFmpeg Error: " + e.Data);
            }
        }
    }
   

}
