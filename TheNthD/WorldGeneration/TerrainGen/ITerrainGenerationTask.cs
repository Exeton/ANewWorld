using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Nth_D;

namespace TheNthD.WorldGeneration
{
	interface ITerrainGenerationTask
	{
		void generate(Map map, int regionStartX, int regionEndX);
	}
}
