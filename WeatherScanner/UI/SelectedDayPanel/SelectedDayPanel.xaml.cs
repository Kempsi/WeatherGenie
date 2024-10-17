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

namespace WeatherScanner.UI.SelectedDayPanel
{
	/// <summary>
	/// Interaction logic for SelectedDayPanel.xaml
	/// </summary>
	public partial class SelectedDayPanel : UserControl, INotifyPropertyChanged
	{
		public SelectedDayPanel()
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

		private string temp;
		public string Temp
		{
			get { return temp; }
			set
			{
				temp = value;
				OnPropertyChanged();
			}
		}


		private string feelsLike;
		public string FeelsLike
		{
			get { return feelsLike; }
			set
			{
				feelsLike = value;
				OnPropertyChanged();
			}
		}

		private string humidity;
		public string Humidity
		{
			get { return humidity; }
			set
			{
				humidity = value;
				OnPropertyChanged();
			}
		}

		private string windSpeed;
		public string WindSpeed
		{
			get { return windSpeed; }
			set
			{
				windSpeed = value;
				OnPropertyChanged();
			}
		}

		private string myVisibility;
		public string MyVisibility
		{
			get { return myVisibility; }
			set
			{
				myVisibility = value;
				OnPropertyChanged();
			}
		}

		private string timezone;
		public string Timezone
		{
			get { return timezone; }
			set
			{
				timezone = value;
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

		private string country;
		public string Country
		{
			get { return country; }
			set
			{
				country = value;
				OnPropertyChanged();
			}
		}

		private string city;
		public string City
		{
			get { return city; }
			set
			{
				city = value;
				OnPropertyChanged();
			}
		}

		private string sunrise;
		public string Sunrise
		{
			get { return sunrise; }
			set
			{
				sunrise = value;
				OnPropertyChanged();
			}
		}

		private string sunset;
		public string Sunset
		{
			get { return sunset; }
			set
			{
				sunset = value;
				OnPropertyChanged();
			}
		}


		#endregion Properties

		#region OnPropertyChanged

		public event PropertyChangedEventHandler? PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] string propName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}

		#endregion OnPropertyChanged


	}
}
