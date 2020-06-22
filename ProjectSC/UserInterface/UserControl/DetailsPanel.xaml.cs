﻿using ProjectSC.Classes.Functions.Common;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectSC.UserControls.Custom
{
    public partial class DetailsPanel : UserControl
    {
        public DetailsPanel(MyDayUSC myDayUSC)
        {
            InitializeComponent();

            myDay = myDayUSC;
        }

        public DetailsPanel(MyDayUSC myDayUSC, ItemBar itembar)
        {
            InitializeComponent();

            myDay = myDayUSC;
            itemBar = itembar;
        }

        #region Properties
        public bool IsNew { get; set; }

        public int Id { get; set; }


        public string Title { get; set; }
        public string Description { get; set; }


        public bool IsCompleted { get; set; }
        public bool IsImportant { get; set; }


        public bool IsReminderOn { get; set; }
        public bool IsAdvRemider { get; set; }
        public int NotifyType { get; set; }


        public DateTime BeginDateTime { get; set; }
        public DateTime EndDateTime { get; set; }


        public DateTime CreationDateTime { get; set; }
        #endregion


        MyDayUSC myDay = new MyDayUSC();
        ItemBar itemBar = new ItemBar();

        private void DarkGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsNew)
            {
                RemoveButton.IsEnabled = false;
                RemoveButton.Visibility = Visibility.Hidden;

                BeginDatePicker.Text = $"{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Year}";
                BeginTimePicker.Text = $"{string.Format("{0:h:mm tt}", DateTime.Now)}";

                EndDatePicker.Text = $"{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Year}";
                EndTimePicker.Text = $"{string.Format("{0:h:mm tt}", DateTime.Now)}";
            }
            else
            {
                textBoxTitle.Text = Title;
                textBoxDescription.Text = Description;


                ReminderToggle.IsChecked = IsReminderOn;
                CheckToggleState();

                AdvReminderToggle.IsChecked = IsReminderOn;
                CheckAdvToggleState();

                if (IsReminderOn)
                {

                    if (IsAdvRemider)
                    {
                        BeginDatePicker.Text = $"{BeginDateTime.Month}/{BeginDateTime.Day}/{BeginDateTime.Year}";
                        BeginTimePicker.Text = $"{string.Format("{0:h:mm tt}", BeginDateTime)}";

                        EndDatePicker.Text = $"{EndDateTime.Month}/{EndDateTime.Day}/{EndDateTime.Year}";
                        EndTimePicker.Text = $"{string.Format("{0:h:mm tt}", EndDateTime)}";
                    }
                    else
                    {
                        BeginDatePicker.Text = $"{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Year}";
                        BeginTimePicker.Text = $"{string.Format("{0:h:mm tt}", DateTime.Now)}";

                        EndDatePicker.Text = $"{DateTime.Now.Month}/{DateTime.Now.Day}/{DateTime.Now.Year}";
                        EndTimePicker.Text = $"{string.Format("{0:h:mm tt}", DateTime.Now)}";
                    }
                }
            }
        }

        private void RetunButton_Click(object sender, RoutedEventArgs e)
        {
            myDay.CloseDetailsPanel();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool isReminderOn;
            if (ReminderToggle.IsChecked == true)
            {
                isReminderOn = true;
            }
            else
            {
                isReminderOn = false;
            }

            if (IsNew)
            {
                DataAccess.AddNew(textBoxTitle.Text, textBoxDescription.Text, isReminderOn, Convert.ToDateTime(BeginDatePicker.Text + " " + BeginTimePicker.Text), Convert.ToDateTime(EndDatePicker.Text + " " + EndTimePicker.Text), DateTime.Now, myDay.Inventory);
            }
            else
            {
                DataAccess.Update(Id, textBoxTitle.Text, textBoxDescription.Text, isReminderOn, Convert.ToDateTime(BeginDatePicker.Text + " " + BeginTimePicker.Text), Convert.ToDateTime(EndDatePicker.Text + " " + EndTimePicker.Text), myDay.Inventory);

                itemBar.Update(textBoxTitle.Text);
            }

            DetailsGrid.Children.Add(SnackbarControl.OpenSnackBar("Saved"));
        }


        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            DataAccess.RemoveAt(Id, myDay.Inventory);

            myDay.RemoveItemBar(Id);

            myDay.CloseDetailsPanel();
        }

        private void ReminderToggle_Click(object sender, RoutedEventArgs e)
        {
            CheckToggleState();
        }
        private void AdvReminderToggle_Click(object sender, RoutedEventArgs e)
        {
            CheckAdvToggleState();
        }

        private void CheckToggleState()
        {
            if (ReminderToggle.IsChecked == true)
            {
                OutterExpender.IsEnabled = true;
                OutterExpender.Foreground = Brushes.Black;
                OutterExpender.IsExpanded = true;
            }
            else
            {
                OutterExpender.IsEnabled = false;
                OutterExpender.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#66000000"));
                OutterExpender.IsExpanded = false;
            }
        }
        private void CheckAdvToggleState()
        {
            if (AdvReminderToggle.IsChecked == true)
            {
                AdvReminderExpander.IsEnabled = true;
                AdvReminderExpander.Foreground = Brushes.Black;
                AdvReminderExpander.IsExpanded = true;
            }
            else
            {
                AdvReminderExpander.IsEnabled = false;
                AdvReminderExpander.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#66000000"));
                AdvReminderExpander.IsExpanded = false;
            }
        }

        private void DarkGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed || Mouse.XButton1 == MouseButtonState.Pressed)
            {
                myDay.CloseDetailsPanel();
            }
        }
    }
}
