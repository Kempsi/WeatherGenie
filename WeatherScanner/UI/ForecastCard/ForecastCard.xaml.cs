using System;
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

namespace WeatherScanner.UI.ForecastCard
{
	/// <summary>
	/// Interaction logic for ForecastCard.xaml
	/// </summary>
	public partial class ForecastCard : UserControl, INotifyPropertyChanged
	{

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
				SetAsActive();
			}
		}

		public event RoutedEventHandler ButtonClick;
		public event PropertyChangedEventHandler? PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] string propName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}

		private void SetAsActive()
		{

			if (isActive)
			{
				border.Background.Opacity = 0.5;
			}

			else
			{
				border.Background.Opacity = 0.0;
			}
		}



		private void btn_ForecastCardClicked(object sender, RoutedEventArgs e)
		{
			ButtonClick?.Invoke(this, e);
			 
			if (isActive)
			{
				isActive= false;
				SetAsActive();
				return;
			}

			if (!isActive)
			{
				isActive = true;
				SetAsActive();
				return;
			}
		}
	}


}
