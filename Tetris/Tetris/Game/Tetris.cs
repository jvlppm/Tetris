using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
	public partial class Tetris : Microsoft.Xna.Framework.Game
	{
		#region Properties
		Random Random { get; set; }

		KeyboardState OldKeyboardState { get; set; }
		KeyboardState CurrentKeyboardState { get; set; }

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Texture2D Background { get; set; }
		Texture2D Square { get; set; }

		Piece CurrentPiece { get; set; }
		Piece NextPiece { get; set; }
		Point CurrentPosition { get; set; }

		Grid Grid { get; set; }

		TimeSpan TimeTick { get; set; }
		DateTime LastTick { get; set; }

		SpriteFont DefaultFont { get; set; }

		double _level;
		double Level
		{
			get { return _level; }
			set
			{
				_level = value;
				TimeTick = TimeSpan.FromSeconds(Math.Pow((0.8 - ((_level - 1) * 0.007)), (_level - 1)));
			}
		}

		int _lines;
		int Lines
		{
			get { return _lines; }
			set
			{
				_lines = value;
				if (Lines / 10 > Level)
					Level = Lines / 10;
			}
		}
		#endregion

		public Tetris()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			graphics.PreferredBackBufferWidth = 320;
			graphics.PreferredBackBufferHeight = 240;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			Random = new Random(DateTime.Now.Millisecond);

			NextPiece = CreatePiece();
			CurrentPiece = CreatePiece();

			Level = 0;
			LastTick = DateTime.Now;

			Grid = new Grid(10, 20);
			CurrentPosition = new Point(Grid.Width / 2, 0);

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			DefaultFont = Content.Load<SpriteFont>("DefaultFont");

			Grid.X = 35;
			Background = Content.Load<Texture2D>("Layout");
			Square = Content.Load<Texture2D>("SquareGray");
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		Piece CreatePiece()
		{
			switch(Random.Next(7))
			{
				case 0: return new Piece_O(Color.Yellow);
				case 1: return new Piece_I(Color.Cyan);
				case 2: return new Piece_S(Color.Green);
				case 3: return new Piece_Z(Color.Pink);
				case 4: return new Piece_L(Color.LightBlue);
				case 5: return new Piece_J(Color.Orange);
				case 6: return new Piece_T(Color.DarkRed);
			}
			return null;
		}
	}
}
