   M S B A M L   `   `   `  ����`  \Lightweight car register WPF Core App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:Lightweight_car_register_WPF_Core_App%Lightweight_car_register_WPF_Core_App  5   	   5    /Lightweight_car_register_WPF_Core_App.CarWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Elocal3clr-namespace:Lightweight_car_register_WPF_Core_App     5   	     � Title$  
Car Window��5   	   $	��309��6   $	��453��6)      WindowStyle#���   65   .��5	      �  � RowDefinitions 5
   
   �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      .+�5   
   �� $��Model:��6   $��Center=�6"   $��Center=�6?     TextBlockStyle#���  6Z   6�   �� 5   
   $��Brand:��6   $��1��6"   $��Center=�6/   $��Center=�6L   #���  6h   6�   �� 5   
   $��Owner:��6   $��2��6"   $��Center=�6/   $��Center=�6L   #���  6h   6�   �� 5   
   $��1��6)   $��Stretch=�69   $��Center=�6W   $��	50,0,50,0q�6r   ��6   �� *Model+6r   �� 5   
   $��1��6)   $��1��69   $��Stretch=�6F   $��Center=�6d   $��	50,0,50,0q�6   ��6   �� *Brand+6   �� 5   
   $��1��6)   $��2��69   $��Stretch=�6F   $��Center=�6d   $��	50,0,50,0q�6   ��6   �� *Owner+6   �� 5   
   $��Center=�6    �� Orientation$ 
Horizontal=�62   $��3��6K   $��1��6X   .+�5      �� -   5         �� 	IsDefault$
 True��5      $��60��6;   $��5q�6I   $	��100��6T     ButtonStyle#���  6`   .��6�   OK6�   �� 5       �� IsCancel$
 True��6   $��60��6%   $��5q�63   $	��100��6>   #���  6J   .��6p   Cancel6x   5      5       5!      