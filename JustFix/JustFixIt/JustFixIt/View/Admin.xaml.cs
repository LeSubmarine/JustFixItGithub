﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using JustFixIt.View;
using JustFixIt.View.Task_for_admin;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JustFixIt
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Admin : Page
    {
        public Admin()
        {
            this.InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LogIn));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Actions.Navigate(typeof(Edit_task));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Actions.Navigate(typeof(Add_task));
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Actions.Navigate(typeof(Remove_task));
        }
    }
}
