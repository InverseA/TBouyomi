   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   C ?TBouyomi, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Twitch_BouyomiTwitch_Bouyomi 5   	      Twitch_Bouyomi.ShutDown   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   .localclr-namespace:Twitch_Bouyomi     5   	     � WindowStartupLocation$  CenterOwner=�5	   	    � Title$ 關閉確認��5
   	   $	��167��6   $	��409��6#    � WindowStyle$ 
ToolWindow=�6/   $��1q�6H    � 
ResizeMode$ NoResize=�6\   (   "MahApps.Metro.Controls.MetroWindow   NonActiveGlowBrush �	#FF666666   TitleForeground �	#FFE8E8E8���	#FF004D9B   WindowTitleBrush �	#FF323232    NonActiveWindowTitleBrush �	#FF323232 � 	Resources 5         BaseButtonStyle	 �� 
TargetType
 �� Property �� Value �� 
TargetType J� SnapsToDevicePixels �� Triggers P� Property P� Value��%J  &	        �� 5   
   "	 ��6(   .�5      �� 
 �@�FocusVisualStyle 61   Z� �� 5      
 � �� �#1919196+   �� 5      
 � ��$ 1q�60   �� 5      
 � ��
 �����6+   �� 5      
 � ��$ Center=�6;   �� 5      
 � ��$ Center=�69   �� 5      
 � �� 5      �� 5      " ��6&   .R�5      �� $��Chrome��6!   #��z��5   !   #��z��5   !   #��z��5   !    . ._�5      �� $��	Presenter��6/   #��z��6@   #��z��5   3   #��z��5    3   ��. # z 5"   3   5#       5$      P� 5%       �@J�IsMouseOver . . �5&   "   �� 
 � �� �#4d4d4d6?   �� 5'   "   
 � ��
 �����6?   5(      P� 5)       � �� . . �5*   "   �� 
 � ��
 �   �6?   �� 5+   "   
 � �� �#2929296?   5,      5.      50      51      52      .��54      � ���	#FF323232.+�55   
   �� -   5         ��Name$ label��55      $*��$關閉Twitch棒讀醬('・ω・`)？��6   
�������$��Center=�6T   $��100,28,99,0q�6q   $	��Top=�6�   $	��202��6�   $��Center=�6�   $��14(�6�   �� 56   
   -   5         ��Name$ button_Sure��56      #��[  6&   $��確定��6O   $��Center=�6\   $��35��6y   $��66,80,215,0q�6�   $	��Top=�6�   $	��120��6�   $��14(�6�   $��Center=�6�   6�   �� 57   
   -   5        $ button_Cancel��57      #��[  6(   $��取消��6Q   $��Center=�6^   $��35��6{   $��215,80,66,0q�6�   $	��Top=�6�   $	��120��6�   $��14(�6�   $��Center=�6�   6�   59      5:      