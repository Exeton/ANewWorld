using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Nth_D.MapLoading;

namespace The_Nth_D.Controller
{
	class NewMapKeybind : IKeyModuel
	{
		private IMapLoader mapLoader;
		private Form1 form1;

		public NewMapKeybind(IMapLoader mapLoader, Form1 form1)
		{
			this.mapLoader = mapLoader;
			this.form1 = form1;
		}

		public void onKeyDown()
		{
			mapLoader.save(Form1.map);
			Form1.fillMap();
			Form1.map.name = incrementMapNameNumber(Form1.map.name);
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
