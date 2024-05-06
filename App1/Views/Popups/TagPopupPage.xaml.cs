﻿using App1.Models;
using App1.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TagPopupPage : PopupPage
    {
        public AssignmentModel Assignment { get; set; }
        public TagPopupPage()
        {
            InitializeComponent();
            BindingContext = new TagPopupViewModel(Navigation);
        }
        public TagPopupPage(AssignmentModel assign)
        {
            InitializeComponent();
            BindingContext = new TagPopupViewModel(Navigation);
            if (assign != null)
            {
                ((TagPopupViewModel)BindingContext).Assignment = assign;
            }
        }
    }
}