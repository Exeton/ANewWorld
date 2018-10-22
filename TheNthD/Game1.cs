using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using The_Nth_D;
using The_Nth_D.Controller;
using The_Nth_D.MapLoading;
using The_Nth_D.Model;
using The_Nth_D.View.MapCaching;

namespace TheNthD
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Texture2D playerSprite;
		Vector2 playerPos;


		KeysManager keyManager;
		Player player;
		List<Entity> entities = new List<Entity>();
		IMapLoader mapLoader = new CompactFileMapLoader(Directory.GetCurrentDirectory() + @"\worlds\");

		public static Map map = new Map(400, 200, "worldA");
		Camera camera;
		ArrayMapCacher mapCacher;

		public static int fps = 50;
		private int ms = 0;


		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			playerPos = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
			base.Initialize();

			loadMap();
			mapCacher = new ArrayMapCacher(map.GetLength(0), map.GetLength(1), map);
			camera = new Camera(map, entities, this, mapCacher);

			WindowState = FormWindowState.Maximized;
			DoubleBuffered = true;

			Bitmap playerSprite;
			//playerSprite = new Bitmap(Bitmap.FromFile(@""));
			playerSprite = createBox(100, 100, Color.Black);
			player = new Player(playerSprite, 60, 200);
			entities.Add(player);

			keyManager = new KeysManager(player);

			gameLoop = new Timer();
			gameLoop.Interval = 10;
			gameLoop.Tick += GameLoop_Tick;
			gameLoop.Start();

			keyManager.registerKeybind(Keys.F, new SaveKeybind(mapLoader));
			keyManager.registerKeybind(Keys.R, new NewMapKeybind(mapLoader, this));

			spawnSnake();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			playerSprite = Content.Load<Texture2D>("Ghost");
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			spriteBatch.Draw(playerSprite, playerPos, null, Color.White, 0f, playerPos, Vector2.One, SpriteEffects.None, 0f);
			spriteBatch.End();
			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}

		private void placeBlocks()
		{
			if (MouseButtons == MouseButtons.Left)
			{
				int x = camera.toWorldX(Cursor.Position.X, (int)player.x) / 10;
				int y = camera.toWorldY((Cursor.Position.Y - 18), (int)player.y) / 10;

				map[x, y].filled = true;
				map[x, y].color = Color.Pink;

				mapCacher.invalidateRegion(x, y);

			}
		}

		//Method Source https://stackoverflow.com/questions/1720160/how-do-i-fill-a-bitmap-with-a-solid-color
		private Bitmap createBox(int width, int height, Color color)
		{
			Bitmap Bmp = new Bitmap(width, height);
			using (Graphics gfx = Graphics.FromImage(Bmp))
			using (SolidBrush brush = new SolidBrush(color))
			{
				gfx.FillRectangle(brush, 0, 0, width, height);
			}

			return Bmp;
		}

		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			keyManager.onKeyUp(e.KeyCode);
			setKeyValue(e.KeyCode, false);
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			keyManager.onKeyDown(e.KeyCode);
			setKeyValue(e.KeyCode, true);
		}

		private void setKeyValue(Keys keyCode, bool value)
		{
			switch (keyCode)
			{
				case Keys.W:
					keyManager.keys[0] = value;
					break;
				case Keys.S:
					keyManager.keys[1] = value;
					break;
				case Keys.A:
					keyManager.keys[2] = value;
					break;
				case Keys.D:
					keyManager.keys[3] = value;
					break;
			}
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			camera.drawFromCenter(e.Graphics, (int)player.x, (int)player.y);
		}

		private void GameLoop_Tick(object sender, EventArgs e)
		{
			placeBlocks();
			keyManager.handelInput();

			ms += gameLoop.Interval;
			int frameDelay = 1000 / fps;
			if (ms > frameDelay)
			{

				Invalidate();
				ms -= frameDelay;
			}

			foreach (Entity entity in entities)
			{
				entity.onTick(map);
			}
		}

		private void loadMap()
		{
			foreach (string mapName in mapLoader.getMapNames())
			{
				map = mapLoader.load(mapName).onDeseralized();
				return;
			}
			fillMap();
		}

		public static void fillMap()
		{
			Color c = Color.Brown;
			for (int i = 0; i < 400; i++)
				for (int j = 0; j < 200; j++)
				{
					bool fill = (i > 397) || (j < 2) || i < 2 || j > 197;
					map[i, j] = new Block(fill, c);
				}
		}

		private void spawnSnake()
		{
			Bitmap evilBoxBitmap = createBox(75, 75, Color.Red);

			EvilBox playerTargetingBox = new EvilBox(evilBoxBitmap, 100, 100, player, 5, false);
			entities.Add(playerTargetingBox);

			Entity target = playerTargetingBox;

			for (float i = 10; i > 0; i--)
			{
				EvilBox evilBox = new EvilBox(evilBoxBitmap, 100, 100, target, 5, true);
				entities.Add(evilBox);

				target = evilBox;
			}
		}


		public static Vector2 positivePerpindicularVector(Vector2 vector2)
		{
			return Vector2.Abs(new Vector2(vector2.Y, vector2.X));
		}

		public static Vector2 velocityAndDimensionToVector(int velocity, int dimension, int val)
		{
			if (velocity < 0)
				val *= -1;

			if (dimension == 0)
				return new Vector2(val, 0);
			if (dimension == 1)
				return new Vector2(0, val);

			throw new Exception("Invalid dimension");
		}

		public static Vector2 velocityAndDimensionToUnitVector(int velocity, int dimension)
		{
			return velocityAndDimensionToVector(velocity, dimension, 1);
		}

		public static Vector2 getUnitUp()
		{
			return new Vector2(0, -1);
		}
		public static Vector2 getUnitDown()
		{
			return new Vector2(0, 1);
		}
		public static Vector2 getUnitLeft()
		{
			return new Vector2(-1, 0);
		}
		public static Vector2 getUnitRight()
		{
			return new Vector2(1, 0);
		}

	}
}
