   M S B A M L   `   `   `  ���� C  ?TBouyomi, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Twitch_BouyomiTwitch_Bouyomi  T PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls     Twitch_Bouyomi.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:Twitch_Bouyomi     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     -     � WindowStartupLocation$  Manual=�(  "MahApps.Metro.Controls.MetroWindow   ShowTitleBar .    ShowIconOnTitleBar .    	GlowBrush �	#FF004D9B   SaveWindowPosition .  � Title$ Twitch棒讀醬 1.3.1��$	��870��$��1q�$��0q�$	��870��$	��500��$	��870��$	��450�� � 
ResizeMode$ CanMinimize=�$	��500��   NonActiveGlowBrush �	#FF666666   TitleForeground �	#FFE8E8E8���	#FF004D9B	   WindowTitleBrush	 �	#FF323232 
   NonActiveWindowTitleBrush
 �	#FF323232 � 	Resources    BaseButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType J� SnapsToDevicePixels �� Triggers P� Property P� Value��%�  &	        �� " ��.���  �@�FocusVisualStyle Z� ��  � �� �#191919��  � ��$ 1q���  � ��
 �������  � ��$ Center=���  � ��$ Center=���  � �� �� " ��.R��� $��Chrome��#��z��#��z��#��z�� . ._��� $��	Presenter��#��z��#��z��#��z����. # z  P�  �@J�IsMouseOver . . ���  � �� �#4d4d4d��  � ��
 �����P�  � �� . . ���  � ��
 �   ���  � �� �#292929.��� ���	#FF323232$
��Auto=�$	��469��$	��Top=� � RowDefinitions �� $	��50* �$��50��$��50���� $
��487* � � ColumnDefinitions �� $
��548* ��� $
��371* �.+��� -    ��Name$ Change_channel��#��[  $��設定頻道��$
��Left=�$��10,4,0,6q�$	��116���� -    ��Name$ 
current_ID��$��目前頻道ID��
�������$
��Left=�$��Center=�$��Center=�$��	138,4,0,6q�$��90��$��15(�$��90��$��90���� -   $ current_ID_text��$��尚未登入��$��233,4,113,6q����	#FFBA64F5$
��Left=�$��Center=�$��15(�$
��Bold"��� $��14��$��	0,42,10,0q�$	��Top=�$��2���� ���	#FF636363$��1q�$��	17,16,0,0q�$��1��$	��235��$	��Top=�$
��Left=�$	��485��._��� -    ��Name$ 	SE_Cancel��$$��啟用SE關鍵字互相取消 ��$
��Left=�$��20��$��
23,170,0,0q�$	��Top=�$	��148��
�������$��Center=��� -   $ Speech_By_Command��$0��*只有在使用指令 !TB 時才會棒讀 ��$
��Left=�$��20��$��
41,216,0,0q�$	��Top=�$	��199��
�������$��Center=�$��1���� -   $ 
WPF_Volume��$��音量��$
��Left=�$��	19,30,0,0q�$��1��$��90��
�������$	��Top=�$��25��$��Right=�$��Center=��� -    ��Name$ WPF_Volume_slider��$��
184,33,0,0q�$��1��$	��280��$��25��$	��Top=� � Minimum$ 0Q� � Maximum$	 100Q� � Value$	 100Q� �� IsSnapToTickEnabled .  �� TickFrequency$ 1Q�|�.  $��Center=� J� RenderTransformOrigin$ 0.5,0.5&�$
��Left=�$��0q��� -	   $ WPF_Rate_slider��$
��Left=�$��
184,63,0,0q�$��1��$	��280��$��25��$	��Top=�$	 -10Q�$ 10Q�$ 1Q� . $ 1Q�|�.  $��Center=�$��Center=�$ 0.5,0.5&��� -
   $ WPF_Rate��$��速度��$
��Left=�$��	19,60,0,0q�$��1��$��90��
�������$��Right=�$��Center=�$��25��$	��Top=��� -     ��Name$  WPF_Volume_slider_box��$
��Left=�$��
119,30,0,0q�$��1��! �� TextWrapping$
! Wrap=�$��35��$��Center=�$	��Top=�$��25��" �� 
IsReadOnly" . # J� 	AllowDrop# .  |�.  $��Center=����	#FF323232���	#FFE6E6E6$��0q����� *Value+$ �� ElementName$$ WPF_Volume_slider���� -   $  WPF_Rate_slider_box��$
��Left=�$��
119,60,0,0q�$��1��$
! Wrap=�$��35�����	#FF323232$��Center=�$��Center=�$	��Top=�$��25��" . |�.  $��0q����	#FFE6E6E6���� *Value+$$ WPF_Rate_slider���� -   $ Bouyomi_setting��$��棒讀詳細設定��$
��Left=�$��	190,2,0,0q�$��1��$	��Top=�
����������	#FF323232$
��Bold"�$	��119��$��Center=�$��Center=�$��14.667(� . �� -   % ��Name$% Design��$
��Left=�$��19��$��
19,386,0,0q�$��1��$
��Wrap=�$	��Top=�$��98��
��������� $�Design ���� -   ���#be7aff$G��Ahttp://twitchbouyomi.blogspot.tw/2016/03/twitchtwitchbouyomi.html?�& �� TextDecorations$& 	Underline{�.��� $�InverseA���� ���	#FF636363$��1q�$��
18,272,0,0q�$��1��$��79��$	��Top=�$
��Left=�$	��485��._�� -   ' �Name$' 
Mic_volume��$
��Left=�$��4��$��	20,64,0,0q�$	��Top=�$	��442�����	#FF3A9BFF���	#FF060606�� -   $ checkBox��$��啟用防搶話功能 ��
�������$��40,284,360,115q�$��1��|�.  $��Center=�$��20��$��Center=��� -   $ Bouyomi_DoNotTalk��$��實驗性質功能��$
��Left=�$��191,256,0,0q�$��1��$	��Top=�
����������	#FF323232$
��Bold"�$	��119��$��Center=�$��Center=�$��14.667(��� -   ( ��Name$( Device_comboBox��$
��Left=�$��21��$��141,307,0,0q�$��1��$	��Top=�$	��341��|�.  �� -   $ 
Mic_Device��$��選擇裝置(Device)��$
��Left=�$��
25,300,0,0q�$��1��$	��120��
�������$��Center=�$��Center=�$��36��$	��Top=��� -   $  enter_command��$��1��$
��Left=�$��30��$��
10,379,0,0q�$��1��$
! Wrap=�$	��Top=�$	��328��
���   �$��	發話框�����	#FF505050$��14(�$��4q��� -   $ DoNotDisturb_setumei��$��(仍在實驗性階段)��$
��Left=�$��141,278,0,0q�$��1��$	��102�����	#FF747474$��Center=�$��Center=�$��32��$	��Top=��� -   $ WordNum_Limit��$��棒讀字數限制��$
��Left=�$��
19,150,0,0q�$��1��$��90��
�������$��Right=�$��25��$	��Top=�$��Center=��� -   $  WordNum_Limitslider_box��$
��Left=�$��119,150,0,0q�$��1��$
! Wrap=�$��35�����	#FF323232$��Center=�$��Center=�$	��Top=�$��25��" . |�.  $��0q����	#FFE6E6E6���� *Value+$$ WordNum_Limit_slider���� -   $ WordNum_Limit_slider��$
��Left=�$��184,153,0,0q�$��1��$	��280��$��25��$	��Top=�$ 5Q�$ 60Q�$ 20Q� . $ 1Q�|�.  $��Center=�$��Center=�$ 0.5,0.5&��� -   $ Speak_Pause��#��[  $��暫停棒讀(Pause)��$��362,361,0,0q�$��1��$
��Left=�$	��141��$��48��$	��Top=��� -   $ SE_Pause��#��[  $��暫停音效(SE Pause)��$��216,361,0,0q�$��1��$
��Left=�$	��141��$��48��$	��Top=��� -   $  IRC_textbox��$��1��$
��Left=�$	��412��$��	10,10,0,0q�$��2��$
! Wrap=�$�� ��$	��Top=�$	��328��
���   ����	#FFA4A4A4" . $��14(�) �� MaxLines$	) 150���� -   $ SE_Volume_L��$��音效音量��$
��Left=�$��
19,120,0,0q�$��1��$��90��
�������$��Right=�$��Center=�$��25��$	��Top=��� -   $  SE_Volume_box��$
��Left=�$��119,120,0,0q�$��1��$
! Wrap=�$��35�����	#FF323232$��Center=�$��Center=�$	��Top=�$��25��" . |�.  $��0q����	#FFE6E6E6���� *Value+$$ SE_Volume_slider���� -   $ SE_Volume_slider��$
��Left=�$��183,123,0,0q�$��1��$	��280��$��25��$	��Top=�$ 0Q�$	 100Q�$	 100Q� . $ 1Q�|�.  $��Center=�$��Center=�$ 0.5,0.5&��� -    $ 	Pitch_num��$��音高��$
��Left=�$��	19,90,0,0q�$��1��$��90��
�������$��Right=�$��Center=�$��25��$	��Top=��� -!   $  Pitch_num_box��$
��Left=�$��
119,90,0,0q�$��1��$
! Wrap=�$��35�����	#FF323232$��Center=�$��Center=�$	��Top=�$��25��" . |�.  $��0q����	#FFE6E6E6���� *Value+$$ Pitch_num_slider���� -"   $ Pitch_num_slider��$
��Left=�$��
184,93,0,0q�$��1��$	��280��$��25��$	��Top=�$	 -10Q�$ 10Q�$ 0Q� . $ 1Q�|�.  $��Center=�$��Center=�$ 0.5,0.5&�