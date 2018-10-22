using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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


		public Player(Bitmap sprite, int x, int y) : base(sprite, x, y)
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
			velocityY = movementSpeed; 
		}

		//This check must be run before the collision, which will set the velocity to 0
		public override void preTileCollision(int velocity, int dimension)
		{
			base.preTileCollision(velocity, dimension);
			if (dimension == 1 && velocity < 0)
					jumpTimer = 0;
		}

		public override void Draw(Graphics g, int screenX, int screenY)
		{
			base.Draw(g, screenX, screenY);
			drawCursorBlock(g);
		}

		private void drawCursorBlock(Graphics graphics)
		{
			//Best way is convert to map coords then back to screen coords
			int x = Cursor.Position.X - 5;
			int y = Cursor.Position.Y -18;
			graphics.FillRectangle(Brushes.Pink, x, y, Block.blockSize, Block.blockSize);
		}
	}
}
