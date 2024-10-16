using System.Windows.Controls;
using WeatherScanner.UI.BackgroundImage;

namespace WeatherScanner.Entities.Managers
{
	public class BackgroundImageManager
	{
		BackgroundImage BackgroundImage { get; set; }

		Dictionary<string, string> BackgroundImages { get; set; }


		public BackgroundImageManager()
		{
			BackgroundImage = new BackgroundImage();
			BackgroundImages = BackgroundImage.GetBackgroundImages();
		}

		public void SetBackgroundImage(string description)
		{
			var words = description.ToLower().Split(' ');

			foreach (var word in words)
			{
				if (BackgroundImages.ContainsKey(word))
				{
					BackgroundImage.ImageSource = BackgroundImages[word];
					return; 
				}

				else
				{
					BackgroundImage.ImageSource = BackgroundImages["cloudy"];
				}
			}


		}

		public BackgroundImage GetImage()
		{
			return BackgroundImage;
		}


	}
}
