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
	class EvilBox : Entity
	{
		Entity target;
		float baseSpeed = 1;
		bool dynamicSpeed;


		public EvilBox(Texture2D sprite, Vector2 position, Entity target, float speed, bool dynamicSpeed) : base(sprite, position)
		{
			this.target = target;
			this.baseSpeed = speed;
			this.dynamicSpeed = dynamicSpeed;
		}

		public void Move()
		{
			//Enemy should move in a straight line towards the enemy at a constant speed
			//movementX^2 + movementY^2 = movementSpeed^2
			//movementY / movementX = slope of a line drawn between the evil box and the target

			//Solve the system of equations to get the dX and dY

			float speed = calculateSpeed();
			float speedSquared = speed * speed;

			float changeInX;
			float changeInY;

			if (target.position.X - position.X == 0)
			{
				changeInX = 0;
				changeInY = speed;
			}
			else
			{
				float slope = (target.position.Y - position.Y) / (target.position.X - position.X);
				changeInX = (float)Math.Sqrt(speedSquared / (1 + slope * slope));

				float insideVal = speedSquared - changeInX * changeInX;
				if (insideVal < 0)
					insideVal = 0;
				changeInY = (float)Math.Sqrt(insideVal);
			}

			if (position.X > target.position.X)
				changeInX = -changeInX;
			if (position.Y > target.position.Y)
				changeInY = -changeInY;

			position.X += changeInX;
			position.Y += changeInY;
		}

		private float calculateSpeed()
		{
			float dx = target.position.X - position.X;
			float dy = target.position.Y - position.Y;

			if (Math.Abs(dx) + Math.Abs(dy) < 20)
				return 0;


			if (!dynamicSpeed)
				return baseSpeed;

			int expectedDistance = 100;
			float distApprox = Math.Abs(dx) + Math.Abs(dy) - expectedDistance;
	
			if (distApprox > 0)
				return baseSpeed + (float)Math.Sqrt(distApprox / 100);
			else
			{
				float value = baseSpeed - distApprox * distApprox / 100;

				if (value < 0)
					return 0;
				return value;
			}			
		}

		public override void onTick(Map map)
		{
			Move();
		}
	}
}
