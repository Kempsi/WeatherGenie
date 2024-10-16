﻿using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using WeatherScanner.Entities.Managers;

namespace WeatherScanner.UI.ForecastCard
{
	/// <summary>
	/// Interaction logic for ForecastCard.xaml
	/// </summary>
	public partial class ForecastCard : UserControl, INotifyPropertyChanged
	{

		public ForecastManager manager;
	
		public ForecastCard()
		{
			InitializeComponent();
			DataContext = this;
		}

		#region Properties

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

		private string tempHigh;
		public string TempHigh
		{
			get { return tempHigh; }
			set
			{
				tempHigh = value;
				OnPropertyChanged();
			}
		}

		private string tempLow;
		public string TempLow
		{
			get { return tempLow; }
			set
			{
				tempLow = value;
				OnPropertyChanged();
			}
		}

		private string day;
		public string Day
		{
			get { return day; }
			set
			{
				day = value;
				OnPropertyChanged();
			}
		}

		private string date;
		public string Date
		{
			get { return date; }
			set
			{
				date = value;
				OnPropertyChanged();
			}
		}


		private string fullDate;
		public string FullDate
		{
			get { return fullDate; }
			set
			{
				fullDate = value;
				OnPropertyChanged();
			}
		}


		private string desc;
		public string Desc
		{
			get { return desc; }
			set
			{
				desc = value;
				OnPropertyChanged();
			}
		}

		private bool isActive;
		public bool IsActive
		{

			get { return isActive; }
			set
			{
				isActive = value;
				OnPropertyChanged();
				ChangeActiveOpacity();
			}
		}

		#endregion Properties

		#region Event handlers

		public event RoutedEventHandler ButtonClick;
		public event PropertyChangedEventHandler? PropertyChanged;

		#endregion Event handlers

		#region Property changed

		private void OnPropertyChanged([CallerMemberName] string propName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}


		#endregion Property changed

		

		// Changes the visual look of an active card
		public void ChangeActiveOpacity()
		{
			if (IsActive)
			{
				border.Background.Opacity = 0.3;
			}
			else
			{
				border.Background.Opacity = 0.0;
			}
		}


		// When clicked, calls for managers SetActiveCard
		public void btn_ForecastCardClicked(object sender, RoutedEventArgs e)
		{
			manager.SetActiveCard(this);
		}


	}


}
