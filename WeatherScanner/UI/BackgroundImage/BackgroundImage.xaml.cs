using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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

namespace WeatherScanner.UI.BackgroundImage
{
	/// <summary>
	/// Interaction logic for BackgroundImage.xaml
	/// </summary>
	public partial class BackgroundImage : UserControl, INotifyPropertyChanged
	{
		public Dictionary<string, string> Images { get; set; }

		public BackgroundImage()
		{
			InitializeComponent();

			Images = new Dictionary<string, string>()
			{
				{"thunderstorm", ConfigurationManager.AppSettings["ThunderstormBG"] },
				{"rain", ConfigurationManager.AppSettings["RainBG"] },
				{"clouds", ConfigurationManager.AppSettings["BrokenCloudsBG"] },
				{"snow", ConfigurationManager.AppSettings["SnowBG"] },
				{"cloudy", ConfigurationManager.AppSettings["CloudyBG"] },
				{"clear", ConfigurationManager.AppSettings["ClearSkyBG"] }
			};

			DataContext = this;
		}


		public event PropertyChangedEventHandler? PropertyChanged;

		private string imageSource = ConfigurationManager.AppSettings["ClearSkyBG"];
		public string ImageSource
		{
			get { return imageSource; }
			set
			{
				imageSource = value;
				OnPropertyChanged();
			}
		}

		private void OnPropertyChanged([CallerMemberName] string propName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
		}

		public Dictionary<string,string> GetBackgroundImages()
		{
			return Images;
		}
	}
}
