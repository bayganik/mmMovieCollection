   M S B A M L   `   `   `  ���� L  HmmMovieCollection, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null5clr-namespace:mmMovieCollectionmmMovieCollection  &     mmMovieCollection.Views.NewMovie   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1myAppclr-namespace:mmMovieCollection       � DataContext  �� *
NewMovie+   Locator �� Source# [   � 	Resources 1   +mmMovieCollection.UriToBitmapImageConverter  UriToImageConverter  TitleColHeader  MovieTitleTemplate,   &mmMovieCollection.YesNoToBoolConverter  YNToBool  PhotoListStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius  ListBoxGradient K� IsItemsHost	 �� Orientation  PhotoListItem
 �� Triggers :� RoutedEvent g� Duration �� TargetProperty	 W� To��%�  &	       &	      &	 *     &	 T     &	 Y     &	 �       �� .R��� $��Titles��$
��Bold"��� .R��� $	��300������ *Title+  �� " ��.���  � �� �� " ��.R��� 
�������$��2q�$ 6��#���  ._��� $��Disabled=�$
��Auto=�.����  . $	 
Horizontal=�$
��Left=��� " ��.���  � ��$	 150����  � ��$	 140����  �@J�Opacity$	 .75Q�
 :� $ Mouse.MouseEnter��[��� .���� .��W� $ 0:0:0.2D�$ 	MaxHeight�$ 85X�W� $ 0:0:0.2D�$ Opacity�$	 1.0X�:� $ Mouse.MouseLeave��[��� .���� .��W� $ 0:0:1D�$ 	MaxHeight�W� $ 0:0:0.2D�$ Opacity�.���� $ 10��
���   �$��4q�  	backBrush#��[ $��	4,4,20,15q� �� Padding$ 10q�._�� ����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$
 Gray�� � Offset$ 0Q�� $ White��$ 1Q� � ColumnDefinitions �� $	��300 ��� $
��Auto ���  � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� -   $��
topButtons��$ 10��$��0��$��0��$��3��
���   �$��4q�
�������$ 15q�._�� .+�]� $��0��.+��� -   $��btnExit��$
��Exit��$��80��$��29��$	��Top=� 	 RoundedButton#��[	  
 mainDropShadow J� Effect# [
 $
��Left=����	#FF060505{��� *IsExitEnable+���� *	CmdExit+����  �� RelativeSource �� *FindAncestor+ �� AncestorType" ��� -   $��	btnImport��$��Import Folder��$��20,0,0,0q�$��85��$��29��$	��Top=�#��[	 # [
 $
��Left=����	#FF060505���� *	CmdImport+����  �� *FindAncestor+" ��� -   $��	btnSearch��$��Search��$��20,0,0,0q�$��80��$��29��$	��Top=�#��[	 # [
 $
��Left=����	#FF060505���� *	CmdSearch+��  �� AcceptsReturn .  ���	#FF060505$��10,0,0,0q�$��30��$	��200������ *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=�.�� J� InputBindings �� 
 �� Key$ Enter�� �� Command �� *CmdSearchText+ �� CommandParameter ��  �� " ��.���� $��IMDB internet Search��$��10,0,0,0q�$��Center=�$��Tahoma)�$��16(����	#FFF50B40
���   �]� $��0��$��1��$��5��$��Visible=�.+��� $��0��$��1�� � Cursor$
 Hand��$	��top=�
���   �$��2q�$	��0,5q�$��Visible=�{��� *ListEnabled+���� *	MovieList+  �� SelectedItem  �� *SelectedIMDB+! �� Mode$! TwoWay=�" �� View" �� .P��� # �� HeaderTemplate## [ $ �� CellTemplate#$ [ ]� $��1��$��1��$��4��$	��top=�.+�� -   % �Name$% grdData�� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � �� $
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
��Left=�$��Center=�$	��100��{��� *	IdEnabled+���� *SelectedMovie.ID+$! TwoWay=��� $��1��$��2��$��Center=�$��Movie��$��	110,0,0,0q�& d� 	IsChecked& �� *SelectedMovie.MultipleVideo+' �� 	Converter#' [ �� $��Title:��$��0��$��3��$
��Left=�$��Center=��� $��1��$��3��$��23��$
��Left=�$��Center=�$	��300��$
��Bold"����� *SelectedMovie.Title+$! TwoWay=��� $��Year:��$��0��$��4��$
��Left=�$��Center=��� $��1��$��4��$��23��$
��Left=�$��Center=�$��80������ *SelectedMovie.Year+$! TwoWay=��� $��Rating:��$��1��$��4��$
��Left=�$��Center=�$��	100,0,0,0q��� $��1��$��4��$��23��$
��Left=�$��Center=�$��	150,0,0,0q�$	��100������ *SelectedMovie.Rating+$! TwoWay=��� $��Summary:��$��0��$��5���� $��1��$��5��$
��Left=�$	��100��$	��Top=�$	��300��"( �� VerticalScrollBarVisibility$
( Auto=�) �� TextWrapping$
) Wrap=����� *SelectedMovie.Summary+$! TwoWay=��� $��Cast:��$��0��$��6��$
��Left=�$	��Top=��� $��1��$��6��$
��Left=�$	��100��$	��Top=�$	��300��$
( Auto=�$
) Wrap=����� *SelectedMovie.Cast+$! TwoWay=��� $��	Run Time:��$��0��$��7��$
��Left=�$��Center=��� $��1��$��7��$��23��$
��Left=�$��Center=�$��80������ *SelectedMovie.RunTime+$! TwoWay=��� $��Gener:��$��1��$��7��$
��Left=�$��Center=�$��	100,0,0,0q��� $��1��$��7��$��23��$
��Left=�$��Center=�$��	150,0,0,0q�$	��100������ *SelectedMovie.Gener+$! TwoWay=��� $��1��$��11��$	 
Horizontal=�$��0,10,0,0q�.+��� -   * ��Name$* btnOK��$��	Add Movie��  
FormButton#��[ $��0,0,0,0q�$	��Top=�$��80��$��30������ *	CmdSave+�� -   $* 	btnCancel��$��Cancel��#��[ $��	100,0,0,0q�$	��Top=�$��80��$��30������ *	CmdCancel+�� $��2��$��2��$��10,0,0,0q����� *SelectedPhoto+