   M S B A M L   `   `   `  ����H  DVideoAutoClip, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:VideoAutoClip.Views.PagesVideoAutoClip.Views.Pages  5      -    'VideoAutoClip.Views.Pages.DashboardPage   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:VideoAutoClip.Views.Pages     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      A =Wpf.Ui, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5        N� Title$  DashboardPage��5	         ApplicationBackgroundBrush  Wpf.Ui.Markup.Design   
Background# �   5        TextFillColorPrimaryBrush   
Foreground# �  5       N� 
Foreground# �  5      .��5      � $
��auto��6   $��0,75,0,0q�6    � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $	��400 �6   �� 5      $	��100 �6   �� 5      $	��32* �6   �� 5      $��80 �6   .+�5   
     Wpf.Ui.Controls.Button  $��	Click me!��5         Icon*  $Wpf.Ui.Controls.IconElementConverter$ Fluent24 5       ��5      �� *#!ViewModel.CounterIncrementCommand+ �� Mode$ OneWay=�5       �� 5!   
   $��	120,0,0,0q�5"      $��Center=�5#      ��5$      �� *ViewModel.Counter+$ OneWay=�r� 5%   
   -   5         r�Name$ mediaPlayer��5'      $��1��5&       r� LoadedBehavior$ Manual=�5(      	 r� Stretch$
	 Fill=�5*      $��337,0,386,58q�5,   -   �� 5-   
   
 �� Orientation$
 
Horizontal=�6   $��2��6.   .+�5.        -   5          Name$ VideoPlayButton��5/      $��播放��50      $��	10, 0,0,0q�51      $��60��52         
Appearance$ Primary=�6;     53      -   5        $ VideoPauseButton��54      $��暂停��55      $��60��57      $ Primary=�6@   �� 58      $��/Assets/Pause.png��6   $
��Fill=�6/   $��60��6>   $��40��6I   �� 59      $��59��6   $��/Assets/volume.png��6    $
��Fill=�6<   $��65��6K   �� 5:      -   5         ��Name$ VideoVolumeSlider��5;       � Minimum$ 0Q�5=       � Maximum$ 1Q�6"    � Value$	 0.5Q�6.   $	��375��5>      $��Center=�6"   $��Center=�6?   5?      �� 5@   
   -   5        $ timelineSlider��5B      $��1��5A      $��Center=�5C      $
��1790��5E      $��
0,357,0,22q�6      5G   
   -   5        $��3��5H      $��选择视频文件��5I      $	��134��5J      
���   �$ Primary=�5L   ,   $��60��6A   $��	33,98,0,0q�6M   $	��Top=�6`     5M   
   -   5        $��3��5N      $��选择输出路径��5O      $	��130��5P      
���   �$��273,103,0,0q�5R   1   $ Primary=�6F   $��60��6[   $	��Top=�6g     Wpf.Ui.Controls.TextBlock  5S   
   -   5          Name$ selectedFileTextBlock��5T      $��3��5U      
���   �$��选中的视频文件��5W      ${�False��5X      $��
20,252,0,0q�5Y      $	��306��6"   $	��200��6.   $
��True��6>    � UseLayoutRounding$
 True��6c    � Cursor$  ��6|   $��WordEllipsis=�6�   $
��Left=�6�   $
��Wrap=�6�   $	��100��6�     5[   
   -	   5        $  selectedOutputDirTextBlock��5\      $��3��5]      
���   �$!��处理后文件保存路径��5_      ${�False��5`      $��466,290,0,44q�5a      $
��Wrap=�6#   $��WordEllipsis=�67   $
��Left=�6S   $	��352��6n   $	��200��6z   $��98��6�     5c   
   -
   5        $ DoVideoOperationBtn��5d      $��4��5e      $	��135��5f      $��60��5g      
���   �$��27,0,0,0q�5i   '   $ Primary=�69   $��16(�6N   $��Arial)�6\   ���	#FF3393EB.��5k      �� $
 
Horizontal=�6   $��35��62   $��98��6>   .+�5l      �� $��22��6   $��/Assets/execute.png��6$   $
��Fill=�6A   $��25��6P    J� OpacityMask 5m      �� 5n       �� Color$ 	#FFEEE9E9��6*    �� Opacity$	 0.5Q�6<   5p      �� 5q      $��Center=�6   $��67��67   $��20(�6B   
��������� 6d    � Language$ zh-cn�6h   $� ��6y   �� 6�   $�执行��6�   6�   5r      5s        Wpf.Ui.Controls.TextBox  5u   
   -   5          Name$ watermarkTextBox��5v      $��3��5w      $��40��5x      $	��200��5y         PlaceholderText$ 请输入水印内容��5z      $��496,118,1224,282q�5{       �� MaxLines$ 1��6'   5~      5      