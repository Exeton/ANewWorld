using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Nth_D.MapLoading;
using TheNthD;

namespace The_Nth_D.Controller
{
	class NewMapKeybind : IKeyModuel
	{
		private IMapLoader mapLoader;
		private Game1 Game1;

		public NewMapKeybind(IMapLoader mapLoader, Game1 Game1)
		{
			this.mapLoader = mapLoader;
			this.Game1 = Game1;
		}

		public void onKeyDown()
		{
			mapLoader.save(Game1.map);
			Game1.fillMap();
			Game1.map.name = incrementMapNameNumber(Game1.map.name);
		}

		public string incrementMapNameNumber(string mapName)
		{
			string end = mapName.Substring(5);//Removes the "world" part of the name

			int mapNum;
			if (int.TryParse(end, out mapNum))
				return "world" + (mapNum + 1).ToString();
			return "world1";
		}

		public void onKeyUp()
		{
		}
	}
}
