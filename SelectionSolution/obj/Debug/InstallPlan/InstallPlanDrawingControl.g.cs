﻿#pragma checksum "..\..\..\InstallPlan\InstallPlanDrawingControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C5D640E973CFA3E4CFDA8412AE261BCF31D33E33"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SelectionSolution.InstallPlan;
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


namespace SelectionSolution.InstallPlan {
    
    
    /// <summary>
    /// InstallPlanDrawingControl
    /// </summary>
    public partial class InstallPlanDrawingControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\InstallPlan\InstallPlanDrawingControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas drawCanvas;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\InstallPlan\InstallPlanDrawingControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image bgImage;
        
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
            System.Uri resourceLocater = new System.Uri("/SelectionSolution;component/installplan/installplandrawingcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\InstallPlan\InstallPlanDrawingControl.xaml"
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
            this.drawCanvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 11 "..\..\..\InstallPlan\InstallPlanDrawingControl.xaml"
            this.drawCanvas.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.drawCanvas_MouseDown);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\InstallPlan\InstallPlanDrawingControl.xaml"
            this.drawCanvas.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.drawCanvas_MouseUp);
            
            #line default
            #line hidden
            
            #line 13 "..\..\..\InstallPlan\InstallPlanDrawingControl.xaml"
            this.drawCanvas.MouseMove += new System.Windows.Input.MouseEventHandler(this.drawCanvas_MouseMove);
            
            #line default
            #line hidden
            return;
            case 2:
            this.bgImage = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

