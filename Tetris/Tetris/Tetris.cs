using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Tetris
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Tetris : Microsoft.Xna.Framework.Game
	{
		Random Random { get; set; }

		KeyboardState OldKeyboardState { get; set; }
		KeyboardState CurrentKeyboardState { get; set; }

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Texture2D Square { get; set; }

		Piece CurrentPiece { get; set; }
		Point CurrentPosition { get; set; }
		List<Piece> Pieces { get; set; }

		Rectangle GridPosition { get; set; }

		TimeSpan TimeTick { get; set; }
		DateTime LastTick { get; set; }


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
				new Piece_L(Color.Blue),
				new Piece_J(Color.Orange),
				new Piece_T(Color.Pink),
			};

			TimeTick = TimeSpan.FromMilliseconds(500);
			LastTick = DateTime.Now;

			GridPosition = new Rectangle(0, 0, 10, 20);

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

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			CurrentKeyboardState = Keyboard.GetState();

			// Allows the game to exit
			if (Press(Keys.Escape))
				this.Exit();

			if (CurrentPiece != null)
			{
				if (Press(Keys.Left))
					CurrentPosition = new Point(CurrentPosition.X - 1, CurrentPosition.Y);
				else if (Press(Keys.Right))
					CurrentPosition = new Point(CurrentPosition.X + 1, CurrentPosition.Y);

				if (Press(Keys.Z))
					CurrentPiece.RotateCounterClockWise();
				else if (Press(Keys.X))
					CurrentPiece.RotateClockWise();

				if (CurrentPosition.X - CurrentPiece.LeftWidth < 0)
					CurrentPosition = new Point(CurrentPiece.LeftWidth, CurrentPosition.Y);
				else if (CurrentPosition.X + CurrentPiece.RightWidth >= GridPosition.Width)
					CurrentPosition = new Point(GridPosition.Width - CurrentPiece.RightWidth - 1, CurrentPosition.Y);
			}

			if (DateTime.Now.Subtract(LastTick) > TimeTick)
			{
				LastTick = DateTime.Now;

				if (CurrentPiece == null)
				{
					CurrentPiece = Pieces[Random.Next(Pieces.Count)];
					CurrentPosition = new Point(GridPosition.Width / 2, 0);
				}
				else
				{
					CurrentPosition = new Point(CurrentPosition.X, CurrentPosition.Y + 1);
				}
			}

			OldKeyboardState = CurrentKeyboardState;

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
			if (CurrentPiece != null)
			{
				for (int l = 0; l < 4; l++)
				{
					for (int c = 0; c < 4; c++)
					{
						if (CurrentPiece.Shape[l, c])
							spriteBatch.Draw(Square,
								new Vector2(
									GridPosition.Left + (CurrentPosition.X + c - 2) * Square.Width,
									GridPosition.Top + (CurrentPosition.Y + l - 1) * Square.Height
								), CurrentPiece.Color);
					}
				}
			}
			spriteBatch.End();

			base.Draw(gameTime);
		}

		bool Press(Keys key)
		{
			return CurrentKeyboardState.IsKeyDown(key) && (OldKeyboardState == null || OldKeyboardState.IsKeyUp(key));
		}
	}
}
