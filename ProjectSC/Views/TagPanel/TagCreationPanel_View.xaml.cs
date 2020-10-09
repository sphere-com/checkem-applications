﻿using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ProjectSC.Views.TagPanel
{
    public partial class TagCreationPanel_View : UserControl
    {
        public TagCreationPanel_View(ToDoList_View toDo, TagList_View tagList_View)
        {
            InitializeComponent();

            ToDoList = toDo;
            TagList = tagList_View;
        }


        private ToDoList_View ToDoList;
        private TagList_View TagList;


        private void DarkGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed || Mouse.XButton1 == MouseButtonState.Pressed)
            {
                ToDoList.CloseTagCreationPanel();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            TextboxTagName.Focus();
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            #region Create new tag button and add to the view
            Style style = this.FindResource("TagButton") as Style;

            Button TagButton = new Button() { Style = style, Content = TextboxTagName.Text };
            TagButton.Click += new RoutedEventHandler(this.Tag_Click);

            TagList.StpTagList.Children.Add(TagButton);
            #endregion

            

            //close everything after the work is completed
            ToDoList.CloseTagCreationPanel();
        }

        private void Tag_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}