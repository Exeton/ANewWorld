using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Nth_D.Model;

namespace The_Nth_D
{
	class Player : EntityWithPhysics, I2DMovementController
	{
		public static int maxJumpTimer = 25;
		int jumpTimer;

		int movementSpeedCap = 10;
		int movementSpeed = 2;
		int verticalMovementSpeed = 10;


		public Player(Texture2D sprite, Vector2 Position) : base(sprite, Position)
		{
		}

		public void handelRightInput()
		{
			//Creates movement speed cap and momentum effect
			velocityX = Math.Min(movementSpeedCap, velocityX + movementSpeed);
		}

		public void handelLeftInput()
		{
			velocityX = Math.Max(-movementSpeedCap, velocityX -movementSpeed);
		}

		public void handelUpInput()
		{
			if (onBlock())
			{
				jumpTimer = maxJumpTimer;
			}
			if (jumpTimer >= 0)
				velocityY = -verticalMovementSpeed;
		}

		public override void onTickHook(Map map)
		{
			jumpTimer--;
		}

		public void handelDownInput()
		{
			//Tie this into acceleration so that different frecquencys of input updates won't affect the fall time.
			velocityY++; 
		}

		//This check must be run before the collision, which will set the velocity to 0
		public override void preTileCollision(int velocity, int dimension)
		{
			base.preTileCollision(velocity, dimension);
			if (dimension == 1 && velocity < 0)
					jumpTimer = 0;
		}

		public override void Draw(SpriteBatch spriteBatch, Camera camera)
		{
			base.Draw(spriteBatch, camera);
			drawCursorBlock(spriteBatch, camera);
		}

		private void drawCursorBlock(SpriteBatch spriteBatch, Camera camera)
		{
			Vector2 cursorBlock = getCursorBlock(camera);
			spriteBatch.Draw(Camera.tileSprite, cursorBlock * 10, null, Color.White, 0f, camera.calcOrigin(), Vector2.One, SpriteEffects.None, 0f);

			//SpriteBatch.FillRectangle(Brushes.Pink, x, y, Block.blockSize, Block.blockSize);
		}

		public static Vector2 getCursorBlock(Camera camera)
		{
			var mouseState = Mouse.GetState();
			int mouseX = mouseState.Position.X;
			int mouseY = mouseState.Position.Y;

			Vector2 worldPos = camera.calcOrigin() + new Vector2(mouseX - 6, mouseY - 3);
			return worldPos / 10;
		}
	}
}
