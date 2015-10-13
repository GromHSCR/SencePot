using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace SenceRep.GromHSCR.Helpers
{
	public static class BitmapHelper
	{
		public static BitmapImage ToBitmapImage(this Bitmap source)
		{
			if (source == null) throw new ArgumentNullException("source");

			var bitmapImage = new BitmapImage();

			using (var memory = new MemoryStream())
			{
				source.Save(memory, ImageFormat.Png);
				memory.Position = 0;

				bitmapImage.BeginInit();
				bitmapImage.StreamSource = memory;
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.EndInit();
			}

			return bitmapImage;
		}
	}
}
