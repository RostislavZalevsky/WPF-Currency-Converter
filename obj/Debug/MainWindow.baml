   M S B A M L   `   `   `  ����L  HCurrencyConverter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:CurrencyConverterCurrencyConverter  5   	   "    CurrencyConverter.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:CurrencyConverter     5   	     � Title$  Currency Converter��5   	   $	��200��6$   $	��400��61    � 
ResizeMode$ NoResize=�6=   .��5	      �� .+�5
   
   ]� .+�5      o� $	��Top=�6   .@�5      m� $
��Menu��6   .@�5      m� -   5        $��	Calculate��5      62   m� 5      -   5        $��Clear��5      6.   �� 5      m� 5      -   5        $
��Exit��5      6-   5      5      5      � 5   
   $��10q�6    � ColumnDefinitions 5      �� 5      6%   �� 5      6%   �� 5      6%   .+�5      �� $��Center=�6    � 	Resources 5       �� 
TargetType �� Property �� Value��%W   '��       �� 5      " ��6   .�5      ��  � ��$ 0,5,0,5q�63   5      5      .+�5       �� -   5        $��AmountL��5       $��20(�6)   $��Center=�67    �� AutoWordSelection . 6v   �� 5!      -   5        $��ComboBoxCurrenciesL��5!       �� DisplayMemberPath$
 base��6f   	 �� 
IsEditable	 . $
 �� IsSynchronizedWithCurrentItem$

 TrueY�6�    �� Text$ 
Loading...��6�   $��Center=�6�   $��20(�6�   ��66   ��  �� Path$ ExchangeRatesModel�6�   �� 5"      -   5        $��20(�5"      .��6<   Clear6C   5#      �� 5$      $��1��6   $	��Top=�6)   .+�5%      �� -   5        $
��Rate��5%   \   $��20(�6   $��Center=�6&   $
��Bold"�6J   .��6h   Please wait6u   �� 5&      -   5        $��Price��5&   \   $��25(�6   $��Center=�6&   $
��Bold"�6J   6k   5'      �� 5(      $��2��6   $��Center=�6)    5)      ��%W   '��       �� 5*      " ��6   .�5+      ��  � ��$ 0,5,0,5q�63   5,      5-      .+�5.      �� -	   5        $��AmountR��5.      $��20(�6)   $��Center=�67   $�� ��6[   6e   �� 5/      -
   5        $��ComboBoxCurrenciesR��5/      	 . $

 TrueY�6H   $ 
Loading...��6m   $��Center=�6   $��20(�6�   6�   �� 50      -   5        $��20(�50      .��6<   Clear6C   51      52      53      54      