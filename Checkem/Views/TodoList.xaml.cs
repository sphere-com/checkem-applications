﻿using Checkem.CustomComponents;
using Checkem.Models;
using Sphere.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Checkem.Views
{
    public partial class TodoList : UserControl, INotifyPropertyChanged
    {
        public TodoList()
        {
            DataContext = this;

            InitializeComponent();
        }


        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


        #region Variable
        List<ToDoItem> currentInventory = TodoManager.Filter(FilterMethods.None);
        #endregion


        #region Property
        public string ItemCount
        {
            get
            {
                if (currentInventory.Count > 0)
                {
                    if (ToDoListItemCounterTextBlock.Margin == new Thickness(0))
                    {
                        ToDoListItemCounterTextBlock.Margin = new Thickness(0, 0, 5, 0);
                    }

                    return currentInventory.Count.ToString();
                }
                else
                {
                    ToDoListItemCounterTextBlock.Margin = new Thickness(0);

                    return string.Empty;
                }
            }
            set
            {
                OnPropertyChanged();
            }
        }

        public string ItemCounterString
        {
            get
            {
                if (currentInventory.Count == 1)
                {
                    return "Task";
                }
                else if (currentInventory.Count > 1)
                {
                    return "Tasks";
                }
                else
                {
                    return "Nothing in the list";
                }
            }
        }
        #endregion



        private void TodoList_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTodoList();

            OnPropertyChanged();
        }


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private void ItemCountChanged()
        {
            ItemCount = currentInventory.Count.ToString();
        }


        private void LoadTodoList()
        {
            foreach (var item in currentInventory)
            {
                Itembar itembar = new Itembar(item);
                itembar.Click += new EventHandler(this.Itembar_Click);
                itembar.Remove += new EventHandler(this.Itembar_Remove);

                TodoItemsStackPanel.Children.Add(itembar);
            }
        }

        private void Itembar_Click(object sender, EventArgs e)
        {
            Itembar itembar = sender as Itembar;

            DetailsPanel detailsPanel = new DetailsPanel(itembar);
            detailsPanel.Close += new EventHandler(this.DetailsPanel_Close);

            DataGrid.Children.Add(detailsPanel);
        }

        private void Itembar_Remove(object sender, EventArgs e)
        {
            Itembar itembar = sender as Itembar;

            TodoItemsStackPanel.Children.Remove(itembar);

            DataManipulation<ToDoItem> ToDoDataManipulation = new DataManipulation<ToDoItem>(@"./Inventory.json");
            currentInventory.Remove(itembar.ItemProperties);
            ToDoDataManipulation.Remove(itembar.ItemProperties);

            ItemCountChanged();
        }

        private void DetailsPanel_Close(object sender, EventArgs e)
        {
            DataGrid.Children.RemoveAt(DataGrid.Children.Count - 1);
        }
    }
}
