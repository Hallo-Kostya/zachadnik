﻿using App1.Models;
using App1.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class AppShellViewModel: BaseAssignmentViewModel
    {
        public Command ToArchiveCommand { get; }
        public INavigation Navigation { get; set; }
        public Command AddFolderCommand { get; }
        public Command SelectedCommand { get; }
        private ListModel selectedFolder { get; set; }
        public ListModel SelectedFolder
        {
            get { return selectedFolder; }
            set
            {
                if (selectedFolder != value)
                {
                    selectedFolder = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<ListModel> foldersList;
        public ObservableCollection<ListModel> FoldersList
        {
            get => foldersList;
            set => SetProperty(ref foldersList, value);
        }
        public AppShellViewModel(INavigation navigation) 
        {
            FoldersList = new ObservableCollection<ListModel>();
            Task.Run(async () => await OnLoaded());
            AddFolderCommand = new Command(AddFolder);
            Navigation = navigation;
            SelectedCommand = new Command(OnSelected);
            ToArchiveCommand = new Command(OnArchive);
        }
        private async void OnArchive(object obj)
        {
            await Shell.Current.GoToAsync(nameof(ArchivePage));
            Shell.Current.FlyoutIsPresented = false;
        }
        async Task OnLoaded()
        {
            var folders = (await App.AssignmentsDB.GetListsAsync()).ToList();
            FoldersList = new ObservableCollection<ListModel>(folders);
        }
        private async void AddFolder(object obj)
        {
            MessagingCenter.Unsubscribe<FolderAddingViewModel>(this, "FolderClosed");
            MessagingCenter.Subscribe<FolderAddingViewModel>(this, "FolderClosed", async (sender) => await OnLoaded());
            await Shell.Current.GoToAsync(nameof(FolderAddingPage));
            Shell.Current.FlyoutIsPresented = false;
        }
        private async void OnSelected()
        {
            var list = selectedFolder;
            await Navigation.PushAsync(new FolderPage(list));
            Shell.Current.FlyoutIsPresented = false;
        }
    }
}