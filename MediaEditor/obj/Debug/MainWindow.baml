   M S B A M L   `   `   `  ����F  BMediaEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MediaEditorMediaEditor  5   	       MediaEditor.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:MediaEditor     5   	     � Title$  Music Metadata Editor��5   	   $��600.0��6'   $��700.0��66   
�����_� � WindowStyle$ SingleBorderWindow=�6[    � 
ResizeMode$ 	CanResize=�5	   
    � WindowStartupLocation$ CenterScreen=�6!    � Icon$, &C:\Users\Selectra\Desktop\download.jpg��6F   .��5
      � .+�5   
   �� -   5        $��FileList��5   )   $��10,25,185,10q�6   $
��Auto=�69   $
��Auto=�5       �� View 5      �� 5      .P�5      �� $��Title��6%    �� Width$	 170��64   4  .System.Windows.Automation.AutomationProperties   IsRowHeader .    HelpText$ Title of the media��5   M   	 �� DisplayMemberBinding	 5   @   �� *Title+5   M   �� 5      $��Artist��6%   $	 130��65   	 6A   �� *Artist+�� 5      $��Genre��6%   $ 53��64   	 6?   �� *Genre+�� 5      $
��Path��6%   $	 137��63   	 6?   �� *Path+5      5      �� 5   
   $
��Left=�6   $��24��6/   $��25,0,0,0q�6;   $
��Wrap=�5      $��SELECTED MEDIA��6(   
��� ���$
��Bold"�5      $��18(�6&   $	��Top=�64   $	��276��6L   �� 5    
   -   5        $��selectFolderButton��5    )   $��SELECT FOLDER��6   $
��Bold"�6C   $
��Left=�5!      $��
545,41,0,0q�6,   $	��Top=�6@   $	��106��5"      $��40��6   6)   �� 5%   
   -   5        $��updateButton��5%   "   $��Update��6   $��16(�66   $
��Left=�6D   $��524,114,0,0q�6_   $	��Top=�5&      $��75��6)   $��39��64   6@   �� 5)   
   -   5        $��titleTextBox��5)      $��Title��6&   $
��Left=�63   $��36��6N   $��524,425,0,108q�6Z   
 �� TextWrapping$

 Wrap=�5*      $��22(�6&   $��Center=�64   $	��158��6O   �� 5-   
   -   5        $��artistTextBox��5-      $��Artist��6'   $
��Left=�65   $��37��6P   $��524,339,0,193q�6\   $

 Wrap=�5.      $��22(�6&   $��Center=�64   $	��158��6O   �� 51   
   -   5        $��genreTextBox��51      $
��Left=�6&   $��36��6A   $��524,511,0,22q�6M   $

 Wrap=�52      $��22(�6&   $��Genre��64   $��Center=�6A   $	��158��6\   �� 55   
   -   5        $��clearButton��55   !   $��Clear��6   $
��Left=�64   $��607,114,0,0q�6O   $	��Top=�56      $��61��6)   $��39��64   6@   �� 58   
   -   5        $��makeBackUpButton��58   N   $��Create Backup��6   $��18(�6)   
���""��$
��Left=�6f   $��524,176,0,0q�6�   $	��Top=�59      $	��144��6)   $��35��65   5;      5<      