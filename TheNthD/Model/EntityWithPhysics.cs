using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Nth_D.Model;
using TheNthD;

namespace The_Nth_D
{
	class EntityWithPhysics : Entity
	{
		public int friction = 1;
		public int velocityX;
		public int velocityY;

		public EntityWithPhysics(Texture2D sprite, Vector2 position) : base(sprite, position)
		{
		}

		public override void onTick(Map map)
		{
			handelPhysics(map);
			onTickHook(map);
		}

		public virtual void onTickHook(Map map)
		{

		}

		public void handelPhysics(Map map)
		{
			velocityY += 2;

			if (velocityY > 10)
				velocityY = 10;

			handelMovement(ref velocityX, 0, map);
			handelMovement(ref velocityY, 1, map);
		}

		public bool onBlock()
		{
			return willCollide(Game1.map, 1, 1, new Vector2(0, 1));
		}

		public void handelMovement(ref int velocity, int dimension, Map map)
		{
			applyFriction(ref velocity);
			if (velocity == 0)//Yes, this must go after friction calculations
				return;

			bool flag;
			if (dimension == 0)
				flag = true;

			Vector2 velocityVec = Game1.velocityAndDimensionToVector(1, dimension, velocity);//If Velocity is passed in instead of 1, if velocity is negative, it'll get canceled out
			if (willCollide(map, velocity, dimension, velocityVec))
			{
				//movePlayerToBlockEdge(velocity, dimension);//Remove redundant calls to this. Like if the player's on the ground
				onTileCollosion(velocity, dimension);
			}
			else
				addVelocityVector(velocityVec);
		}

		public virtual void onTileCollosion(int velocity, int dimension)
		{
			preTileCollision(velocity, dimension);
			setVelocity(0, dimension);
		}

		//This method is used when the knowing the original velocity of the entity is necessary
		public virtual void preTileCollision(int velocity, int dimension)
		{

		}

		public bool willCollide(Map map, float velocity, int dimension, Vector2 velocityVec)
		{
			int spriteSizeOnAxis = getSize(dimension);
			Vector2 spriteOffsetVec = Game1.velocityAndDimensionToVector((int)velocity, dimension, spriteSizeOnAxis - 1);

			Vector2 positionVec = new Vector2(position.X, position.Y);
			Vector2 perpVector = Block.blockSize * Vector2.Normalize(Game1.positivePerpindicularVector(velocityVec));
			positionVec += velocityVec;//Prevent clipping issues

			if (velocity > 0)
				positionVec += spriteOffsetVec;



			for (int i = 0; i < spriteSizeOnAxis / Block.blockSize; i++)
			{
				Vector2 blockCoords = positionVec / Block.blockSize;
				if (map[blockCoords].filled == true)
					return true;
				positionVec += perpVector;
			}
			return false;
		}

		public void applyFriction(ref int velocity)
		{
			if (velocity > 0)
				velocity -= friction;
			if (velocity < 0)
				velocity += friction;
		}

		public void setVelocity(int value, int dimension)
		{
			if (dimension == 0)
				velocityX = value;
			else if (dimension == 1)
				velocityY = value;
			else
				throw new Exception("Invalid dimension");
		}
	}
}
