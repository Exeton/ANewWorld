﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Nth_D.MapLoading;
using TheNthD;

namespace The_Nth_D.Controller
{
	class SaveKeybind : IKeyModuel
	{
		private IMapLoader mapLoader;

		public SaveKeybind(IMapLoader mapLoader)
		{
			this.mapLoader = mapLoader;
		}

		public void onKeyDown()
		{
			mapLoader.save(ANewWorld.map);
		}

		public void onKeyUp()
		{
		}
	}
}
