﻿#pragma checksum "..\..\frmFarm.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2DCFBA47D3A923B63E48337A4170C8C62A1C00822FEEE606033AECCDF30690A0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FarmTacker;
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


namespace FarmTacker {
    
    
    /// <summary>
    /// frmFarm
    /// </summary>
    public partial class frmFarm : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 46 "..\..\frmFarm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DGFields;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\frmFarm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtFarmID;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\frmFarm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cboLandOwner;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\frmFarm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtAddress;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\frmFarm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCity;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\frmFarm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtState;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\frmFarm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtZipcode;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\frmFarm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chkActive;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\frmFarm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddField;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\frmFarm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEdit;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\frmFarm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\frmFarm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/FarmTacker;component/frmfarm.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\frmFarm.xaml"
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
            
            #line 8 "..\..\frmFarm.xaml"
            ((FarmTacker.frmFarm)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.DGFields = ((System.Windows.Controls.DataGrid)(target));
            
            #line 46 "..\..\frmFarm.xaml"
            this.DGFields.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.lstFields_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 46 "..\..\frmFarm.xaml"
            this.DGFields.AutoGeneratedColumns += new System.EventHandler(this.DGFields_AutoGeneratedColumns);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtFarmID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.cboLandOwner = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.txtAddress = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.txtCity = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.txtState = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.txtZipcode = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.chkActive = ((System.Windows.Controls.CheckBox)(target));
            
            #line 61 "..\..\frmFarm.xaml"
            this.chkActive.Click += new System.Windows.RoutedEventHandler(this.chkActive_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnAddField = ((System.Windows.Controls.Button)(target));
            
            #line 64 "..\..\frmFarm.xaml"
            this.btnAddField.Click += new System.Windows.RoutedEventHandler(this.btnAddField_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnEdit = ((System.Windows.Controls.Button)(target));
            
            #line 67 "..\..\frmFarm.xaml"
            this.btnEdit.Click += new System.Windows.RoutedEventHandler(this.btnEdit_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\frmFarm.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 71 "..\..\frmFarm.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
