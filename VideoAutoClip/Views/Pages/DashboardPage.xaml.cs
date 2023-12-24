// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Windows.Controls;
using VideoAutoClip.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace VideoAutoClip.Views.Pages
{
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public DashboardViewModel ViewModel { get; }

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
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "All Files (*.*)|*.*"; // 设置文件筛选器
            openFileDialog.Multiselect = true; // 设置是否允许选择多个文件

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string[] selectedFilePath = openFileDialog.FileNames;
                // 处理选择的文件路径
                // 打印语句
                Console.WriteLine("user choosed file path:" + selectedFilePath);
            }
        }

        private void VideoClip(string[] filePaths)
        {
            // 1. 每个视频前几秒和后几秒切掉；
            // 2. 每个视频的比例变成1：1
            // 3. 视频和视频之间利用叠化转场；
            // 4. 视频其他区域变成模糊
            // 5. 视频按照选择的顺序拼接，或者shuffle之后拼接
        }
    }
}
