   M S B A M L   `   `   `  ���� B  >WpfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfTest.GUIWpfTest.GUI  3clr-namespace:WpfTest.ControlsWpfTest.Controls      WpfTest.ShopWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     (myclr-namespace:WpfTest.GUI     .my1clr-namespace:WpfTest.Controls     -     � 
ResizeMode$  NoResize=� � WindowStyle$
 None=�
���   � � Title$ 
ShopWindow��$	��600��$	��800�� � 	Resources  �� 
TargetType �� Property �� Value �� 
TargetType ��Name	 �� RadiusX
 �� RadiusY ��Name �� Triggers P� Property P� Value �� 
TargetName   ThirdTabStyle �� CornerRadius  ListBoxStyle ��Name J� SnapsToDevicePixels��%�  '��       &	  �    &	 �    �� " ��.���  � ��
 �   ���  � ��$ 80����  � ��$ 30����  � �� �� " ��.R�� #��z��#��z��}�. .+��� $ innerRectangle��
�������$��2��$	 5��$
 5��
������ ]� $!��myContentPresenterDockPanel��.+��� $ myContentPresenter��$�� Center=�$�� Center=�#��z��
������� P�  �@J�IsMouseOver . . ��� $ innerRectangle�� � ��
 ������� $ myContentPresenter�� � ��
 �   ��� " ��.���  � �� �� " ��.R��� $��Border��
�������
�������$��1,1,1,1q�$ 6,6,0,0��._�]� .+��� $ ContentSite��$��Center=�$��Center=�$��Header��$��	43,5,44,5q�����  P�  �@��
IsSelected . . ��� $ Border�� � ��
 ������� $ Border�� � ��
 �   �P�  �@��
IsSelected .  . ��� $ Border�� � ��
 �   ��� $ Border�� � ��
 �������  � ��$ Stretch=��� " ��.R��� $ Bd�� . ._��� #��z��|�.  .���� # z .��� $��Stretch=�$��Stretch=�$	��600��$	��800�� � ColumnDefinitions �� $	��77* ��� $	��23* �.+��� -   $��avatarTabControl��$��0�� �� TabStripPlacement$	 Top=�$��Stretch=�$��0,1,0,0q�$��Stretch=�
���   �
�������
�������.@��� -   $��
TankHeader��#��[  $��Tanks��.��� .+��� -   $��TankListBox��#���  $��Disabled=�$��Stretch=�$��Stretch=�$��0q���Z� ���� ���� .R�� .+��� ._�#   WpfTest.GUI.ShopObjectControl  ���� .R�� ���� �� -   $��
HeadHeader��#��[  $
��Head��.��� .+��� -   $��AvaHeadListBox��#���  $��Disabled=�$��Stretch=�$��Stretch=�$��0q���Z� ���� ���� .R�� .+��� ._�  ���� .R�� ���� �� -   $��ChestHeader��#��[  $��Chest��.��� .+��� -   $��AvaChestListBox��#���  $��Disabled=�$��Stretch=�$��0,-4,0,0q�$��Stretch=�$��0q���Z� ���� ���� .R�� .+��� ._�  ���� .R�� ���� �� -	   $��ShoulderHeader��#��[  $��Shoulder��.��� .+��� -
   $��AvaShoulderListBox��#���  $��Disabled=�$��Stretch=�$��0,-4,0,0q�$��Stretch=�$��0q���Z� ���� ���� .R�� .+��� ._�  ���� .R�� ���� �� -   $��
SkinHeader��#��[  $��Skins��.��� .+��� -   $��SkinListBox��#���  $��Disabled=�$��Stretch=�$��0,-4,0,0q�$��Stretch=�$��0q���Z� ���� ���� .R�� .+��� ._�  ���� .R�� ���� � $��1��.+�'   !WpfTest.Controls.CharacterControl  -     Name$ xnaCharControl��$	��175��$	��175��$��5,0,0,0q��� -   $��image8��$��25��$/��)/WpfTest;component/Content/Icons/gold.png��$
��Fill=�$��25��$��40,0,5,0q��� -   $��
GoldTxtBlk��$��18(�
�������$��75��$��0,0,20,0q����� *
GoldCost+�� -   $��image2��$��25��$
��Fill=�$��25��$1��+/WpfTest;component/Content/Icons/attack.png��$��10,0,5,0q��� -   $��AttackTxtBlk��
�������$��18(�$��Center=�$��52������ *Attack+�� -   $��image3��$��25��$2��,/WpfTest;component/Content/Icons/defense.png��$
��Fill=�$��25��$��5,0,5,0q��� -   $��DefenseTxtBlk��
�������$��18(�$��Right=�$��Center=�$��52������ *	Defense+�� -   $��image4��$��25��$1��+/WpfTest;component/Content/Icons/health.png��$
��Fill=�$��25��$��10,0,5,0q��� -   $��HealthTxtBlk��
�������$��18(�$��Center=�$��52������ *Health+�� -   $��image5��$��25��$1��+/WpfTest;component/Content/Icons/Shield.png��$
��Fill=�$��25��$��5,0,5,0q��� -   $��ShieldTxtBlk��
�������$��18(�$��Center=�$��52������ *Shield+�� -   $��image6��$��25��$/��)/WpfTest;component/Content/Icons/Move.png��$
��Fill=�$��25��$��10,0,5,0q��� -   $��
MoveTxtBlk��
�������$��18(�$��Center=�$��52������ *Move+�� -   $��image7��$��25��$0��*/WpfTest;component/Content/Icons/gold2.png��$
��Fill=�$��25��$��5,0,5,0q��� -   $��PopularityTxtBlk��
�������$��18(�$��Center=�$��52������ *Gold+�� -   $��doneBtn��$
��Done��$��42��$��Center=�$��
40,200,0,0q�$��Bottom=�$	��120��