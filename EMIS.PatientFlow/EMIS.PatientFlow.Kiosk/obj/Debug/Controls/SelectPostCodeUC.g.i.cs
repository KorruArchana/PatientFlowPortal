﻿#pragma checksum "..\..\..\Controls\SelectPostCodeUC.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "60A353A27885A757FBDCA492DC49F84D4D130672"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using EMIS.PatientFlow.Kiosk.Controls;
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
using System.Windows.Interactivity;
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


namespace EMIS.PatientFlow.Kiosk.Controls {
    
    
    /// <summary>
    /// SelectPostCodeUC
    /// </summary>
    public partial class SelectPostCodeUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 58 "..\..\..\Controls\SelectPostCodeUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel PostCodeStkPnl;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\Controls\SelectPostCodeUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid PostcodeGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/EMIS.PatientFlow.Kiosk;component/controls/selectpostcodeuc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Controls\SelectPostCodeUC.xaml"
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
            this.PostCodeStkPnl = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            this.PostcodeGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            
            #line 70 "..\..\..\Controls\SelectPostCodeUC.xaml"
            ((System.Windows.Controls.ScrollViewer)(target)).PreviewMouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.UIElement_OnPreviewMouseRightButtonDown);
            
            #line default
            #line hidden
            
            #line 71 "..\..\..\Controls\SelectPostCodeUC.xaml"
            ((System.Windows.Controls.ScrollViewer)(target)).PreviewMouseRightButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.UIElement_OnPreviewMouseRightButtonDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

