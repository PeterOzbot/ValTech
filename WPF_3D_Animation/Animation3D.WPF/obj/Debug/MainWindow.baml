   M S B A M L   `   `   `  ����J  FAnimation3D.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Animation3D.WPF.FrameworkAnimation3D.WPF.Framework  5   	        Animation3D.WPF.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   =	framework'clr-namespace:Animation3D.WPF.Framework     5   	     � Title$  
MainWindow��5   	   $	��550��6   $	��550��6)    � 	Resources 5         
ButtonTilt �� 
TargetType �� Property �� Value �� 
TargetType '�Name g� Content �� Color	 t� 	Direction5  /System.Windows.Media.Media3D.Viewport2DVisual3D
   Geometry k� 	Positions k� Normals k� TextureCoordinates k� TriangleIndices "� 	Transform �� Rotation �� Angle   Material   IsVisualHostMaterial u� Brush   Visual ��Name��%�  &	        �� 5   
   " ��6#   .�5      ��  � �� 5	      �� 5
      " ��6&   .R�5      �� ._�5      '� $ PART_Viewport��6)   x�5   "   g� 5   &    5   *   h� 5   .   .��5   2   t� $ #444444��6C   $	 0,0,-15�6S   �� 5   2   $ #BBBBBB��6?   5   /   5   '     5   &   
 5   *   k� 5   .    �    �    �    �    5    +    5#   *   �� 5$   .    5%   2   �� 5&   6   $ 0Q�6J   5(   /    5+   *   u� 5,   .    . 
 �����6m    5/   *   �� 50   .   ._�51   2   �� $  PART_FrontContentPresenter��52   5   #��z��53   5   #��z��54   5   55   /   57   '   5:      5;      5<      5>      5?      5@      .��5B      �  � ShowGridLines .  � ColumnDefinitions 5C   
   �� 5D      $��* �6   �� 5E      $��* �6   �� 5F      $��* �6    � RowDefinitions 5H   
   �� 5I      �� 5J      �� 5K      .+�5O   
   �� $��10q�6   $��0��6   $��0��6.   $��Reset��5P      ��5Q      �� *ResetCommand+�� 5R   
   $��10q�6   $��1��6   $��0��6.   $��Initialization��5S      ��5T      �� *InitializeCommand+��67   ��  �� ElementName$ BUTton���� 5V   
   -   5         ��Name$ 	pointText��5V      $��2��6%   $��0��65   ��6B   �� *Logger.Text+6a   �� 5X   
   -   5         ��Name$ BUTton��5X      $��1��6!   $��1��5Y      $��Stretch=�6   $��Stretch=�5Z      
������ #��[  5[      ��5_      � 5a      $��5q�6   .+�5c      �� $��Stretch=�5d      $��Stretch=�6B   
������ 5   /Animation3D.WPF.Framework.MouseCommandBehaviors   MouseMoveCommand 5c      �� *MouseMoveCommand+.��5e      �� -   5        $��Slika��5e       ��6-   �� *	ImagePath+5f      5g      5i      5l      5m      