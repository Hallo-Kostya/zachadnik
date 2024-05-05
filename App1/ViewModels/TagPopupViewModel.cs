﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using App1.Data;
using App1.Models;
using App1.Views;
using App1.Views.Popups;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using static App1.Models.AssignmentModel;

namespace App1.ViewModels
{
    public class TagPopupViewModel: BaseAssignmentViewModel
    {
        public Command LoadTagsCommand { get; }
        public Command SelectedItemCommand { get; }
        public INavigation Navigation { get; set; }
        private ObservableCollection<string> tagList { get; set; }
        public ObservableCollection<string> TagList 
        {
            get { return tagList; }
            set
            {
                tagList = value;
                OnPropertyChanged();
            }
        }
        private string selectedtag { get; set; }
        public string SelectedTag
        {
            get { return selectedtag; }
            set
            {
                if (selectedtag != value)
                {
                    selectedtag = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public TagPopupViewModel(INavigation navigation)
        {
            tagList = new ObservableCollection<string>();
            LoadTagsCommand = new Command(OnLoaded);
            SelectedItemCommand = new Command(OnSelected);
            Navigation = navigation;
        }
        
        private async void OnLoaded()
        {
            tagList.Clear();
            var tags = (await App.AssignmentsDB.GetItemsAsync()).Select(t=>t.Tag).ToList();
            foreach(var tag in tags)
            {
                tagList.Add(tag);
            }
        }

        private async void OnSelected()
        {
            Assignment.Tag = SelectedTag;
            await Navigation.PopPopupAsync();
        }
    }
}
