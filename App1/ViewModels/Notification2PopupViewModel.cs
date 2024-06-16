﻿using App1.Models;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class Notification2PopupViewModel : BaseAssignmentViewModel
    {
        public Command SetRepeatitionCommand { get; }
        public Command ConfirmCommand { get; }
        public INavigation Navigation { get; set; }
        private string customInterval;
        
        public string CustomInterval
        {
            get => customInterval;
            set
            {
                SetProperty(ref customInterval, value);
            }
        }
        public Notification2PopupViewModel(INavigation navigation)
        {
            Navigation = navigation;
            Assignment = new AssignmentModel();
            SetRepeatitionCommand = new Command<string>(SetRepeatition);
            ConfirmCommand = new Command(OnConfirm);
        }
        private async void SetRepeatition(string repeatTime)
        {
            Console.WriteLine("вызывается  не тот метод который должен");
            if (repeatTime == "0")
            {
                Assignment.IsRepeatable = false;
                await Navigation.PopPopupAsync();
                MessagingCenter.Send(Assignment, "RepeatitionSetted");
            }
            if (int.TryParse(repeatTime, out int days))
            {
                Assignment.IsRepeatable = true;
                Assignment.RepeatitionAdditional = days;
                await Navigation.PopPopupAsync();
                MessagingCenter.Send(Assignment, "RepeatitionSetted");
            }
        }
        private async void OnConfirm()
        {
            Console.WriteLine("Вызывается тот который должен");
            if (int.TryParse(CustomInterval, out int interval))
            {
                if (interval > 0 && interval < 367)
                {
                    Assignment.IsRepeatable = true;
                    Assignment.RepeatitionAdditional = interval;
                    await Navigation.PopPopupAsync();
                    MessagingCenter.Send(Assignment, "RepeatitionSetted");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Ошибка", "Период повторения задачи не может быть больше 366 дней", "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Введите корректное число для периода повторения", "OK");
            }
        }
    }
}
