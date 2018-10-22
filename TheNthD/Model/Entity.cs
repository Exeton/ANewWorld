using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace The_Nth_D.Model
{
	abstract class Entity
	{
		public Bitmap sprite;
		int health;

		public float x;
		public float y;

		public Entity(Bitmap sprite, int x, int y)
		{
			this.sprite = sprite;
			this.x = x;
			this.y = y;
		}

		public virtual void Draw(Graphics g, int screenX, int screenY)
		{
			g.DrawImage(sprite, screenX, screenY);
		}

		public float getOutsideEdge(int dimension)
		{
			if (dimension == 0)
				return getRight();
			if (dimension == 1)
				return getBottom();
			throw new Exception("Invalid dimension");
		}

		public float getBottom()
		{
			return y + sprite.Height - 1;
		}

		public float getRight()
		{
			return x + sprite.Width - 1;
		}

		public virtual int getSize(int dimension)
		{
			if (dimension == 0)
				return sprite.Width;
			if (dimension == 1)
				return sprite.Height;
			throw new Exception("Invalid dimension");
		}

		public float getPos(int dimension)
		{
			if (dimension == 0)
				return x;
			if (dimension == 1)
				return y;
			throw new Exception("Invalid dimension");
		}

		public float getEdge(float velocity, int dimension)
		{
			if (velocity < 0)
				return getPos(dimension);
			else
				return getOutsideEdge(dimension);
		}

		public void setPos(int value, int dimension)
		{
			if (dimension == 0)
				x = value;
			else if (dimension == 1)
				y = value;
			else
				throw new Exception("Invalid dimension");
		}

		public void addPos(int value, int dimension)
		{
			if (dimension == 0)
				x += value;
			else if (dimension == 1)
				y += value;
			else
				throw new Exception("Invalid dimension");
		}

		public void movePlayerToBlockEdge(int velocity, int dimension)
		{
			//Set the players position to 9, or 0

			int pos = (int)getEdge(velocity, dimension);//Round float to whole number
			int lastDigit = pos % 10;

			if (velocity > 0)
			{
				int composite = 9 - lastDigit;
				addPos(composite, dimension);
			}
			else
			{
				addPos(-lastDigit, dimension);
			}
		}

		public void addVelocityVector(Vector2 vector)
		{
			x += vector.X;
			y += vector.Y;
		}


		public abstract void onTick(Map map);
	}
}
