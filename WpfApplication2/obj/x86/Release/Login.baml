   M S B A M L   `   `   `  ���� T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  C ?TBouyomi, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Twitch_BouyomiTwitch_Bouyomi    Twitch_Bouyomi.Login   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     .localclr-namespace:Twitch_Bouyomi       � WindowStartupLocation$  CenterOwner=�(   "MahApps.Metro.Controls.MetroWindow   	GlowBrush �	#FF004D9B � Title$ Login��$	��271��$	��490�� � WindowStyle$ 
ToolWindow=�$��1q�$	��244��$	��241��$	��500��$	��500�� � 
ResizeMode$ NoResize=�   NonActiveGlowBrush �	#FF666666   TitleForeground �	#FFE8E8E8���	#FF004D9B   WindowTitleBrush �	#FF323232    NonActiveWindowTitleBrush �	#FF323232	 � 	Resources	    BaseButtonStyle
 �� 
TargetType �� Property �� Value �� 
TargetType J� SnapsToDevicePixels �� Triggers P� Property P� Value��%�  &	        �� "
 ��.���  �@�FocusVisualStyle Z� ��  � �� �#191919��  � ��$ 1q���  � ��
 �������  � ��$ Center=���  � ��$ Center=���  � �� �� " ��.R��� $��Chrome��#��z��#��z��#��z�� . ._��� $��	Presenter��#��z��#��z��#��z����. # z  P�  �@J�IsMouseOver . . ���  � �� �#4d4d4d��  � ��
 �����P�  � �� . . ���  � ��
 �   ���  � �� �#292929.��� ���	#FF323232$��0,0,0,-2q�.+��� -    ��Name$ Channel_Account��$��
110,63,0,0q� �� TextWrapping$
 Wrap=�$��頻道ID��$��Center=�$��24��$	��Top=�$
��Left=�$	��310�����	#FF1B1B1B���	#FFCDCDCD$��0q�$��5,0,0,0q��� -   $ OAuth��$��
110,92,0,0q�$
 Wrap=�$��OAuth��$��Center=�$��24��$	��Top=�$
��Left=�$	��310�����	#FF1B1B1B���	#FFCDCDCD$��0q�$��5,0,0,0q��� -    ��Name$ button��$��確定��#��[  $��192,166,192,12q�$��38��$��Center=�$	��108���� -    ��Name$ Account��$��帳號��
�������$��	42,62,0,0q�$��Right=�$��Center=�$��24��$	��Top=�$
��Left=�$��60���� -   $ OAuth_ID��$��OAuth��
�������$��	42,91,0,0q�$��Right=�$��Center=�$��24��$	��Top=�$
��Left=�$��60���� -    ��Name$ 
OAuth_Link��$
��Left=�$��15��$��
109,36,0,0q�$
��Wrap=�$	��Top=�$��66���� -   ���#be7aff$#��http://www.twitchapps.com/tmi?� �� TextDecorations$ 	Underline{�.�取得OAuth�� -   $ OAuth_Intro��$
��Left=�$��15��$��
207,36,0,0q�$
��Wrap=�$	��Top=�$	��133���� -	   ���#be7aff$H��Bhttps://help.twitch.tv/customer/portal/articles/1302780-twitch-irc?�$ 	Underline{�.�Twitch官方對IRC的說明�� -
   $ 	Account_X��$�� ��
���  ��$��
425,62,0,0q�$��Center=�$��Center=�$��24��$	��Top=�$
��Left=�$��23���� -   $ 
OAuth_ID_X��$�� ��
���  ��$��
425,92,0,0q�$��Center=�$��Center=�$��24��$	��Top=�$
��Left=�$��23���� -   $ Login_Wrong��$�� ��
���  ��$��192,123,0,0q�$��Center=�$��24��$	��Top=�$
��Left=�$	��290���� -    ��Name$ Auto_Login_check��$��    下次自動登入��
�������$
��Left=�$��18��$��
77,126,0,0q�$	��Top=�$	��110��$��Center=�