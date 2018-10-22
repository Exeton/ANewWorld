using System;
using System.Collections.Generic;
using System.Diagnostics;
using The_Nth_D.Model;
using The_Nth_D.View;
using The_Nth_D.View.MapCaching;
using TheNthD;

namespace The_Nth_D
{
	class Camera
	{
		//Map map;
		//List<Entity> entities;
		//Game1 form;
		//ArrayMapCacher arrayMapCacher;

		//Stopwatch diagonsiticTimer = new Stopwatch();

		//public Camera(Map map, List<Entity> entities, Game1 form, ArrayMapCacher arrayMapCacher)
		//{
		//	this.map = map;
		//	this.entities = entities;
		//	this.form = form;
		//	this.arrayMapCacher = arrayMapCacher;
		//}

		//public int toWorldX(int screenX, int cameraWorldX) {
		//	return screenX + (cameraWorldX - form.Width / 2);
		//}

		//public int toWorldY(int screenY, int cameraWorldY)
		//{
		//	return screenY + (cameraWorldY - form.Height / 2);
		//}

		//public void drawFromCenter(SpriteBatch SpriteBatch, int centerX, int centerY)
		//{
		//	//Transform the point 1/2 a screen to the top left to be the top left.
		//	draw(SpriteBatch, centerX - form.Width / 2, centerY - form.Height / 2);
		//}

		//public void draw(SpriteBatch screenSpriteBatch, int left, int top)
		//{
		//	drawBitmap(screenSpriteBatch, left, top);
		//	return;
		//	//Currently on hold until I can profile and compare this to the bitmap method
		//	BufferedSpriteBatchContext SpriteBatchContext = BufferedSpriteBatchManager.Current;
		//	BufferedSpriteBatch bufferedSpriteBatch = SpriteBatchContext.Allocate(screenSpriteBatch, form.DisplayRectangle);
		//	SpriteBatch SpriteBatch = bufferedSpriteBatch.SpriteBatch;
		//	SpriteBatch.Clear(Color.White);
		//	//runMapDrawTest(SpriteBatch, left, top);
		//	drawMap(SpriteBatch, left, top);
			
		//	foreach (Entity entity in entities)
		//	{
		//		int screenX = (int)entity.x - left;
		//		int screenY = (int)entity.y - top;
		//		entity.Draw(SpriteBatch, screenX, screenY);
		//	}

		//	bufferedSpriteBatch.Render();
		//	bufferedSpriteBatch.Dispose();
		//}

		//public void drawBitmap(SpriteBatch screenSpriteBatch, int left, int top)
		//{

		//	Bitmap buffer = new Bitmap(form.Width, form.Height);
		//	SpriteBatch SpriteBatch = SpriteBatch.FromImage(buffer);

		//	SpriteBatch.Clear(Color.White);
		//	//runMapDrawTest(SpriteBatch, left, top);
		//	drawMap(SpriteBatch, left, top);

		//	foreach (Entity entity in entities)
		//	{
		//		int screenX = (int)entity.x - left;
		//		int screenY = (int)entity.y - top;
		//		entity.Draw(SpriteBatch, screenX, screenY);
		//	}

		//	screenSpriteBatch.DrawImage(buffer, 0, 0);

		//	SpriteBatch.Dispose();
		//	buffer.Dispose();
		//}

		//private void runMapDrawTest(SpriteBatch SpriteBatch, int left, int top)
		//{
		//	Stopwatch stopwatch = new Stopwatch();
		//	stopwatch.Start();

		//	for (int i = 0; i < 10000; i++)
		//	{
		//		drawMap(SpriteBatch, left, top);
		//	}

		//	stopwatch.Stop();
		//	long elasped = stopwatch.ElapsedMilliseconds;
		//	int breakpoint = 0;
		//}

		//public void drawMap(SpriteBatch SpriteBatch, int left, int top)
		//{
		//	int blockSize = Block.blockSize;

		//	int regionWidthInPixels = MapCacher.regionWidthInBlocks * blockSize;
		//	int regionHeightInPixels = MapCacher.regionHeightInBlocks * blockSize;

		//	int xOffset = toRegionCoord(left, regionWidthInPixels);
		//	int yOffset = toRegionCoord(top, regionHeightInPixels);

		//	int screenRegionWidth = form.Width / regionWidthInPixels;
		//	int screenRegionHeight = form.Height / regionHeightInPixels;

		//	int remX = left % regionWidthInPixels;
		//	int remY = top % regionHeightInPixels;

		//	int drawnMaps = 0;

		//	for (int i = 0; i < screenRegionWidth + 2; i++)
		//		for (int j = 0; j < screenRegionHeight + 2; j++)
		//		{
		//			int xPos = regionWidthInPixels * i - remX;
		//			if (remX < 0)
		//				xPos -= regionWidthInPixels;

		//			int yPos = regionHeightInPixels * j - remY;
		//			if (remY < 0)
		//				yPos -= regionHeightInPixels;

		//			if (xPos > form.Width || yPos > form.Height)
		//				continue;//Although system SpriteBatch likely already preforms culling, this prevents excess calls to mapCacher.getCachedRegion()

		//			Texture2D mapSection = arrayMapCacher.getCachedRegion(i + xOffset, j + yOffset);
		//			SpriteBatch.DrawImage(mapSection, xPos, yPos);
		//			drawnMaps++;
		//		}
		//	int k = drawnMaps;
		//}

		////This method makes it so negative screenCoords don't round towards 0
		//public int toRegionCoord(int screenCoord, int regionWidthInPixels)
		//{
		//	if (screenCoord >= 0)
		//		return screenCoord / regionWidthInPixels;
		//	else if (screenCoord % regionWidthInPixels == 0)//Rounding will not occur if the screenCoord is a - multiple of the regionWidth
		//		return screenCoord / regionWidthInPixels;
		//	else
		//		return (screenCoord / regionWidthInPixels) - 1;
		//}
	}
}
