using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Nth_D.Model;

namespace The_Nth_D.View.MapCaching
{
	class ArrayMapCacher : MapCacher
	{
		//Bitmap[,] cache;
		//Map map;
		//Bitmap outsideMap;

		//public ArrayMapCacher(int mapBlockWidth, int mapBlockHeight, Map map)
		//{
		//	this.map = map;
		//	cache = new Bitmap[mapBlockWidth / regionWidthInBlocks, mapBlockHeight / regionHeightInBlocks];

		//	int blockSize = Block.blockSize;
		//	outsideMap = new Bitmap(regionWidthInBlocks * blockSize, regionHeightInBlocks * blockSize);
			
		//	using (SpriteBatch grapics = SpriteBatch.FromImage(outsideMap))
		//	{
		//		grapics.Clear(Color.Orange);
		//	}
		//}

		//public override void chacheRegion(int regionX, int regionY, Texture2D regionBlocks)
		//{
		//	cache[regionX, regionY] = regionBlocks;
		//}

		//public void invalidateRegion(int blockX, int blockY)
		//{
		//	int x = blockX / regionWidthInBlocks;
		//	int y = blockY / regionHeightInBlocks;
		//	if (x < 0 || y < 0 || x >= cache.GetLength(0) || y >= cache.GetLength(1))
		//		return;

		//	if (cache[x,y] != null)
		//		cache[x, y].Dispose();
		//	cache[x, y] = null;//Do bounds check
		//}

		////Negative region coords can round to zero when converted to region coords creating a white screen on the top and left of the world
		//public override Texture2D getCachedRegion(int regionX, int regionY)
		//{
		//	int blockSize = Block.blockSize;
		//	if (regionX < 0 || regionY < 0 || regionX >= cache.GetLength(0) || regionY >= cache.GetLength(1))
		//		return outsideMap;

		//	Bitmap cachedBitmap = cache[regionX, regionY];
		//	if (cachedBitmap != null)
		//		return cachedBitmap;


		//	Bitmap regionBitmap = new Bitmap(regionWidthInBlocks * blockSize, regionHeightInBlocks * blockSize);

		//	using (SpriteBatch SpriteBatch = SpriteBatch.FromImage(regionBitmap))
		//	{
		//		int startBlockX = regionX * regionWidthInBlocks;
		//		int startBlockY = regionY * regionHeightInBlocks;

		//		for (int i = 0; i < regionWidthInBlocks; i++)
		//			for (int j = 0; j < regionHeightInBlocks; j++)
		//			{
		//				if (map[startBlockX + i, startBlockY + j].filled)
		//				{
		//					SpriteBatch.FillRectangle(map[startBlockX + i, startBlockY + j].brush, blockSize * i, blockSize * j, blockSize, blockSize);
		//				}
		//			}
		//		cache[regionX, regionY] = regionBitmap;
		//	}
		//	return regionBitmap;
		//}
	}
}
