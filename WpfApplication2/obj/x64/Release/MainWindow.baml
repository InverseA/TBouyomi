   M S B A M L   `   `   `  ���� L  HTwitchBouyomi 0.8, Version=0.4.5.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Twitch_BouyomiTwitch_Bouyomi  T PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls     Twitch_Bouyomi.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:Twitch_Bouyomi     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     -     � WindowStartupLocation$  Manual=�(  "MahApps.Metro.Controls.MetroWindow   ShowTitleBar .    ShowIconOnTitleBar .    	GlowBrush �	#FF004D9B   SaveWindowPosition .  � Title$ Twitch bouyomi 0.8��$	��870��$��1q�$��0q�$	��870��$	��500��$	��870��$	��450�� � 
ResizeMode$ CanMinimize=�$	��500��   NonActiveGlowBrush �	#FF666666   TitleForeground �	#FFE8E8E8���	#FF004D9B	   WindowTitleBrush	 �	#FF323232 
   NonActiveWindowTitleBrush
 �	#FF323232.��� ���	#FF323232$
��Auto=�$	��469��$	��Top=� � RowDefinitions �� $	��50* �$��50��$��50���� $
��487* � � ColumnDefinitions �� $
��548* ��� $
��371* �.+��� -    ��Name$ Change_channel��$��設定頻道��$
��Left=�$��10,6,0,4q�$	��116���� -    ��Name$ 
current_ID��$��目前頻道ID��
�������$
��Left=�$��Center=�$��Center=�$��	138,6,0,4q�$��90��$��15(�$��90��$��90���� -   $ current_ID_text��$��尚未登入��$��233,6,113,4q����	#FF3D9DEE$
��Left=�$��Center=�$��15(�$
��Bold"��� $��14��$��0,-4.358,10,0q�$	��Top=�$��1���� -    ��Name$ IRC_textRoom��$��1��$��10,10,10,50q�$��2��
���   ����	#FFCFCFCF �� 
IsReadOnly .  J� 	AllowDrop . ��. " �� VerticalScrollBarVisibility$
 Auto=�$��14(� �� 
LineHeight$ 2��$	��328�� �� SelectionBrush Z� . �/�  �� 	AllowDrop .  .T�J� .*��� -   $ 
WPF_Volume��$��音量(Volume)��$
��Left=�$��	13,49,0,0q�$��1��$	��120��
�������$	��Top=�$��36��$��Center=�$��Center=��� ���	#FF636363$��1q�$��	18,29,0,0q�$��1��$	��170��$	��Top=�$
��Left=�$	��485���� -    ��Name$ WPF_Volume_slider��$
��Left=�$��
226,58,0,0q�$��1��$	��257��$��Center=�$��Center=�$��27��$	��Top=� � Minimum$ 0Q� � Maximum$	 100Q� � Value$	 100Q� �� IsSnapToTickEnabled .  �� TickFrequency$ 1Q��� -   $ WPF_Rate_slider��$
��Left=�$��226,102,0,0q�$��1��$	��257��$��Center=�$��Center=�$��27��$	��Top=�$	 -10Q�$ 10Q�$ 2Q� . $ 1Q��� -	   $ WPF_Rate��$��速度(Rate)��$
��Left=�$��	13,94,0,0q�$��1��$	��120��
�������$��Center=�$��Center=�$��36��$	��Top=��� -
    ��Name$ WPF_Volume_slider_box��$
��Left=�$��
144,49,0,0q�$��1�� �� TextWrapping$
 Wrap=�$��58��
���   �
�������$��Center=�$��Center=�$	��Top=�$��36�� . ����  �� ElementName$ WPF_Volume_slider�� �� Path$ Value��� -   $ WPF_Rate_slider_box��$
��Left=�$��
144,94,0,0q�$��1��$
 Wrap=�$��58��
���   �
�������$��Center=�$��Center=�$	��Top=�$��36�� . ���� $ WPF_Rate_slider��$ Value��� -   $ Bouyomi_setting��$��棒讀詳細設定��$
��Left=�$��
191,15,0,0q�$��1��$	��Top=�
����������	#FF323232$
��Bold"�$	��119��$��Center=�$��Center=�$��14.667(��� -   $ Set_the_change��$��套用設定(Apply)��$��362,361,0,0q�$��1��$
��Left=�$	��141��$��48��$	��Top=��� -     ��Name$  Design��$
��Left=�$��19��$��
19,386,0,0q�$��1��$
��Wrap=�$	��Top=�$��98��
�������	Design �� -   ���#be7aff$G��Ahttp://twitchbouyomi.blogspot.tw/2016/03/twitchtwitchbouyomi.html?�! �� TextDecorations$! 	Underline{�.�
InverseA�� ���	#FF636363$��1q�$��
18,223,0,0q�$��1��$	��129��$	��Top=�$
��Left=�$	��485��._�� -   " �Name$" 
Mic_volume��$
��Left=�$��6��$��
15,100,0,0q�$	��Top=�$	��442�����	#FF3A9BFF�� -   $ WPF_DoNotTalk_Sure��$6��0啟用防搶話功能 (Enable DO NOT Disturb Me)��$
��Left=�$��
37,229,0,0q�$��1��$	��268��
�������$��Center=�$��Center=�$��36��$	��Top=��� -   # ��Name$# checkBox��$
��Left=�$��
27,239,0,0q�$��1��$	��Top=�$��15��$	��268���� -   $ Bouyomi_DoNotTalk��$��實驗性質功能��$
��Left=�$��191,209,0,0q�$��1��$	��Top=�
����������	#FF323232$
��Bold"�$	��119��$��Center=�$��Center=�$��14.667(��� -   $ ��Name$$ Device_comboBox��$
��Left=�$��21��$��136,287,0,0q�$��1��$	��Top=�$	��341���� -   $ 
Mic_Device��$��選擇裝置(Device)��$
��Left=�$��
18,279,0,0q�$��1��$	��120��
�������$��Center=�$��Center=�$��36��$	��Top=��� -   $ enter_command��$��1��$
��Left=�$��30��$��
10,374,0,0q�$��1��$
 Wrap=�$	��Top=�$	��328��
���   �$��	發話框�����	#FF505050$��14(�$��4q��� -   $ DoNotDisturb_setumei��$3��-仍在實驗性階段，目前暫停使用。��$
��Left=�$��
24,254,0,0q�$��1��$	��183�����	#FF747474$��Center=�$��Center=�$��32��$	��Top=��� -   $ WordNum_Limit��$��棒讀字數限制��$
��Left=�$��
13,140,0,0q�$��1��$	��120��
�������$��Center=�$��Center=�$��36��$	��Top=��� -   $ WordNum_Limitslider_box��$
��Left=�$��144,141,0,0q�$��1��$
 Wrap=�$��58��
���   �
�������$��Center=�$��Center=�$	��Top=�$��36�� . ���� *Value+$ WordNum_Limit_slider���� -   $ WordNum_Limit_slider��$
��Left=�$��226,148,0,0q�$��1��$	��257��$��Center=�$��Center=�$��27��$	��Top=�$ 5Q�$ 60Q�$ 20Q� . $ 1Q��� -   $ Speak_Pause��$��暫停棒讀(Pause)��$��210,361,0,0q�$��1��$
��Left=�$	��141��$��48��$	��Top=��� -   $ 
Viewer_num��$
��Left=�$��	423,8,0,0q�$
 Wrap=�$��● OffLine��$��93�����	#FF323232���	#FF8D1B1B$��Center=�$	��Top=�$��36�� . $��16(�$
��Bold"�$��2����Z� % �� TextDecorations% Z� 