// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Windows.Controls;
using VideoAutoClip.ViewModels.Pages;
using Wpf.Ui.Controls;
using System.IO;
using VideoAutoClip.Helpers;
using System;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace VideoAutoClip.Views.Pages
{
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public DashboardViewModel ViewModel { get; }
        List<string> selectedVideoFiles = new List<string>();
        string finalVideoOutFile = "";

        public DashboardPage(DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
            mediaPlayer.Source = new Uri("D:\\workspace\\auto-clip\\concat_video.mp4", UriKind.RelativeOrAbsolute);
            mediaPlayer.Play();
            mediaPlayer.Position = TimeSpan.FromSeconds(1); // 设置位置为第一秒
            mediaPlayer.Pause(); // 暂停播放

        }

        private void VideoPauseButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void VideoPlayButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Play();
        }


        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Volume = (double)VideoVolumeSlider.Value;
        }

        // When the media opens, initialize the "Seek To" slider maximum value
        // to the total number of miliseconds in the length of the media clip.
        private void Element_MediaOpened(object sender, EventArgs e)
        {
            timelineSlider.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
            mediaPlayer.Play();
            mediaPlayer.Position = TimeSpan.FromSeconds(1); // 设置位置为第一秒
            mediaPlayer.Pause(); // 暂停播放
        }

        // When the media playback is finished. Stop() the media to seek to media start.
        private void Element_MediaEnded(object sender, EventArgs e)
        {
            mediaPlayer.Stop();
        }
        // Jump to different parts of the media (seek to).
        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            int SliderValue = (int)timelineSlider.Value;

            // Overloaded constructor takes the arguments days, hours, minutes, seconds, milliseconds.
            // Create a TimeSpan with miliseconds equal to the slider value.
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            mediaPlayer.Position = ts;
        }


        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "All Files (*.*)|*.*", // 设置文件筛选器
                Multiselect = true // 设置是否允许选择多个文件
            };

            bool? result = openFileDialog.ShowDialog();
            List<string> selectedFiles = new List<string>();

            if (result == true)
            {

                foreach (string item in openFileDialog.FileNames)
                {
                    selectedFiles.Add(item);
                }
                // 处理选择的文件路径
                // 打印语句
                Log4Net.WriteLog("SelectFileButton_Click", string.Format("user choosed file path:{0}", string.Join(",", selectedFiles)));
                VideoClip(selectedFiles);
            }
        }

        private void VideoClip(List<string> filePaths, string waterMark)
        {
            // 当前目录下创建tmp目录，用来存放临时文件；
            string currentDir = Directory.GetCurrentDirectory();
            string tmpOutputDir = Path.Combine(currentDir, "tmp");

            if (!Directory.Exists(tmpOutputDir))
            {
                Directory.CreateDirectory(tmpOutputDir);
            }
            // 文件排序
            List<string> sortedSelectFiles = filePaths.OrderBy(x => x).ToList();

            // 1. 每个视频前几秒和后几秒切掉；
            List<string> runFfmpegCmds = new();
            string outputFile = "";
            string ffmpegCmd = "";
            List<string> cuttedFilePath = new();
            foreach (string file in sortedSelectFiles)
            {
                outputFile = tmpOutputDir + "\\" + file.Split("\\").Last();
                cuttedFilePath.Add(outputFile);
                ffmpegCmd = FFmpegHelper.videoCutDuration(file, outputFile);
                runFfmpegCmds.Add(ffmpegCmd);
            }
            Log4Net.WriteLog("VideoClip", string.Format("accomplish video cut cmd, cmd cnt:{0}", runFfmpegCmds.Count));
            // 2. 每个视频的比例变成1.1倍 视频和视频之间利用叠化转场；
            string concatMultiVideo = tmpOutputDir + "concat_res.mp4";
            runFfmpegCmds.Add(FFmpegHelper.concatMultiVideo(cuttedFilePath, concatMultiVideo));
            Log4Net.WriteLog("VideoClip", string.Format("accomplish concatMultiVideo, cmd cnt:{0}", runFfmpegCmds.Count));
            // 3. 视频增加文字水印
            if (!string.IsNullOrEmpty(waterMark))
            {
                runFfmpegCmds.Add(FFmpegHelper.videoAddText(concatMultiVideo, waterMark));
            }
            foreach (string cmd in runFfmpegCmds)
            {
                FFmpegHelper.runFFmpeg(cmd);
            }

            Log4Net.WriteLog("VideoClip", string.Format("accomplish video process, cmd cnt:{0}", runFfmpegCmds.Count));

        }

        private void SelectOutputDirButton_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new();
            dialog.Title = "选择文件夹";
            string selectDir = "";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                selectDir = dialog.FileName;
            }
            Log4Net.WriteLog("SelectOutputDirButton_Click", string.Format("output directory:{0}", selectDir));
        }

        private void doOperation_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}