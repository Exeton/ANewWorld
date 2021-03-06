﻿using System;
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
		private ANewWorld Game1;

		public NewMapKeybind(IMapLoader mapLoader, ANewWorld Game1)
		{
			this.mapLoader = mapLoader;
			this.Game1 = Game1;
		}

		public void onKeyDown()
		{
			Map currentMap = ANewWorld.map;
			mapLoader.save(currentMap);
			ANewWorld.map = new Map(currentMap.GetLength(0), currentMap.GetLength(1), incrementMapNameNumber(currentMap.name));
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
