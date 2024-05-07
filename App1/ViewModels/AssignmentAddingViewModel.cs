﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class AssignmentAddingViewModel: BaseAssignmentViewModel
    {
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command LoadTagPopupCommand { get; }
        public Command FoldersPopupCommand { get; }
        public Command PriorityPopupCommand { get; }



        public ObservableCollection<string> TagList { get; }
        public INavigation Navigation { get; set; }
        public Command BackgroundClickedCommand { get; }
        private EnumPriority selectedPriority { get; set; }
        public EnumPriority SelectedPriority
        {
            get { return selectedPriority; }
            set
            {
                if (selectedPriority != value)
                {
                    selectedPriority = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool TagLoaded { get; set; } = false;
        //private string selectedtag { get; set; }
        //public string SelectedTag
        //{
        //    get { return selectedtag; }
        //    set
        //    {
        //        if (selectedtag != value)
        //        {
        //            selectedtag = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        public List<EnumPriority> Priority { get; set; }

        public AssignmentAddingViewModel(INavigation navigation)
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            TagList = new ObservableCollection<string>();
            LoadTagPopupCommand = new Command(ExecuteLoadTagPopup);
            FoldersPopupCommand = new Command(ExecuteFoldersPopup);
            PriorityPopupCommand = new Command(ExecutePriorityPopup);
            Navigation = navigation;
            Priority = new List<EnumPriority> { EnumPriority.Нет, EnumPriority.Низкий, EnumPriority.Средний, EnumPriority.Высокий };
            this.PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
            Assignment = new AssignmentModel();
            BackgroundClickedCommand = new Command(OnBackgroundClicked);

        }
        private async void OnSave()
        {
            Assignment.Priority = SelectedPriority;
            var assignment = Assignment;
            //if (TagLoaded == true)
            //{
            //    var assign = await App.AssignmentsDB.GetItemtAsync(Assignment.ID);
            //    assignment.Tag = assign.Tag;
            //}
            await App.AssignmentsDB.AddItemAsync(assignment);
            await Navigation.PopPopupAsync();
            //TagLoaded = false;
            MessagingCenter.Send(this, "PopupClosed");
        }
        private async void ExecuteLoadTagPopup()
        {
            //TagLoaded = true;
            //var assignment = Assignment;
            //await App.AssignmentsDB.AddItemAsync(assignment);
            MessagingCenter.Unsubscribe<TagModel>(this, "TagChanged");
            MessagingCenter.Subscribe<TagModel>(this, "TagChanged",
                (sender) =>
                {
                    Assignment.Tag = sender.Name;
                });
            await Navigation.PushPopupAsync(new TagPopupPage());
        }

        private async void ExecuteFoldersPopup()
        {
            await Navigation.PushPopupAsync(new FoldersPopupPage());
        }

        private async void ExecutePriorityPopup()
        {
            await Navigation.PushPopupAsync(new PriorityPopupPage());
        }

        private async void OnBackgroundClicked()
        {
            //var assign = Assignment;
            //await App.AssignmentsDB.DeleteItemAsync(assign.ID);
            await Navigation.PopPopupAsync();
            MessagingCenter.Send(this, "PopupClosed");
        }
        private async void OnCancel()
        {
            //var assign = Assignment;
            //await App.AssignmentsDB.DeleteItemAsync(assign.ID);
            await Navigation.PopPopupAsync();
            MessagingCenter.Send(this, "PopupClosed");
        }
    }
}
