﻿#pragma checksum "..\..\..\..\..\Views\Pages\DashboardPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2A2B3C38D1E970A0D5FB505A7D6949FF966EEEC0"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using VideoAutoClip.Views.Pages;
using Wpf.Ui;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Converters;
using Wpf.Ui.Markup;


namespace VideoAutoClip.Views.Pages {
    
    
    /// <summary>
    /// DashboardPage
    /// </summary>
    public partial class DashboardPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\..\..\Views\Pages\DashboardPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MediaElement mediaPlayer;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\..\Views\Pages\DashboardPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Ui.Controls.Button VideoPlayButton;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\..\Views\Pages\DashboardPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Ui.Controls.Button VideoPauseButton;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\..\Views\Pages\DashboardPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider VideoVolumeSlider;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\..\Views\Pages\DashboardPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider timelineSlider;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\..\Views\Pages\DashboardPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Ui.Controls.TextBlock selectedFileTextBlock;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\..\..\Views\Pages\DashboardPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Ui.Controls.TextBlock selectedOutputDirTextBlock;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\..\..\Views\Pages\DashboardPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Ui.Controls.Button DoVideoOperationBtn;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\..\..\Views\Pages\DashboardPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Ui.Controls.TextBox watermarkTextBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.14.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/VideoAutoClip;component/views/pages/dashboardpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Pages\DashboardPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.14.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.mediaPlayer = ((System.Windows.Controls.MediaElement)(target));
            
            #line 33 "..\..\..\..\..\Views\Pages\DashboardPage.xaml"
            this.mediaPlayer.MediaOpened += new System.Windows.RoutedEventHandler(this.Element_MediaOpened);
            
            #line default
            #line hidden
            
            #line 34 "..\..\..\..\..\Views\Pages\DashboardPage.xaml"
            this.mediaPlayer.MediaEnded += new System.Windows.RoutedEventHandler(this.Element_MediaEnded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.VideoPlayButton = ((Wpf.Ui.Controls.Button)(target));
            
            #line 41 "..\..\..\..\..\Views\Pages\DashboardPage.xaml"
            this.VideoPlayButton.Click += new System.Windows.RoutedEventHandler(this.VideoPlayButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.VideoPauseButton = ((Wpf.Ui.Controls.Button)(target));
            
            #line 46 "..\..\..\..\..\Views\Pages\DashboardPage.xaml"
            this.VideoPauseButton.Click += new System.Windows.RoutedEventHandler(this.VideoPauseButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.VideoVolumeSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 52 "..\..\..\..\..\Views\Pages\DashboardPage.xaml"
            this.VideoVolumeSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.ChangeMediaVolume);
            
            #line default
            #line hidden
            return;
            case 5:
            this.timelineSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 60 "..\..\..\..\..\Views\Pages\DashboardPage.xaml"
            this.timelineSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.SeekToMediaPosition);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 68 "..\..\..\..\..\Views\Pages\DashboardPage.xaml"
            ((Wpf.Ui.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SelectFileButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 74 "..\..\..\..\..\Views\Pages\DashboardPage.xaml"
            ((Wpf.Ui.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SelectOutputDirButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.selectedFileTextBlock = ((Wpf.Ui.Controls.TextBlock)(target));
            return;
            case 9:
            this.selectedOutputDirTextBlock = ((Wpf.Ui.Controls.TextBlock)(target));
            return;
            case 10:
            this.DoVideoOperationBtn = ((Wpf.Ui.Controls.Button)(target));
            
            #line 97 "..\..\..\..\..\Views\Pages\DashboardPage.xaml"
            this.DoVideoOperationBtn.Click += new System.Windows.RoutedEventHandler(this.doOperation_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.watermarkTextBox = ((Wpf.Ui.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

