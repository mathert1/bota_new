   M S B A M L   `   `   `  ���� B  >WpfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfTest.GUIWpfTest.GUI  3clr-namespace:WpfTest.ControlsWpfTest.Controls      WpfTest.MainScreen   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     (myclr-namespace:WpfTest.GUI     .my1clr-namespace:WpfTest.Controls     -     � 
ResizeMode$  NoResize=� � WindowStyle$
 None=� � Title$ 
MainScreen��$	��600��$	��800��
���   � � 	Resources  �� 
TargetType �� Property �� Value �� 
TargetType ��Name	 �� RadiusX
 �� RadiusY ��Name �� Triggers P� Property P� Value �� 
TargetName �� CornerRadius �� RelativeSource �� AncestorType �� Path��%8  '��       '�� �    '�� f    '�� �    �� " ��.���  � ��
 �   ���  � ��$ 80����  � ��$ 30����  � �� �� " ��.R�� #��z��#��z��}�. .+��� $ innerRectangle��
�������$��2��$	 5��$
 5��
������ ]� $!��myContentPresenterDockPanel��.+��� $ myContentPresenter��$�� Center=�$�� Center=�#��z��
������� P�  �@J�IsMouseOver . . ��� $ innerRectangle�� � ��
 ������� $ myContentPresenter�� � ��
 �   ��� " ��.���  � �� �� " ��.R��� $��Border��
���   �
�������$��1,1,1,1q�$ 6,6,6,6��._�]� .+��� $��	btnDelete��-   $��15��$��15��
���   �$��Right=�$��0q���Z� ����  �� " ��$
 Name�.���� $+��%/WpfTest;component/Content/delete.png��$��11��$��11���� $ ContentSite��$��Center=�$��Center=�$��Header��$��	12,8,12,8q����� ���� " ��.�-    P�  �@��
IsSelected . . ��� $ Border�� � ��
 ������� $ Border�� � ��
 �   �P�  �@��
IsSelected .  . ��� $ Border�� � ��
 �   ��� $ Border�� � ��
 �������  � ��$
 Left=��� " ��.���  � ��
 �   ���  � ��
 �������  � ��
 ������� " ��.���  � ��
 �   ���  � ��
 �������  � ��
 �����.���� $��1q�
�������._�� $	��800��$	��600��.+�� $	��228��$
��Left=�$��
166,61,0,0q�$	��Top=�$	��440��.+��� 
�������$��1q�._��� -   $��label3��$��Some ad goes here��$��Center=�$��Center=�
�������$��36(�� $	��213��$
��Left=�$��166,295,0,0q�$	��Top=�$	��440��.+��� 
�������$��1q�._��� -   $��label4��$��Updates/Blog here��$��36(�
�������$��Center=�$��Center=��� -   $��buddyListBox��$	��447��$
��Left=�$��3,61,0,0q�$	��Top=�$	��157��
���   �
�������
�������.@��� " ��.���  � ��
 ��������� " �� �� BasedOn# [B��.�-   � $	��453��$��Right=�$	��Top=�$��0,96,5,0q�$	��185��.+��� 
�������$��1q�._�� .+��� -   $��label9��$��Character Info��
�������$��20(�$��Pericles)�'   !WpfTest.Controls.CharacterControl  -	     Name$ xnaCharControl��$	��175��$	��175��$��5,0,0,0q��� -
   $��label10��$��Name:��
�������$��10,0,0,0q��� -   $��
nameTxtBox��$	��120��
�������
���   �$��18(��� -   $��label11��$��Rank:��
�������$��10,0,0,0q��� -   $��
rankTxtBox��
���   �
�������$��18(�$	��120���� -   $��label13��$��Wins:��
��������� -   $��
winsTxtBox��
���   �
�������$��18(�$��46���� -   $��label14��$��Losses:��
��������� -   $��lossesTxtBox��
���   �
�������$��18(�$��Right=�$��43���� -   $��image8��$��25��$/��)/WpfTest;component/Content/Icons/gold.png��$
��Fill=�$��25��$��40,0,5,0q��� -   $��
GoldTxtBlk��$��18(�
�������$��75��$��0,0,20,0q����� *
GoldCost+�� -   $��image2��$��25��$
��Fill=�$��25��$1��+/WpfTest;component/Content/Icons/attack.png��$��10,0,5,0q��� -   $��AttackTxtBlk��
�������$��18(�$��Center=�$��52������ *Attack+�� -   $��image3��$��25��$2��,/WpfTest;component/Content/Icons/defense.png��$
��Fill=�$��25��$��5,0,5,0q��� -   $��DefenseTxtBlk��
�������$��18(�$��Right=�$��Center=�$��52������ *	Defense+�� -   $��image4��$��25��$1��+/WpfTest;component/Content/Icons/health.png��$
��Fill=�$��25��$��10,0,5,0q��� -   $��HealthTxtBlk��
�������$��18(�$��Center=�$��52������ *Health+�� -   $��image5��$��25��$1��+/WpfTest;component/Content/Icons/Shield.png��$
��Fill=�$��25��$��5,0,5,0q��� -   $��ShieldTxtBlk��
�������$��18(�$��Center=�$��52������ *Shield+�� -   $��image6��$��25��$/��)/WpfTest;component/Content/Icons/Move.png��$
��Fill=�$��25��$��10,0,5,0q��� -   $��
MoveTxtBlk��
�������$��18(�$��Center=�$��52������ *Move+�� -   $��image7��$��25��$0��*/WpfTest;component/Content/Icons/gold2.png��$
��Fill=�$��25��$��5,0,5,0q��� -   $��PopularityTxtBlk��
�������$��18(�$��Center=�$��52������ *Gold+�� -    $��editBtn��$
��Edit��$��46��$	��129��$��	30,10,0,0q��� -!   $��btnQuit��$
��Quit��$��47��$
��Left=�$��1,0,0,1q�$��Bottom=�$	��106���� -"   $��label2��$��Friend List��$��26��$
��Left=�$��0,41,0,0q�$	��Top=�
��������� -#   $��addBuddyBtn��$��	Add Buddy��$��23��$
��Left=�$��	0,510,0,0q�$	��Top=�$��75���� -$   $��addBuddyPanel��$��33��$
��Left=�$��	3,475,0,0q�$	��Top=�$	��252�� �� Orientation$ 
Horizontal=�$y�Hidden=�.+��� -%   $��buddyTxtBox��$��23��$	��172��$��0,0,3,0q����	#FD000000
�������
��������� -&   $��addBtn��$	��Add��$��23��$��75���� -'   $��botsCheckBox��$
��Bots��$��19��$
��Left=�$��
274,35,0,0q�$	��Top=�$��47��
��������� -(   $��playBtn��$
��PLAY��$��43��$
��Left=�$��	453,8,0,0q�$	��Top=�$	��153���� -)   $��playersComboBox��$��23��$
��Left=�$��
327,32,0,0q�$	��Top=�$	��120��.@��� $��1 vs 1�� �� 
IsSelected . �� $��2 vs 2���� $��3 vs 3���� $��4 vs 4���� -*   $��gameTypeComboBox��$��23��$
��Left=�$��	327,5,0,0q�$	��Top=�$	��120��.@��� $��
Deathmatch�� . �� $��Free-for-all���� -+   $��label5��$��B��$��87��$��632,-14,104,0q�$��Right=�$	��Top=�
�������$��72(�$��0q�$��Pericles)��� -,   $��label6��$��A��$��Pericles)�$��72(�
�������$��87��$��672,17,47,0q�$��0q�$��Right=�$	��Top=��� --   $��label7��$��of��$��Pericles)�$��24(�
�������$��44��$��
681,1,55,0q�$��0q�$��Right=�$	��Top=��� -.   $��label8��$	��the��$��Pericles)�$��24(�
�������$��44��$��628,59,98,0q�$��0q�$��Right=�$	��Top=��� -/   $��_chatTabControl�� �� TabStripPlacement$ Bottom=�$��35.75��$
��Left=�$��	112,0,0,0q�$��Bottom=�$	��470��
�������$��1q����� ���� ���� �� -0   $��gameSearchOverlay��$y�Hidden=�.+�� 
���   � J� Opacity$	 0.9Q�$	��800��$	��600���� -1   $��label1��$��Searching for game...��$��45��$
��Left=�$��340,176,0,0q�$	��Top=�
�������$��24(� �� Left$	 -45��
 �� Top$	 -17��$	��277���� -2   $��	cancelBtn��$��Cancel��$��23��$��75��$��358,210,367,367q�