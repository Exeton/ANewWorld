using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Nth_D.MapLoading
{
	interface IMapLoader
	{
		string[] getMapNames();
		Map load(string name);
		void save(Map map);
	}
}
