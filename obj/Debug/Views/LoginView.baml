   M S B A M L   `   `   `  ����  mmMovieCollection'    !mmMovieCollection.Views.LoginView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $	��300��5   	   $	��350��6     � Title$  Login to eXpedite Link��5	       +� FocusedElement 5
      ��  �� ElementName$ txtUserName�� � 	Resources 5       �� 
TargetType �� Property �� Value��%�  '��       '�� u     'G� �     '��     �� 5   
   " ��6   .�5      ��  � ��$ 4q�5      �� 5       � ��
 �����5      5      �� 5   
   " ��6   .�5      ��  � ��$ 4q�5      5      �� 5   
   " G�6   .�5      ��  � ��$ 4q�5      5      �� 5   
   " ��6   .�5      ��  � ��$ 4q�5      �� 5       � ��$ 36��5      �� 5        � ��$ 36��5!      5"      5#      .��5%      ��  �� CornerRadius$ 10��6   
��� ���$��4q�5'         	backBrush#��[  5(        shadowWindow J� Effect# [ 5)      	 �� Padding$	 24q�5+      ._�5,   
   � 
 � ColumnDefinitions
 5-      �� 5.      $
��Auto �6#   �� 5/      $
��Auto �6#   �� 50      $��* �6#    � RowDefinitions 52      �� 53      $
��Auto �6    �� 54      $
��Auto �6    �� 55      $
��Auto �6    �� 56      $
��Auto �6    �� 57      $��* �6    .+�5:      �� $��1��6   $��0��5;      $��2��5<      $��18(�5=      $��10q�5>      $��     eXpediteLink Login��5?      �� 5@      $��1��6   $��1��5A      $��	User Name��5B      �� 5C      -   5        $��txtUserName��5F      $��2��5C      $��1��5D       � ToolTip$ Enter Your User Name��5E      5F      �� 5G      $��1��6   $��2��5H      $��Password��5I      G� 5J      -   5        $��txtPassword��5M      $��2��5J      $��2��5K      $ Enter Your Password��5L      5M      �� 5N      $��1��6   $��3��5O      $��Domain��5P      �� 5Q      -   5        $��	txtDomain��5T      $��2��5Q      $��3��5R      $' !Enter The Domain Name to Login To��5S      5T      �� 5U      $��2��6   $��4��5V      $��4q�5W      $��Right=�5X       �� Orientation$ 
Horizontal=�5Y      .+�5Z      �� -   5        $��	btnCancel��5Z      $��Canel��6*    �� IsCancel . $ Cancel��5\      5^      �� 5_      -   5        $��btnLogin��5_      $��OK��6)    �� 	IsDefault . $ Login��5a      5c      5d      5e      5f      5g      