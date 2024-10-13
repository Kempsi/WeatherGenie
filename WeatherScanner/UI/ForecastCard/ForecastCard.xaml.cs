using System.ComponentModel;
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

		private int tempHigh;
		public int TempHigh
		{
			get { return tempHigh; }
			set
			{
				tempHigh = value;
				OnPropertyChanged();
			}
		}

		private int tempLow;
		public int TempLow
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

		private int date;
		public int Date
		{
			get { return date; }
			set
			{
				date = value;
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

		public event RoutedEventHandler ButtonClick;
		public event PropertyChangedEventHandler? PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] string propName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}

		// Changes the visual look of an active card
		public void ChangeActiveOpacity()
		{
			if (IsActive)
			{
				border.Background.Opacity = 0.5;
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
