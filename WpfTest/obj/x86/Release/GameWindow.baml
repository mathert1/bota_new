   M S B A M L   `   `   `  ���� B  >WpfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:WpfTest.ControlsWpfTest.Controls  !clr-namespace:WpfTestWpfTest      WpfTest.GameWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -myclr-namespace:WpfTest.Controls     'localclr-namespace:WpfTest     -     � 
ResizeMode$  NoResize=� � WindowStyle$
 None=�
���   � � Title$ 
GameWindow��$	��600��$	��800�� � 	Resources  �� 
TargetType �� Property �� Value �� 
TargetType ��Name	 �� RadiusX
 �� RadiusY ��Name �� Triggers P� Property P� Value �� 
TargetName��%�  '��       �� " ��.���  � ��
 �   ���  � ��$ 80����  � ��$ 30����  � �� �� " ��.R�� #��z��#��z��}�. .+��� $ innerRectangle��
�������$��2��$	 5��$
 5��
������ ]� $!��myContentPresenterDockPanel��.+��� $ myContentPresenter��$�� Center=�$�� Center=�#��z��
������� P�  �@J�IsMouseOver . . ��� $ innerRectangle�� � ��
 ������� $ myContentPresenter�� � ��
 �   �.��� $	��800��$	��600��.+�   WpfTest.AirspacePopup     FollowPlacementTarget . "   AllowOutsideScreenPlacement . ��.  � AllowsTransparency . $��Center=� � PlacementTarget ��  �� ElementName$ gameControl1��   ParentWindow ��  �� RelativeSource ��  �� AncestorType" ����� $ gameControl1�� �� Path$ ActualWidth����� $ gameControl1��$ ActualHeight�.��� .+��� -   $��gameOverOverlay��$y�Hidden=�.+�� 
���   � J� Opacity$	 0.9Q�$	��800��$	��600���� -   $��label1��$��
Game Over!��$��45��$
��Left=�$��340,176,0,0q�$	��Top=�
�������$��24(� �� Left$	 -45��
 �� Top$	 -17��$	��277���� -   $��quitBtn��$
��Quit��$��23��$��75��$��358,210,367,367q��� -   $��btnQuit��$
��Quit��$��23��$
��Left=�$��Bottom=�$��75�� K� ZIndex$ 99999��"   WpfTest.Controls.GameControl  -     Name$ gameControl1��$
��Left=�$	��800��$	��600��$��0,0,0,0q�$	��Top=�$ 0��