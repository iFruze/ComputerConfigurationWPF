﻿#pragma checksum "..\..\ChangeComponent.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "CFBE43573CE65DE52CB9F9E1B5BDEF1AC696D6380DB90384976C3AFCF520521D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ComputerConfig;
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


namespace ComputerConfig {
    
    
    /// <summary>
    /// ChangeComponent
    /// </summary>
    public partial class ChangeComponent : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\ChangeComponent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ComponentImage;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\ChangeComponent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameBox;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\ChangeComponent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DescBox;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\ChangeComponent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CostText;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\ChangeComponent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox AvailBox;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\ChangeComponent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TypeComponentBox;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\ChangeComponent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CatComponentBox;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\ChangeComponent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddImageBtn;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\ChangeComponent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddComponentBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/ComputerConfig;component/changecomponent.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ChangeComponent.xaml"
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
            this.ComponentImage = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.NameBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.DescBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.CostText = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.AvailBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.TypeComponentBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.CatComponentBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.AddImageBtn = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\ChangeComponent.xaml"
            this.AddImageBtn.Click += new System.Windows.RoutedEventHandler(this.AddImg_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.AddComponentBtn = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\ChangeComponent.xaml"
            this.AddComponentBtn.Click += new System.Windows.RoutedEventHandler(this.AddComponent_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

