   M S B A M L   `   `   `  ����B  >WpfTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:WpfTest.ControlsWpfTest.Controls  5   	   !clr-namespace:WpfTestWpfTest  5   	       WpfTest.GameWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -myclr-namespace:WpfTest.Controls     5   	   'localclr-namespace:WpfTest     5   	   -   5          � 
ResizeMode$  NoResize=�5       � WindowStyle$
 None=�6   
���   � � Title$ 
GameWindow��5   	   $	��600��6   $	��800��6)    � 	Resources 5
       �� 
TargetType �� Property �� Value �� 
TargetType ��Name	 �� RadiusX
 �� RadiusY ��Name �� Triggers P� Property P� Value �� 
TargetName��%  '��       �� 5      " ��6
   .�5      ��  � ��
 �   �6"   �� 5       � ��$ 80��6   �� 5       � ��$ 30��6   �� 5       � �� 5      �� 5      " ��6   .R�5      � #��z��6   #��z��6-   }�. .+�5   	   �� $ innerRectangle��6   
�������$��2��6:   $	 5��5   
   $
 5��6   
������ 6#   ]� 5   	   $!��myContentPresenterDockPanel��6   .+�5   
   �� $ myContentPresenter��6   $�� Center=�67   $�� Center=�6U   #��z��5   
   
�������6/   5   
   5   	    5      P� 5   	    �@J�IsMouseOver . . �5   
   �� $ innerRectangle��6+    � ��
 �����5      �� 5   
   $ myContentPresenter��61    � ��
 �   �6Q   5    
   5"      5$      5%      5&      .��5'      � $	��800��6   $	��600��6   .+�5(         WpfTest.AirspacePopup     FollowPlacementTarget . "   AllowOutsideScreenPlacement . ��.  � AllowsTransparency . $��Center=�5.       � PlacementTarget 5(      ��  �� ElementName$ gameControl1��   ParentWindow 5+      ��  �� RelativeSource ��  �� AncestorType" ���5/      �� $ gameControl1�� �� Path$ ActualWidth���50      �� $ gameControl1��$ ActualHeight�.��51      � .+�52      �� -   5        $��gameOverOverlay��52   -   $y�Hidden=�6   .+�53      � 
���   � J� Opacity$	 0.9Q�6.   $	��800��6<   $	��600��6H   54      �� 55      -   5        $��label1��55   m   $��
Game Over!��6   $��45��61   $
��Left=�6=   $��340,176,0,0q�6X   $	��Top=�6{   
�������$��24(�6�    �� Left$	 -45��6�   
 �� Top$	 -17��6�   $	��277��6�   �� 56      -   5        $��quitBtn��56   8   $
��Quit��6   $��23��6,   $��75��6G   $��358,210,367,367q�6R   57      �� 58      -   5        $��btnQuit��58   O   $
��Quit��6   $��23��6(   $
��Left=�64   $��Bottom=�6^   $��75��6y    K� ZIndex$ 99999��6�   59      5:      "   WpfTest.Controls.GameControl  5;      -   5          Name$ gameControl1��5;   X   $
��Left=�6   $	��800��6.   $	��600��6:   $��0,0,0,0q�6G   $	��Top=�6n   $ 0��6�   5<      5=      