﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using App1.Data;
using App1.Models;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using static App1.Models.AssignmentModel;

namespace App1.ViewModels
{
    public class AssignmentAddingViewModel: BaseAssignmentViewModel
    {
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public INavigation Navigation { get; set; }
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
        
        public List<EnumPriority> Priority { get; set; }

        public AssignmentAddingViewModel(INavigation navigation)
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            Navigation = navigation;
            Priority = new List<EnumPriority> { EnumPriority.Нет, EnumPriority.Низкий, EnumPriority.Средний, EnumPriority.Высокий };
            this.PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
            Assignment = new AssignmentModel();
        }
        private async void OnSave()
        {
            Assignment.Priority = SelectedPriority;
            var assignment = Assignment;
            await App.AssignmentsDB.AddItemAsync(assignment);
            //await Shell.Current.GoToAsync("..");
            await Navigation.PopPopupAsync();
        }
        
        
        private async void OnCancel()
        {
            await Navigation.PopPopupAsync();
        }
    }
}
