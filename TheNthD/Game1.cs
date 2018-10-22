using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using The_Nth_D;
using The_Nth_D.Controller;
using The_Nth_D.MapLoading;
using The_Nth_D.Model;
using The_Nth_D.View.MapCaching;
using Microsoft.Xna.Framework.Graphics;
using TheNthD.View;
using TheNthD.TestingTools;

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
		Texture2D evilBoxSprite;

		KeysManager keyManager;
		Player player;
		List<Entity> entities = new List<Entity>();
		IMapLoader mapLoader = new CompactFileMapLoader(Directory.GetCurrentDirectory() + @"\worlds\");

		public static Map map = new Map(400, 200, "worldA");
		Camera camera;
		ArrayMapCacher mapCacher;
		TileTextureMap tileTextureMap;
		SpriteFont spriteFont;

		private FrameCounter frameCounter = new FrameCounter();

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
			Window.AllowUserResizing = true;
			IsMouseVisible = true;

			// TODO: Add your initialization logic here
			base.Initialize();

			loadMap();
			//mapCacher = new ArrayMapCacher(map.GetLength(0), map.GetLength(1), map);
			player = new Player(playerSprite, new Vector2(60, 200));
			entities.Add(player);

			keyManager = new KeysManager(player);


			keyManager.registerKeybind(Keys.F, new SaveKeybind(mapLoader));
			keyManager.registerKeybind(Keys.R, new NewMapKeybind(mapLoader, this));

			spawnSnake();

			camera = new Camera(map, entities, this, new ArrayMapCacher(), GraphicsDevice, spriteBatch, player, graphics, tileTextureMap,frameCounter, spriteFont);
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			spriteFont = Content.Load<SpriteFont>("Font");
			playerSprite = Content.Load<Texture2D>("Ghost");
			evilBoxSprite = Content.Load<Texture2D>("EvilBox");
			Texture2D dirtSprite = Content.Load<Texture2D>("Dirt");

			Camera.tileSprite = dirtSprite;
			Camera.playerSprite = playerSprite;

			Dictionary<int, Texture2D> typesAndBlockTextures = new Dictionary<int, Texture2D>();
			typesAndBlockTextures.Add(0, Content.Load<Texture2D>("NullBlock"));
			typesAndBlockTextures.Add(1, Content.Load<Texture2D>("Air"));
			typesAndBlockTextures.Add(2, dirtSprite);

			tileTextureMap = new TileTextureMap(typesAndBlockTextures);
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


			frameCounter.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			var keyboardState = Keyboard.GetState();
			if (keyboardState.IsKeyDown(Keys.W))
				player.handelUpInput();
			if (keyboardState.IsKeyDown(Keys.A))
				player.handelLeftInput();
			if (keyboardState.IsKeyDown(Keys.S))
				player.handelDownInput();
			if (keyboardState.IsKeyDown(Keys.D))
				player.handelRightInput();


			placeBlocks();
			keyManager.handelInput();
			foreach (Entity entity in entities)
			{
				entity.onTick(map);
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			camera.draw(gameTime);
			base.Draw(gameTime);
		}

		private void placeBlocks()
		{
			var mouseState = Mouse.GetState();
			if (mouseState.LeftButton == ButtonState.Pressed)
			{
				Vector2 mouseBlock = Player.getCursorBlock(camera);
				map[mouseBlock].filled = true;
				map[mouseBlock].type = 2;
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
			for (int i = 0; i < 400; i++)
				for (int j = 0; j < 200; j++)
				{
					bool fill = (i > 397) || (j < 2) || i < 2 || j > 197;
					int type = 1;

					if (fill)
						type = 2;		

					map[i, j] = new Block(fill, type);
				}
		}

		private void spawnSnake()
		{
			//Bitmap evilBoxBitmap = createBox(75, 75, Color.Red);

			EvilBox playerTargetingBox = new EvilBox(evilBoxSprite, new Vector2(100, 100), player, 5, false);
			entities.Add(playerTargetingBox);

			Entity target = playerTargetingBox;

			for (float i = 10; i > 0; i--)
			{
				EvilBox evilBox = new EvilBox(evilBoxSprite, new Vector2(100, 100), target, 5, true);
				entities.Add(evilBox);

				target = evilBox;
			}
		}


		public static Vector2 positivePerpindicularVector(Vector2 vector2)
		{
			return new Vector2(Math.Abs(vector2.Y), Math.Abs(vector2.X));
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
