using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
		Map map;
		List<Entity> entities;
		Game1 form;
		ArrayMapCacher arrayMapCacher;
		Player player;

		Stopwatch diagonsiticTimer = new Stopwatch();

		GraphicsDeviceManager graphicsManager;
		GraphicsDevice graphicsDevice;
		SpriteBatch spriteBatch;

		public static Texture2D playerSprite;
		public static Texture2D tileSprite;

		public Camera(Map map, List<Entity> entities, Game1 form, ArrayMapCacher arrayMapCacher, GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, Player player, GraphicsDeviceManager graphicsManager)
		{
			this.map = map;
			this.entities = entities;
			this.form = form;
			this.arrayMapCacher = arrayMapCacher;
			this.graphicsDevice = graphicsDevice;
			this.spriteBatch = spriteBatch;
			this.player = player;
			this.graphicsManager = graphicsManager;
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
			float playerCenterX = player.position.X + playerSprite.Width / 2;
			float playerCenterY = player.position.Y + playerSprite.Height / 2;
			Vector2 origin = new Vector2(playerCenterX - graphicsManager.PreferredBackBufferWidth / 2, playerCenterY - graphicsManager.PreferredBackBufferHeight / 2);

			graphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin();

			drawMap(spriteBatch, origin);
		
			//	foreach (Entity entity in entities)
			//	{
			//		int screenX = (int)entity.x - left;
			//		int screenY = (int)entity.y - top;
			//		entity.Draw(SpriteBatch, screenX, screenY);
			//	}


			spriteBatch.Draw(playerSprite, player.position, null, Color.White, 0f, origin, Vector2.One, SpriteEffects.None, 0f);
			spriteBatch.End();
		}

		public void drawMap(SpriteBatch spriteBatch, Vector2 origin)
		{
			for (int i = 0; i < map.GetLength(0); i++)
				for (int j = 0; j < map.GetLength(1); j++)
				{
					if (map[i, j].filled)
					{
						spriteBatch.Draw(tileSprite, new Vector2(i * 10, j * 10), null, Color.White, 0f, origin, Vector2.One, SpriteEffects.None, 0f);
					}
				}
		}
	}
}
