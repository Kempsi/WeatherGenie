﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GalleryApp.UserControls
{
    /// <summary>
    /// Interaction logic for WindowControlButton.xaml
    /// </summary>
    public partial class WindowControlButton : UserControl, INotifyPropertyChanged
    {
        private Image icon;
        private string normalImageSource;
        private string actImageSource;

        public WindowControlButton()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += WindowControlButtonLoaded;
        }

        private void WindowControlButtonLoaded(object sender, RoutedEventArgs e)
        {
            var button = (Button)Content;
            icon = (Image)button.Template.FindName("imageIcon", button);
        }

        public event RoutedEventHandler ButtonClick;
        public event PropertyChangedEventHandler? PropertyChanged;


        private string imageSource;
        public string ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                OnPropertyChanged();
            }
        }

        public string NormalImageSource
        {
            get { return normalImageSource; }
            set
            {
                normalImageSource = value;
                ImageSource = value;
            }
        }

        public string ActiveImageSource
        {
            get { return actImageSource; }
            set
            {
                actImageSource = value;
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick?.Invoke(this, e);
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            ImageSource = ActiveImageSource;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ImageSource = NormalImageSource;
        }
    }
}
