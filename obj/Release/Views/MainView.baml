   M S B A M L   `   `   `  ���� L  HmmMovieCollection, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null5clr-namespace:mmMovieCollectionmmMovieCollection  &     mmMovieCollection.Views.MainView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1myAppclr-namespace:mmMovieCollection     $
��Auto=�  � DataContext  �� *Main+   Locator �� Source# [   � 	Resources   TitleColHeader  MovieTitleTemplate,   &mmMovieCollection.YesNoToBoolConverter  YNToBool  PhotoListStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius  ListBoxGradient K� IsItemsHost	 �� Orientation  PhotoListItem
 �� Triggers :� RoutedEvent g� Duration �� TargetProperty	 W� To��%|  &	       &	 %     &	 O     &	 T     &	 �     �� .R��� $��Titles��$
��Bold"��� .R��� $	��250������ *Title+  �� " ��.���  � �� �� " ��.R��� 
�������$��2q�$ 6��#���  ._��� $��Disabled=�$
��Auto=�.����  . $	 
Horizontal=�$
��Left=��� " ��.���  � ��$	 150����  � ��$	 140����  �@J�Opacity$	 .75Q�
 :� $ Mouse.MouseEnter��[��� .���� .��W� $ 0:0:0.2D�$ 	MaxHeight�$ 85X�W� $ 0:0:0.2D�$ Opacity�$	 1.0X�:� $ Mouse.MouseLeave��[��� .���� .��W� $ 0:0:1D�$ 	MaxHeight�W� $ 0:0:0.2D�$ Opacity�.���� $ 10��
������$��4q�  	backBrush#��[   shadowWindow J� Effect# [ $��	4,4,20,15q� �� Padding$ 10q�._�� ����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$
 Gray�� � Offset$ 0Q�� $ White��$ 1Q� � ColumnDefinitions �� $	��250 ��� ��  � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� -   $��
topButtons��$ 10��$��0��$��0��$��3��
������$��4q�
�������# [ $ 15q�._�� .+�]� $��0��.+��� -   $��btnExit��$��	Save/Exit��$��80��$��29��$	��Top=� 	 RoundedButton#��[	  
 mainDropShadow# [
 $
��Left=����	#FF060505���� *	CmdExit+�� -   $��	btnAddNew��$��	New Movie��$��80��$��29��$��10,0,0,0q�$	��Top=�#��[	 # [
 $
��Left=����	#FF060505���� *CmdNew+�� -   $��
btnImport1��$��Search��$��50,0,0,0q�$��50��$��29��$	��Top=�#��[	 # [
 $
��Left=����	#FF060505���� *	CmdSearch+�� $��1��$��10,0,0,0q�$��23��$
��Left=�$��Center=�$	��100������ *SearchFields+ �� Mode$ TwoWay=� �� SelectedItem �� *SelectedSearch+$ TwoWay=� �� SelectedValue �� *SelectedSearch+$ TwoWay=��� -   $��	txtSearch�����	#FF060505$��10,0,0,0q�$��23��$	��200��$
��Left=����� *
SearchText+�� $��1��$��1��$��4��#���  $��0,5,0,0q� � Cursor$
 Hand��
�������$��1q�$	��Top=�#���   �� SelectedIndex$ 0�� �� *SelectedPhoto+$ TwoWay=����� *
PhotoFiles+���� .R��� ���� *Image+���� .R��  .  � Orientation$ 
Horizontal=�]� $��0��$��1��$��5��.+��� $��0��$��1��$
 Hand��$	��top=�
������$��2q�$	��0,5q�$��Visible=�{��� *ListEnabled+���� *	MovieList+ �� *SelectedMovie+$ TwoWay=� �� *SelectedMovieIndex+$ TwoWay=� �� View �� .P���  �� HeaderTemplate# [  �� CellTemplate# [ ]� $��2��$��1��$��4��$	��top=�.+�� -     �Name$  grdData�� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��IMDB Id:��$��0��$��2��$
��Left=�$��Center=��� $��1��$��2��$��23��$
��Left=�$��Center=�$	��100��{��� *	IdEnabled+���� *SelectedMovie.ID+$ TwoWay=��� $��1��$��2��$��Center=�$��Movie��$��	110,0,0,0q�! d� 	IsChecked! �� *SelectedMovie.MultipleVideo+" �� 	Converter#" [ �� $��Title:��$��0��$��3��$
��Left=�$��Center=��� $��1��$��3��$��23��$
��Left=�$��Center=�$	��300��$
��Bold"����� *SelectedMovie.Title+$ TwoWay=��� $��Year:��$��0��$��4��$
��Left=�$��Center=��� $��1��$��4��$��23��$
��Left=�$��Center=�$��80������ *SelectedMovie.Year+$ TwoWay=��� $��Rating:��$��1��$��4��$
��Left=�$��Center=�$��	100,0,0,0q��� $��1��$��4��$��23��$
��Left=�$��Center=�$��	150,0,0,0q�$	��100��$
��Bold"����� *SelectedMovie.Rating+$ TwoWay=��� $��Summary:��$��0��$��5���� $��1��$��5��$
��Left=�$	��100��$	��Top=�$	��300��"# �� VerticalScrollBarVisibility$
# Auto=�$ �� TextWrapping$
$ Wrap=����� *SelectedMovie.Summary+$ TwoWay=��� $��Cast:��$��0��$��6��$
��Left=�$	��Top=��� $��1��$��6��$
��Left=�$	��100��$	��Top=�$	��300��$
# Auto=�$
$ Wrap=����� *SelectedMovie.Cast+$ TwoWay=��� $��	Run Time:��$��0��$��7��$
��Left=�$��Center=��� $��1��$��7��$��23��$
��Left=�$��Center=�$��80������ *SelectedMovie.RunTime+$ TwoWay=��� $��Gener:��$��1��$��7��$
��Left=�$��Center=�$��80,0,0,0q��� $��1��$��7��$��23��$
��Left=�$��Center=�$��	120,0,0,0q�$	��160������ *SelectedMovie.Gener+$ TwoWay=��� $��Video Playing:��$��0��$��8��$
��Left=�$��Center=��� $��1��$��8��$��23��$
��Left=�$��Center=�$	��300������ *SelectedMovie.CurrentVideo+$ TwoWay=��� -   % ��Name$% 
btnPlayVid��$
��Play��$��1��$��8��#��[	 $	��Top=�$��40��$��25��$��	310,0,0,0q����� *
CmdPlayVid+{��� *SaveEnabled+�� $��Video Files:��$��0��$��9��$
��Left=�$��Center=��� $��1��$��9��$��23��$
��Left=�$��Center=�& �� DisplayMemberPath$& FileName��$	��300������ *
VideoFiles+$ TwoWay=� �� *SelectedVideo+$ TwoWay=� �� *VideoFiles.VideoFile+$ TwoWay=� �� *
VideoIndex+$ TwoWay=��� -   $% 	btnAddVid��$	��Add��$��1��$��9��  
FormButton#��[ $	��Top=�$��40��$��25��$��	310,0,0,0q����� *	CmdAddVid+{��� *SaveEnabled+�� $��1��$��11��$	 
Horizontal=�$��0,10,0,0q�.+��� -	   $% btnOK��$
��Save��#��[ $��0,0,0,0q�$	��Top=�$��60��$��25��{��� *SaveEnabled+���� *	CmdSave+�� -
   $% btnDel��$��Delete��#��[ $��10,0,0,0q�$	��Top=�$��60��$��25��{��� *SaveEnabled+���� *	CmdDelete+�� -   $% 	btnCancel��$��Cancel��#��[ $��10,0,0,0q�$	��Top=�$��60��$��25������ *	CmdCancel+�� $��2��$��2��$	��400��$	��300��$��10,0,0,0q����� *PosterImage+