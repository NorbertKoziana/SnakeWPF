   M S B A M L   `   `   `  ����C  ?SnakeWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp3WpfApp3  5   	       WpfApp3.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp3     5   	   -   5          � Title$  Snake��5   	   $	��650��6   $	��800��6$   .��5
      �  � ColumnDefinitions 5   
   �� 5       � RowDefinitions 5   
   �� 5      $��70 �6   �� 5      .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   .+�5      �� -   5        $��InfoText��5      $��Score:��6(   $��Center=�66   $��10q�6Q    �� Padding$ 5q�6]   $
��Bold"�5      $��34(�6&   �� 5      -   5        $��	ScoreText��5      $��	TextBlock��6)   $��Center=�6:   $��10q�6U   $ 5q�6a   $
��Bold"�5      $��34(�6*   5      A� 5   
   -   5        $��GameGrid��5      $��1��6&   $��60, 40q�63    A� Rows$ 20��6C    A� Columns$ 20��6M   
�������$��Center=�5      $��Center=�63    � UseLayoutRounding$
 True��5      5      5      5       