using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			return get(block.type);
		}

		public Texture2D get(int blockType)
		{
			Texture2D texture;
			typesAndBlockTextures.TryGetValue(blockType, out texture);

			return texture;
		}
	}
}
