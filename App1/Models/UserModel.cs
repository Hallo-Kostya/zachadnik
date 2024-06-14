﻿using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Essentials;

namespace App1.Models
{
    public class UserModel : BaseModel
    {
        private int _exp;
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        private int _level;
        public int Level
        {
            get => _level;
            set
            {
                if (_level != value)
                {
                    _level = value;
                    OnPropertyChanged(nameof(Level));
                    SetPreferencesForLevel(_level);
                }
            }
        }
        public int Exp
        {
            get => _exp;
            set
            {
                if (_exp != value)
                {
                    _exp = value < 0 ? 0 : value;

                    if (_exp <= 100)
                        Level = 0;
                    else if (_exp <= 250)
                        Level = 1;
                    else if (_exp <= 500)
                        Level = 2;
                    else if (_exp <= 870)
                        Level = 3;
                    else
                        Level = 4;

                    OnPropertyChanged(nameof(Exp));
                }
            }
        }
        private void SetPreferencesForLevel(int level)
        {
            switch (level)
            {
                case 0:
                    Preferences.Set("SomeSetting", "ValueForLevel0");
                    break;
                case 1:
                    Preferences.Set("SomeSetting", "ValueForLevel1");
                    break;
                case 2:
                    Preferences.Set("SomeSetting", "ValueForLevel2");
                    break;
                case 3:
                    Preferences.Set("SomeSetting", "ValueForLevel3");
                    break;
                case 4:
                    Preferences.Set("SomeSetting", "ValueForLevel4");
                    break;
                default:
                    break;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}