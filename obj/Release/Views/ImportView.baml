   M S B A M L   `   `   `  ���� L  HmmMovieCollection, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null5clr-namespace:mmMovieCollectionmmMovieCollection  (    "mmMovieCollection.Views.ImportView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     (ignorehttp://www.ignore.com     1myAppclr-namespace:mmMovieCollection     $
��Auto��  � SizeToContent$  Width=� � DataContext �� *ImportFolderMovie+   Locator �� Source# [   � 	Resources   TitleColHeader  MovieTitleTemplate,   &mmMovieCollection.YesNoToBoolConverter  YNToBool  PhotoListStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius  ListBoxGradient	 K� IsItemsHost
 �� Orientation  PhotoListItem �� Triggers :� RoutedEvent g� Duration �� TargetProperty	 W� To��%�  &	       &	 3     &	 ]     &	 b     &	     �� .R��� $��
File Names��$
��Bold"�$	��210���� .R��� $	��250������ *Title+  �� " ��.���  � �� �� " ��.R��� 
�������$��2q�$ 6��#���  ._��� $��Disabled=�$
��Auto=�.���� 	 . $
 
Horizontal=�$
��Left=��� " ��.���  � ��$	 150����  � ��$	 140����  �@J�Opacity$	 .75Q� :� $ Mouse.MouseEnter��[��� .���� .��W� $ 0:0:0.2D�$ 	MaxHeight�$ 85X�W� $ 0:0:0.2D�$ Opacity�$	 1.0X�:� $ Mouse.MouseLeave��[��� .���� .��W� $ 0:0:1D�$ 	MaxHeight�W� $ 0:0:0.2D�$ Opacity�.���� $ 10��
��� ���$��4q�  	backBrush#��[   shadowWindow J� Effect# [  �� Padding$ 12q�._��  � ColumnDefinitions ��  � RowDefinitions �� $��* ��� $
��Auto �.+��� $��0�� � Cursor$
 Hand��
������$��2q�$	��0,5q�$��Visible=����� *	MovieList+ �� SelectedItem �� *SelectedMovie+ �� Mode$ TwoWay=� �� SelectedIndex �� *SelectedMovieIndex+$ TwoWay=� �� View �� .P���  �� HeaderTemplate# [  �� CellTemplate# [ �� $��1��$
 
Horizontal=�$��	30,10,0,0q�.+��� -    ��Name$ btnOK��$��Folder�� 	 
FormButton#��[	 $��0,0,0,0q�$	��Top=�$��75��$��25������ *	CmdFolder+�� -   $ 	btnCancel��$��Cancel��#��[	 $��50,0,0,0q�$	��Top=�$��75��$��25������ *	CmdExit+����  �� RelativeSource �� *FindAncestor+ �� AncestorType" �