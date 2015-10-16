using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liath.BigSpace.Domain
{
	public class LocalAreaView
	{
		public Coordinates FocusCoordinates { get; set; }
		public ScreenSize ScreenSize { get; set; }

		public LocalAreaView(Coordinates focusCoordinates, ScreenSize screenSize)
		{
			if (focusCoordinates == null) throw new ArgumentNullException("focusCoordinates");
			if (screenSize == null) throw new ArgumentNullException("screenSize");

			FocusCoordinates = focusCoordinates;
			ScreenSize = screenSize;
		}
	}
}
