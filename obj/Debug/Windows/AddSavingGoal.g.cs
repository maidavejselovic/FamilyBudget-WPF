﻿#pragma checksum "..\..\..\Windows\AddSavingGoal.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "606A884F2FB4DFCE4744E2C8626087554C7B9B3472DF8DF2F8E1231618C410DA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FamilyBudgetApp.Windows;
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


namespace FamilyBudgetApp.Windows {
    
    
    /// <summary>
    /// AddSavingGoal
    /// </summary>
    public partial class AddSavingGoal : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\Windows\AddSavingGoal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock goalAmount_block;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Windows\AddSavingGoal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox goalAmount_box;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Windows\AddSavingGoal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock date_block;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Windows\AddSavingGoal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker date_picker;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Windows\AddSavingGoal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock description_block;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Windows\AddSavingGoal.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox description_box;
        
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
            System.Uri resourceLocater = new System.Uri("/FamilyBudgetApp;component/windows/addsavinggoal.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\AddSavingGoal.xaml"
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
            this.goalAmount_block = ((System.Windows.Controls.TextBlock)(target));
            
            #line 15 "..\..\..\Windows\AddSavingGoal.xaml"
            this.goalAmount_block.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.goalAmount_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.goalAmount_box = ((System.Windows.Controls.TextBox)(target));
            
            #line 16 "..\..\..\Windows\AddSavingGoal.xaml"
            this.goalAmount_box.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.goalAmount_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.date_block = ((System.Windows.Controls.TextBlock)(target));
            
            #line 27 "..\..\..\Windows\AddSavingGoal.xaml"
            this.date_block.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.date_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.date_picker = ((System.Windows.Controls.DatePicker)(target));
            
            #line 29 "..\..\..\Windows\AddSavingGoal.xaml"
            this.date_picker.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.date_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.description_block = ((System.Windows.Controls.TextBlock)(target));
            
            #line 38 "..\..\..\Windows\AddSavingGoal.xaml"
            this.description_block.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.description_MouseDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.description_box = ((System.Windows.Controls.TextBox)(target));
            
            #line 39 "..\..\..\Windows\AddSavingGoal.xaml"
            this.description_box.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.description_TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 43 "..\..\..\Windows\AddSavingGoal.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

