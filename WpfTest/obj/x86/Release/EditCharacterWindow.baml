   M S B A M L   `   `   `  ���� B  >WpfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfTest.GUIWpfTest.GUI  3clr-namespace:WpfTest.ControlsWpfTest.Controls  !    WpfTest.EditCharacterWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     (myclr-namespace:WpfTest.GUI     .my1clr-namespace:WpfTest.Controls     -     � WindowStyle$
  None=� � 
ResizeMode$ NoResize=� � ShowInTaskbar .  � Title$ EditCharacterWindow��$	��600��$	��800��
���   � � 	Resources  �� 
TargetType �� Property �� Value �� 
TargetType	 ��Name
 �� RadiusX �� RadiusY ��Name �� Triggers P� Property P� Value �� 
TargetName   MainTabStyle �� CornerRadius � LayoutTransform �� Angle  SecondTabStyle  ThirdTabStyle  ListBoxStyle ��Name J� SnapsToDevicePixels��%{  '��       &	  �    &	 �    &	 |    '�� D    '�� �    &	 �    �� " ��.���  � ��
 �   ���  � ��$ 80����  � ��$ 30����  � �� �� " ��.R�� #��z��#��z��}�. .+��� $	 innerRectangle��
�������$��2��$
 5��$ 5��
������ ]� $!��myContentPresenterDockPanel��.+��� $ myContentPresenter��$�� Center=�$�� Center=�#��z��
������� P�  �@J�IsMouseOver . . ��� $ innerRectangle�� � ��
 ������� $ myContentPresenter�� � ��
 �   ��� " ��.���  � �� �� " ��.R��� $��Border��
�������
�������$��1,1,1,1q�$ 6,0,0,6��._�]� .+��� $ ContentSite��$��Center=�$��Center=�$��Header��$��5,130,5,130q� V� .~��� $	 -90Q�����  P�  �@��
IsSelected . . ��� $ Border�� � ��
 ������� $ Border�� � ��
 �   �P�  �@��
IsSelected .  . ��� $ Border�� � ��
 �   ��� $ Border�� � ��
 �������  � ��$ Stretch=��� " ��.���  � �� �� " ��.R��� $��Border��
�������
�������$��1,1,1,1q�$ 6,6,0,0��._�]� .+��� $ ContentSite��$��Center=�$��Center=�$��Header��$��	77,5,76,5q�����  P�  �@��
IsSelected . . ��� $ Border�� � ��
 ������� $ Border�� � ��
 �   �P�  �@��
IsSelected .  . ��� $ Border�� � ��
 �   ��� $ Border�� � ��
 �������  � ��$ Stretch=��� " ��.���  � �� �� " ��.R��� $��Border��
�������
�������$��1,1,1,1q�$ 6,6,0,0��._�]� .+��� $ ContentSite��$��Center=�$��Center=�$��Header��$��	52,5,51,5q�����  P�  �@��
IsSelected . . ��� $ Border�� � ��
 ������� $ Border�� � ��
 �   �P�  �@��
IsSelected .  . ��� $ Border�� � ��
 �   ��� $ Border�� � ��
 �������  � ��$ Stretch=��� " ��.���  � ��
 �   ���  � ��
 �������  � ��
 ������� " ��.���  � ��
 �   ���  � ��
 �������  � ��
 ������� " ��.R��� $ Bd�� . ._��� #��z��|�.  .���� # z .��� $��Stretch=�$��Stretch=�$	��600��$	��800�� � ColumnDefinitions �� $	��77* ��� $	��23* �.+��� -   $��tabControl1��$��Stretch=�$��Stretch=� �� TabStripPlacement$
 Left=�
���   �
�������.@��� -   $��tankTabItem��#��[  $
��Tank��.��� .+��� -   $��tabControl2��$��0��$	 Top=�$��Stretch=�$��0,1,0,0q�
���   �
�������
�������.@��� -   $��tanksHeader��#��[ $��Tanks��.��� .+��� -   $��tankListBox��#���  $��Disabled=�$��Stretch=�$��Stretch=�$��0q���Z� ���� ���� .R�� .+��� ._�&    WpfTest.GUI.UpgradeObjectControl  ���� .R�� ���� �� -   $��itemsHeader��#��[ $��Items��.��� �� -   $��shotsHeader��#��[ $��Shots��.��� �� -	   $��avatarTabItem��#��[  $��Avatar��.��� $��Stretch=�.+��� -
   $��avatarTabControl��$	 Top=�$��Stretch=�$��0,1,0,0q�$��Stretch=�
���   �
�������
�������.@��� -   $��
HeadHeader��#��[ $
��Head��.��� .+��� -   $��AvaHeadListBox��#���  $��Disabled=�$��Stretch=�$��Stretch=�$��0q���Z� ���� ���� .R�� .+��� ._�  ���� .R�� ���� �� -   $��ChestHeader��#��[ $��Chest��.��� .+��� -   $��AvaChestListBox��#���  $��Disabled=�$��Stretch=�$��0,-4,0,0q�$��Stretch=�$��0q���Z� ���� ���� .R�� .+��� ._�  ���� .R�� ���� �� -   $��ShoulderHeader��#��[ $��Shoulder��.��� .+��� -   $��AvaShoulderListBox��#���  $��Disabled=�$��Stretch=�$��0,-4,0,0q�$��Stretch=�$��0q���Z� ���� ���� .R�� .+��� ._�  ���� .R�� ���� �� -   $��
SkinHeader��#��[ $��Skins��.��� .+��� -   $��SkinListBox��#���  $��Disabled=�$��Stretch=�$��0,-4,0,0q�$��Stretch=�$��0q���Z� ���� ���� .R�� .+��� ._�  ���� .R�� ���� � $��1��.+�'   !WpfTest.Controls.CharacterControl  -     Name$ xnaCharControl��$	��175��$	��175��$��5,0,0,0q��� -   $��image8��$��25��$/��)/WpfTest;component/Content/Icons/gold.png��$
��Fill=�$��25��$��40,0,5,0q��� -   $��
GoldTxtBlk��$��18(�
�������$��75��$��0,0,20,0q����� *
GoldCost+�� -   $��image2��$��25��$
��Fill=�$��25��$1��+/WpfTest;component/Content/Icons/attack.png��$��10,0,5,0q��� -   $��AttackTxtBlk��
�������$��18(�$��Center=�$��52������ *Attack+�� -   $��image3��$��25��$2��,/WpfTest;component/Content/Icons/defense.png��$
��Fill=�$��25��$��5,0,5,0q��� -   $��DefenseTxtBlk��
�������$��18(�$��Right=�$��Center=�$��52������ *	Defense+�� -   $��image4��$��25��$1��+/WpfTest;component/Content/Icons/health.png��$
��Fill=�$��25��$��10,0,5,0q��� -   $��HealthTxtBlk��
�������$��18(�$��Center=�$��52������ *Health+�� -   $��image5��$��25��$1��+/WpfTest;component/Content/Icons/Shield.png��$
��Fill=�$��25��$��5,0,5,0q��� -   $��ShieldTxtBlk��
�������$��18(�$��Center=�$��52������ *Shield+�� -   $��image6��$��25��$/��)/WpfTest;component/Content/Icons/Move.png��$
��Fill=�$��25��$��10,0,5,0q��� -   $��
MoveTxtBlk��
�������$��18(�$��Center=�$��52������ *Move+�� -    $��image7��$��25��$0��*/WpfTest;component/Content/Icons/gold2.png��$
��Fill=�$��25��$��5,0,5,0q��� -!   $��PopularityTxtBlk��
�������$��18(�$��Center=�$��52������ *Gold+�� -"   $��shopBtn��$
��Shop��$��42��$��Center=�$��	40,90,0,0q�$��Bottom=�$	��120���� -#   $��	cancelBtn��$��Cancel��$��23��$��Center=�$��
65,100,0,0q�$��Bottom=�$��75���� -$   $��doneBtn��$
��Done��$��42��$��Center=�$��	40,10,0,0q�$��Bottom=�$	��120��