﻿#pragma checksum "..\..\..\Pages\AddExpense.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4BB93B43888F45DA3308DEB28594FD6B25F5C5EA33C13785ED401961574DBA6B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FamilyBudgetApp.Pages;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace FamilyBudgetApp.Pages {
    
    
    /// <summary>
    /// AddExpense
    /// </summary>
    public partial class AddExpense : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\Pages\AddExpense.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame NavbarFrame;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Pages\AddExpense.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock amount_block;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Pages\AddExpense.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox amount_box;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Pages\AddExpense.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock category_block;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Pages\AddExpense.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox category_comboBox;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Pages\AddExpense.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock date_block;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Pages\AddExpense.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker date_picker;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Pages\AddExpense.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock description_block;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\Pages\AddExpense.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox description_box;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\Pages\AddExpense.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid incomeDataGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FamilyBudgetApp;component/pages/addexpense.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\AddExpense.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.NavbarFrame = ((System.Windows.Controls.Frame)(target));
            return;
            case 2:
            this.amount_block = ((System.Windows.Controls.TextBlock)(target));
            
            #line 32 "..\..\..\Pages\AddExpense.xaml"
            this.amount_block.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.amount_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.amount_box = ((System.Windows.Controls.TextBox)(target));
            
            #line 33 "..\..\..\Pages\AddExpense.xaml"
            this.amount_box.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.amount_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.category_block = ((System.Windows.Controls.TextBlock)(target));
            
            #line 38 "..\..\..\Pages\AddExpense.xaml"
            this.category_block.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.category_MouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.category_comboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 39 "..\..\..\Pages\AddExpense.xaml"
            this.category_comboBox.Loaded += new System.Windows.RoutedEventHandler(this.CategoryComboBox_Loaded);
            
            #line default
            #line hidden
            return;
            case 6:
            this.date_block = ((System.Windows.Controls.TextBlock)(target));
            
            #line 44 "..\..\..\Pages\AddExpense.xaml"
            this.date_block.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.date_MouseDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.date_picker = ((System.Windows.Controls.DatePicker)(target));
            
            #line 45 "..\..\..\Pages\AddExpense.xaml"
            this.date_picker.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.date_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.description_block = ((System.Windows.Controls.TextBlock)(target));
            
            #line 50 "..\..\..\Pages\AddExpense.xaml"
            this.description_block.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.description_MouseDown);
            
            #line default
            #line hidden
            return;
            case 9:
            this.description_box = ((System.Windows.Controls.TextBox)(target));
            
            #line 51 "..\..\..\Pages\AddExpense.xaml"
            this.description_box.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.description_TextChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 57 "..\..\..\Pages\AddExpense.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.incomeDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
