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

		Texture2D Square { get; set; }

		Piece CurrentPiece { get; set; }
		Point CurrentPosition { get; set; }
		List<Piece> Pieces { get; set; }

		Grid Grid { get; set; }

		TimeSpan TimeTick { get; set; }
		DateTime LastTick { get; set; }
		#endregion

		public Tetris()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			graphics.PreferredBackBufferWidth = 12 * 10;
			graphics.PreferredBackBufferHeight = 12 * 20;
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
			Pieces = new List<Piece>
			{
				new Piece_O(Color.Yellow),
				new Piece_I(Color.Cyan),
				new Piece_S(Color.Green),
				new Piece_Z(Color.Pink),
				new Piece_L(Color.LightBlue),
				new Piece_J(Color.Orange),
				new Piece_T(Color.DarkRed),
			};

			TimeTick = TimeSpan.FromMilliseconds(500);
			LastTick = DateTime.Now;

			Grid = new Grid(10, 20);

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
	}
}
