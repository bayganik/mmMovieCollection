   M S B A M L   `   `   `  ����L  HmmMovieCollection, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null5clr-namespace:mmMovieCollectionmmMovieCollection  5   	   (    "mmMovieCollection.Views.ImportView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (ignorehttp://www.ignore.com     5   	   1myAppclr-namespace:mmMovieCollection     5   	   $
��Auto��5	   	     � SizeToContent$  Width=�6    � DataContext 5
      �� *ImportFolderMovie+   Locator �� Source# [   � 	Resources 5        TitleColHeader  MovieTitleTemplate,   &mmMovieCollection.YesNoToBoolConverter  YNToBool  PhotoListStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius  ListBoxGradient	 K� IsItemsHost
 �� Orientation  PhotoListItem �� Triggers :� RoutedEvent g� Duration �� TargetProperty	 W� To��%�  &	       &	 ]     &	 �     &	 �     &	     �� 5   
   .R�5      �� $��
File Names��6   $
��Bold"�6*   $	��210��6<   5      �� 5   
   .R�5      �� $	��250��60   ��6   �� *Title+60   5        5   
   6%   �� 5   
   " ��6'   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� 
�������$��2q�5      $ 6��5      #���  5      ._�5      �� $��Disabled=�5   	   $
��Auto=�5   	   .��5   "   �� 	 . $
 
Horizontal=�5!   
   $
��Left=�5"   
   5#      5$      5%      5'      5(      �� 5)   
   " ��6&   .�5*      ��  � ��$	 150��6*   �� 5+       � ��$	 140��6*   �� 5,       �@J�Opacity$	 .75Q�6(    5-      :� 5.      $ Mouse.MouseEnter��6   [�5/      �� 50      .��51      �� .��52   "   W� $ 0:0:0.2D�53   
   $ 	MaxHeight�54   
   $ 85X�55   
   W� 56   "   $ 0:0:0.2D�57   
   $ Opacity�58   
   $	 1.0X�59   
   5:      5;      5=      :� 5?      $ Mouse.MouseLeave��6   [�5@      �� 5A      .��5B      �� .��5C   "   W� $ 0:0:1D�5D   
   $ 	MaxHeight�5E   
   W� 5F   "   $ 0:0:0.2D�5G   
   $ Opacity�5H   
   5I      5J      5L      5N      5O      .��5P      �� $ 10��6   
��� ���$��4q�5S        	backBrush#��[ 5T        shadowWindow J� Effect# [ 5U       �� Padding$ 12q�5V      ._�5W   
   �  � ColumnDefinitions 5X      �� 5Y       � RowDefinitions 5[      �� 5\      $��* �6    �� 5]      $
��Auto �6    .+�5_      �� $��0��6    � Cursor$
 Hand��6%   
������$��2q�5`   3   $	��0,5q�6G   $��Visible=�5a   9   ��6   �� *	MovieList+ �� SelectedItem 5b      �� *SelectedMovie+ �� Mode$ TwoWay=� �� SelectedIndex 5c      �� *SelectedMovieIndex+$ TwoWay=� �� View 5d      �� 5e      .P�5f      ��  �� HeaderTemplate# [ 5g       �� CellTemplate# [ 5h      5i      5j      5l      �� 5m      $��1��6   $
 
Horizontal=�6'   $��	30,10,0,0q�6@   .+�5n      �� -   5         ��Name$ btnOK��5n      $��Folder��6(    	 
FormButton#��[	 6W   $��0,0,0,0q�6~   $	��Top=�6�   $��75��6�   $��25��6�   ��69   �� *	CmdFolder+6�   �� 5o      -   5        $ 	btnCancel��5o      $��Cancel��6,   #��[	 6\   $��50,0,0,0q�5p      $	��Top=�6-   $��75��6E   $��25��6P   ��5o   >   �� *	CmdExit+��5q      ��  �� RelativeSource �� *FindAncestor+ �� AncestorType" �5r      5s      5t      5u      