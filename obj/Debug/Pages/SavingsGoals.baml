   M S B A M L   `   `   `  ����J  FFamilyBudgetApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:FamilyBudgetApp.PagesFamilyBudgetApp.Pages  5      (    "FamilyBudgetApp.Pages.SavingsGoals   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:FamilyBudgetApp.Pages     5        N� Title$  SavingsGoals��5	      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��4* �6   .+�5   
    � -   5          �Name$ NavbarFrame��5       $��0��6   6    �� 5   
   ���#dfeeed$��1��6*   .+�5      �� $��Ciljevi štednje��6   $��20(�6/   $
��Bold"�6=   
���   �$��10q�6b   �� 5      $
��Left=�6   $��50��60   $	��200��6<    �� CornerRadius$ 10��6H   ._�5      �� -   5        $��Dodajte cilj štednje��5         buttonView2#��[  68   6�   5      �� 5      -   5         ��Name$ GoalsItemsControl��5      $��10q�66   ��5      �� 5      .R�5      � 5      ��5      �� 5       .R�5"      �� 
�������$ 10��64    �� Padding$ 10q�6F   $��15q�6S   ��5#      �� 5$   "    �� 
TargetType" ��6(    �� Triggers 5%   &   �� 5&   *    �� Binding 66   �� *status+	 �� Value	 uspešno6Q   .a�5'   .   �� 
 �� Property
 � �� �� Value
 � � �6K   5(   +   �� 5)   *    66   �� *status+	 
neuspešno6Q   .a�5*   .   �� 
 � ��
 �  ��6K   5+   +   �� 5,   *    66   �� *status+	 	u toku...6Q   .a�5-   .   �� 
 � ��
 �����6K   5.   +   50   #   ._�52      �� .+�53   "   �� ���#565f66$��15(�6A   $	��8 2q�6O   Ciljni iznos: �� 54   4   $
��Bold"�6>   ��6P   �� *
goalAmount+55   #   �� 56   "   ���#565f66$��15(�6A   $	��8 2q�6O   Trenutni iznos: �� 57   6   $
��Bold"�6@   ��6R   �� *budgetAmount+58   #   �� 59   "   ���#565f66$��15(�6A   $	��8 2q�6O   Opis: �� 5:   ,   $
��Bold"�66   ��6H   �� *description+5;   #   �� 5<   "   ���#565f66$��15(�6A   $	��8 2q�6O   	Datum: �� 5=   -   $
��Bold"�67   ��6I   �� *
targetDate+ �� StringFormat$ 
dd.MM.yyyy��5>   #   �� 5@   "   ���#565f66$��15(�6A   $	��8 2q�6O   
Status: �� 5B   &   $
��Bold"�60   ��6B   �� *status+��5C   *   �� 5D   .   " ��64    5E   2   �� 5F   6    6B   �� *status+	 
neuspešno6]   .a�5G   :   �� 
 � ��
 �  ��6W   5H   7   �� 5I   6    6B   �� *status+	 uspešno6]   .a�5J   :   �� 
 � ��
 � � �6W   5K   7   �� 5L   6    6B   �� *status+	 	u toku...6]   .a�5M   :   �� 
 � �� �#fbc5586W   5N   7   5P   /   5R   '   5S   #   5V      5W      5X      5Z      5[      5\      5]      