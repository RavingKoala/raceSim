using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RaceVisuals {
	public static class CreateManager {
		private static Dictionary<string, Bitmap> Images = new Dictionary<string, Bitmap>();

		public static Bitmap GetImage(string URI, int width, int height) {
			if (URI.Equals("Empty"))
				return EmptyBitmap(width, height);
			if (!Images.ContainsKey(URI))
				Images.Add(URI, (Bitmap)new Bitmap(Image.FromFile(URI)).Clone());
			return ResizeBitmap(Images[URI], width, height);
		}

		public static void ClearCashe() {
			Images.Clear();
		}

		public static Bitmap EmptyBitmap(int width, int height) {
			return ResizeBitmap((Bitmap)new Bitmap(Image.FromFile("Media/Background/SandImproved2.png")).Clone(), width, height);
		}
		public static Bitmap ResizeBitmap(Bitmap original, int width, int height) {
			return new Bitmap(original, new Size(width, height));
		}

		public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap) {
			if (bitmap == null)
				throw new ArgumentNullException("bitmap");

			Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

			BitmapData bitmapData = bitmap.LockBits(
				rect,
				ImageLockMode.ReadWrite,
				System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			try {
				int size = (rect.Width * rect.Height) * 4;

				return BitmapSource.Create(
					bitmap.Width,
					bitmap.Height,
					bitmap.HorizontalResolution,
					bitmap.VerticalResolution,
					PixelFormats.Bgra32,
					null,
					bitmapData.Scan0,
					size,
					bitmapData.Stride);
			} finally {
				bitmap.UnlockBits(bitmapData);
			}
		}

	}
}
