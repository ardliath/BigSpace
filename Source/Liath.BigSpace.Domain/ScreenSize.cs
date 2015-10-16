namespace Liath.BigSpace.Domain
{
	public class ScreenSize
	{
		public int Height { get; set; }

		public int Width { get; set; }

		public ScreenSize(int height, int width)
		{
			Height = height;
			Width = width;
		}
	}
}