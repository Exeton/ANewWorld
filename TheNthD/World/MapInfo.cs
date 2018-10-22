using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Nth_D.World
{
	[Serializable]
	public class MapInfo
	{
		public string name;
		public string version;

		public MapInfo(string name, string version)
		{
			this.name = name;
			this.version = version;
		}
	}
}
