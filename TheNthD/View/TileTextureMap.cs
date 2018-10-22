using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Nth_D.Model;

namespace TheNthD.View
{
	class TileTextureMap
	{
		private Dictionary<int, Texture2D> typesAndBlockTextures;

		public TileTextureMap(Dictionary<int, Texture2D> typesAndBlockTextures)
		{
			this.typesAndBlockTextures = typesAndBlockTextures;
		}

		public Texture2D get(Block block)
		{
			Texture2D texture;
			typesAndBlockTextures.TryGetValue(block.type, out texture);

			return texture;
		}
	}
}
