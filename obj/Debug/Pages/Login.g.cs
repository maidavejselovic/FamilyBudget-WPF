﻿#pragma checksum "..\..\..\Pages\Login.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1A55FDE59CD894B221F1E4652A6736D3F26988653B0E876F771D4D70DBE68890"
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
    /// Login
    /// </summary>
    public partial class Login : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 32 "..\..\..\Pages\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock email_block;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Pages\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox email_box;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Pages\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock password_block;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Pages\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox password_box;
        
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
            System.Uri resourceLocater = new System.Uri("/FamilyBudgetApp;component/pages/login.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\Login.xaml"
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
            this.email_block = ((System.Windows.Controls.TextBlock)(target));
            
            #line 32 "..\..\..\Pages\Login.xaml"
            this.email_block.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.email_block_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.email_box = ((System.Windows.Controls.TextBox)(target));
            
            #line 33 "..\..\..\Pages\Login.xaml"
            this.email_box.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.email_box_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.password_block = ((System.Windows.Controls.TextBlock)(target));
            
            #line 38 "..\..\..\Pages\Login.xaml"
            this.password_block.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.password_block_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.password_box = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 39 "..\..\..\Pages\Login.xaml"
            this.password_box.PasswordChanged += new System.Windows.RoutedEventHandler(this.password_box_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 46 "..\..\..\Pages\Login.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LoginButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 51 "..\..\..\Pages\Login.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.Hyperlink_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
