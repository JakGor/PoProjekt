   M S B A M L   `   `   `  ����H  DRezerwacjeGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:RezerwacjeGUIRezerwacjeGUI  5   	       RezerwacjeGUI.SalaWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:RezerwacjeGUI     5   	     � Title$  Dodawanie sali��5   	   $��357.117��6    $��531.394��61   .��5	      � $
��Left=�6   $	��573��6&    � ColumnDefinitions 5
   
   �� 5      �� 5      $	��19* �6   �� 5      $	��75* �6   .+�5   
   �� -   5         ��Name$ slider_pojemnosc��5      $
��Left=�6+   $��33��6F   $��
29,230,0,0q�6R   $	��Top=�6f   $	��466��6~    � Maximum$	 250Q�6�    � LargeChange$ 25Q�6�    � SmallChange$ 5Q�6�    �� TickPlacement$ BottomRight=�6�    �� TickFrequency$ 5Q�6�    �� IsSnapToTickEnabled . $��2��6  $��1��6  �� 5   
   -   5        	 ��Name$	 label��5      $	��Typ��6   $
��Left=�6-   $��35��6H   $��	29,94,0,0q�6T   $	��Top=�6g   $	��135��6   $��2��6�   $��1��6�   $��16(�6�   �� 5   
   -   5        
 ��Name$
 comboBox_typSali��5      $
��Left=�6-   $��35��6H   $��	62,94,0,0q�6T   $	��Top=�6g   $	��196��6   $��2��6�   .@�5      �� -   5         ��Name$
 Aula��5      .��6)   Aula6/   �� 5      -   5        $ Komputerowa��5      .��60   Komputerowa6=   �� 5      -   5        $ Cwiczeniowa��5      .��60   Cwiczeniowa6=   5      �� 5   
   -   5        $	 label1��5      $��Nazwa��6    $
��Left=�60   $��38��6K   $��	29,29,0,0q�6W   $	��Top=�6j   $	��146��6�   $��2��6�   $��1��6�   $��16(�6�   �� 5   
   -   5         ��Name$ textBox_nazwaSali��5      $
��Left=�6-   $��38��6H   $��	62,29,0,0q�6T    �� TextWrapping$
 Wrap=�6g   $�� ��6{   $	��Top=�6�   $	��196��6�   $��2��6�   �� 5   
   -	   5         ��Name$ button_zatwierdz��5      $��
Zatwierdź��6+   $
��Left=�6?   $��38��6Z   $��
29,271,0,0q�6f   $	��Top=�6z   $	��191��6�   $��2��6�   $��1��6�   $��14(�6�   �� 5   
   -
   5        $ textBox��5      $��2��6#   $
��Left=�63   $��33��6N   $��
33,179,0,0q�6Z   $
 Wrap=�6n    �� TextAlignment$ Right=�6�   $	��Top=�6�   $��69��6  $��16(�6  ��6�   ��  �� ElementName$ slider_pojemnosc�� �� Path$ Value� �� UpdateSourceTrigger$ PropertyChanged=�6  �� 5   
   -   5        $	 label3��5      $��2��6    $��Ilość miejsc��64   $��1��6K   $
��Left=�6[   $��39��6v   $��
29,173,0,0q�6�   $	��Top=�6�   $	��159��6�   $��18(�6�   5      5      