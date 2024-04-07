﻿using System;
using System.Collections.Generic;
using System.Text;
using App1.Models;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class AssignmentAddingViewModel: BaseAssignmentViewModel
    {
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public  AssignmentAddingViewModel() 
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged += (_,__) => SaveCommand.ChangeCanExecute();
            Assignment = new Assignment();
        }

        private async void OnSave()
        {
            var assignment = Assignment;
            await App.AssignmentsDB.AddAssignmentAsync(assignment);
            await Shell.Current.GoToAsync("..");
        }
        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
