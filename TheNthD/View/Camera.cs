﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using The_Nth_D.Model;
using The_Nth_D.View;
using The_Nth_D.View.MapCaching;
using TheNthD;
using TheNthD.TestingTools;
using TheNthD.View;

namespace The_Nth_D
{
	class Camera
	{
		Map map;
		List<Entity> entities;
		Game1 form;
		ArrayMapCacher arrayMapCacher;
		Player player;

		Stopwatch diagonsiticTimer = new Stopwatch();

		GraphicsDeviceManager graphicsManager;
		GraphicsDevice graphicsDevice;
		SpriteBatch spriteBatch;

		TileTextureMap tileTextureMap;
		FrameCounter frameCounter;
		SpriteFont spriteFont;

		public static Texture2D playerSprite;
		public static Texture2D tileSprite;

		public Camera(Map map, List<Entity> entities, Game1 form, ArrayMapCacher arrayMapCacher, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Player player, GraphicsDeviceManager graphicsManager, TileTextureMap tileTextureMap, FrameCounter frameCounter, SpriteFont spriteFont)
		{
			this.map = map;
			this.entities = entities;
			this.form = form;
			this.arrayMapCacher = arrayMapCacher;
			this.graphicsDevice = graphicsDevice;
			this.spriteBatch = spriteBatch;
			this.player = player;
			this.graphicsManager = graphicsManager;
			this.tileTextureMap = tileTextureMap;
			this.frameCounter = frameCounter;
			this.spriteFont = spriteFont;
		}

		//public int toWorldX(int screenX, int cameraWorldX) {
		//	return screenX + (cameraWorldX - form.Width / 2);
		//}

		//public int toWorldY(int screenY, int cameraWorldY)
		//{
		//	return screenY + (cameraWorldY - form.Height / 2);
		//}

		public void draw(GameTime gameTime)
		{
			//Top left is .5 screen from center of player sprite
			Vector2 origin = calcOrigin();

			graphicsDevice.Clear(Color.Black);
			spriteBatch.Begin();

			drawMap(origin);
			drawEntities(origin);
			spriteBatch.DrawString(spriteFont, frameCounter.AverageFramesPerSecond.ToString() + " fps", new Vector2(10, 10), Color.Yellow);
			spriteBatch.Draw(playerSprite, player.position, null, Color.White, 0f, origin, Vector2.One, SpriteEffects.None, 0f);
			spriteBatch.End();
		}
		public Vector2 calcOrigin()
		{
			float playerCenterX = player.position.X + playerSprite.Width / 2;
			float playerCenterY = player.position.Y + playerSprite.Height / 2;
			Vector2 origin = new Vector2(playerCenterX - graphicsManager.PreferredBackBufferWidth / 2, playerCenterY - graphicsManager.PreferredBackBufferHeight / 2);
			return origin;
		}


		public void drawMap(Vector2 origin)
		{
			Vector2 blockOrgin = origin / 10;
			Vector2 blockEnd = blockOrgin + new Vector2(graphicsManager.PreferredBackBufferWidth, graphicsManager.PreferredBackBufferHeight) / 10;

			for (int i = (int)blockOrgin.X; i < blockEnd.X; i++)
				for (int j = (int)blockOrgin.Y; j < blockEnd.Y; j++)
				{
					Texture2D sprite = tileTextureMap.get(map[i, j]);
					spriteBatch.Draw(sprite, new Vector2(i * 10, j * 10), null, Color.White, 0f, origin, Vector2.One, SpriteEffects.None, 0f);
				}
		}
		public void drawEntities(Vector2 origin)
		{
			foreach (Entity entity in entities)
			{
				spriteBatch.Draw(entity.sprite, entity.position, null, Color.White, 0f, origin, Vector2.One, SpriteEffects.None, 0f);
				entity.Draw(spriteBatch, this);
			}
		}
	}
}
