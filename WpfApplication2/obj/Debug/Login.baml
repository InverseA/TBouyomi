   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   C ?TBouyomi, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Twitch_BouyomiTwitch_Bouyomi 5   	      Twitch_Bouyomi.Login   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   .localclr-namespace:Twitch_Bouyomi     5   	     � WindowStartupLocation$  CenterOwner=�5	   	   (   "MahApps.Metro.Controls.MetroWindow   	GlowBrush �	#FF004D9B � Title$ Login��5   	   $	��271��6   $	��490��6$    � WindowStyle$ 
ToolWindow=�60   $��1q�6I   $	��244��6]   $	��241��6m   $	��500��6}   $	��500��6�    � 
ResizeMode$ NoResize=�6�      NonActiveGlowBrush �	#FF666666   TitleForeground �	#FFE8E8E8���	#FF004D9B   WindowTitleBrush �	#FF323232    NonActiveWindowTitleBrush �	#FF323232	 � 	Resources	 5         BaseButtonStyle
 �� 
TargetType �� Property �� Value �� 
TargetType J� SnapsToDevicePixels �� Triggers P� Property P� Value��%J  &	        �� 5   
   "
 ��6(   .�5      ��  �@�FocusVisualStyle 61   Z� �� 5       � �� �#1919196+   �� 5       � ��$ 1q�60   �� 5       � ��
 �����6+   �� 5       � ��$ Center=�6;   �� 5       � ��$ Center=�69   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $��Chrome��6!   #��z��5   !   #��z��5   !   #��z��5   !    . ._�5      �� $��	Presenter��6/   #��z��6@   #��z��5   3   #��z��5   3   ��. # z 5!   3   5"       5#      P� 5$       �@J�IsMouseOver . . �5%   "   ��  � �� �#4d4d4d6?   �� 5&   "    � ��
 �����6?   5'      P� 5(       � �� . . �5)   "   ��  � ��
 �   �6?   �� 5*   "    � �� �#2929296?   5+      5-      5/      50      51      .��54      � ���	#FF323232$��0,0,0,-2q�6"   .+�55   
   �� -   5         ��Name$ Channel_Account��55      $��
110,63,0,0q�6+    �� TextWrapping$
 Wrap=�6?   $��頻道ID��6S   $��Center=�6_   $��24��6�   $	��Top=�6�   $
��Left=�6�   $	��310��6�   ���	#FF1B1B1B���	#FFCDCDCD$��0q�6�   $��5,0,0,0q�6  �� 56   
   -   5        $ OAuth��56      $��
110,92,0,0q�6!   $
 Wrap=�65   $��OAuth��6I   $��Center=�6V   $��24��6x   $	��Top=�6�   $
��Left=�6�   $	��310��6�   ���	#FF1B1B1B���	#FFCDCDCD$��0q�6�   $��5,0,0,0q�6  �� 57   
   -   5         ��Name$ button��57      $��確定��6!   #��[  6/   $��192,166,192,12q�58      $��38��59   (   $��Center=�64   $	��108��6Q   �� 5:   
   -   5         ��Name$ Account��5:      $��帳號��6!   
�������$��	42,62,0,0q�5;   '   $��Right=�6:   $��Center=�6]   $��24��6   $	��Top=�6�   $
��Left=�6�   $��60��6�   �� 5<   
   -   5        $ OAuth_ID��5<      $��OAuth��6"   
�������$��	42,91,0,0q�5=   '   $��Right=�6:   $��Center=�6]   $��24��6   $	��Top=�6�   $
��Left=�6�   $��60��6�   �� 5>   
   -   5         ��Name$ 
OAuth_Link��5>      $
��Left=�6(   $��15��6C   $��
109,36,0,0q�6O   $
��Wrap=�6c   $	��Top=�6w   $��66��6�   �� 6�   -   5        ���#be7aff$#��http://www.twitchapps.com/tmi?�5>   �    �� TextDecorations$ 	Underline{�6�   .�6  取得OAuth6&  62  �� 5?   
   -   5        $ OAuth_Intro��5?      $
��Left=�6)   $��15��6D   $��
207,36,0,0q�6P   $
��Wrap=�6d   $	��Top=�6x   $	��133��6�   �� 6�   -	   5        ���#be7aff$H��Bhttps://help.twitch.tv/customer/portal/articles/1302780-twitch-irc?�5?   �   $ 	Underline{�6  .�6E  Twitch官方對IRC的說明6V  6b  �� 5@   
   -
   5        $ 	Account_X��5@      $�� ��6#   
���  ��$��
425,62,0,0q�5A      $��Center=�6/   $��Center=�6S   $��24��6u   $	��Top=�6�   $
��Left=�6�   $��23��6�   �� 5B   
   -   5        $ 
OAuth_ID_X��5B      $�� ��6$   
���  ��$��
425,92,0,0q�5C      $��Center=�6/   $��Center=�6S   $��24��6u   $	��Top=�6�   $
��Left=�6�   $��23��6�   �� 5D   
   -   5        $ Login_Wrong��5D      $�� ��6%   
���  ��$��192,123,0,0q�5E      $��Center=�63   $��24��6U   $	��Top=�6a   $
��Left=�6y   $	��290��6�   �� 5F   
   -   5         ��Name$ Auto_Login_check��5F      $��    下次自動登入��6-   
�������$
��Left=�6Z   $��18��6u   $��
77,126,0,0q�6�   $	��Top=�6�   $	��110��6�   $��Center=�6�   6�   5I      5J      