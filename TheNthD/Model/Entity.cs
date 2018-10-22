using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Nth_D.Model
{
	abstract class Entity
	{
		public Texture2D sprite;
		int health;

		public Vector2 position;

		public Entity(Texture2D sprite, Vector2 position)
		{
			this.sprite = sprite;
			this.position = position;
		}

		public virtual void Draw(SpriteBatch g, int screenX, int screenY)
		{
			g.Draw(sprite, new Vector2(screenX, screenY));
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
			return position.Y + sprite.Height - 1;
		}

		public float getRight()
		{
			return position.X + sprite.Width - 1;
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
				return position.X;
			if (dimension == 1)
				return position.Y;
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
				position.X = value;
			else if (dimension == 1)
				position.Y = value;
			else
				throw new Exception("Invalid dimension");
		}

		public void addPos(int value, int dimension)
		{
			if (dimension == 0)
				position.X += value;
			else if (dimension == 1)
				position.Y += value;
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
			position.X += vector.X;
			position.Y += vector.Y;
		}


		public abstract void onTick(Map map);
	}
}
