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

		GameGrid Grid { get; set; }

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

			Grid = new GameGrid(10, 20);

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
				{
					var leftPoint = new Point(CurrentPosition.X - 1, CurrentPosition.Y);
					if(ValidPosition(leftPoint))
						CurrentPosition = leftPoint;
				}
				else if (Press(Keys.Right))
				{
					var rightPoint = new Point(CurrentPosition.X + 1, CurrentPosition.Y);
					if(ValidPosition(rightPoint))
						CurrentPosition = rightPoint;
				}

				if (Press(Keys.Z))
					CurrentPiece.RotateCounterClockWise();
				else if (Press(Keys.X))
					CurrentPiece.RotateClockWise();

				if (CurrentPosition.X - CurrentPiece.LeftWidth < 0)
					CurrentPosition = new Point(CurrentPiece.LeftWidth, CurrentPosition.Y);
				else if (CurrentPosition.X + CurrentPiece.RightWidth >= Grid.Width)
					CurrentPosition = new Point(Grid.Width - CurrentPiece.RightWidth - 1, CurrentPosition.Y);
			}

			if (DateTime.Now.Subtract(LastTick) > TimeTick)
			{
				LastTick = DateTime.Now;

				if (CurrentPiece == null)
				{
					CurrentPiece = Pieces[Random.Next(Pieces.Count)];
					CurrentPosition = new Point(Grid.Width / 2, 0);
				}
				else
				{
					var nextPosition = new Point(CurrentPosition.X, CurrentPosition.Y + 1);
					if(ValidPosition(nextPosition))
						CurrentPosition = nextPosition;
					else
					{
						for (int l = 0; l < 4; l++)
							for (int c = 0; c < 4; c++)
								if (CurrentPiece.Shape[l, c])
									Grid[CurrentPosition.Y + l - 1, CurrentPosition.X + c - 2] = CurrentPiece.Color;

						for (int l = Grid.Height - 1; l >= 0; l--)
						{
							bool removeLine = true;
							for (int c = 0; c < Grid.Width; c++)
							{
								if (Grid[l, c] == Color.Transparent)
								{
									removeLine = false;
									break;
								}
							}

							if (removeLine)
							{
								for (int j = l - 1; j >= 0; j--)
								{
									for (int c = 0; c < Grid.Width; c++)
										Grid[j + 1, c] = Grid[j, c];
								}
								l++;
							}
						}
						CurrentPiece = null;
					}
				}
			}

			OldKeyboardState = CurrentKeyboardState;

			base.Update(gameTime);
		}

		private bool ValidPosition(Point nextPosition)
		{
			for (int l = 0; l < 4; l++)
			{
				int checkY = nextPosition.Y + l - 1;
				if (checkY < 0) continue;
				for (int c = 0; c < 4; c++)
				{
					int checkX = nextPosition.X + c - 2;
					if (checkX < 0 || checkX >= Grid.Width) continue;
					if (CurrentPiece.Shape[l, c] && (checkY >= Grid.Height || Grid[checkY, checkX] != Color.Transparent))
						return false;
				}
			}
			return true;
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
									Grid.X + (CurrentPosition.X + c - 2) * Square.Width,
									Grid.Y + (CurrentPosition.Y + l - 1) * Square.Height
								), CurrentPiece.Color);
					}
				}
			}
			for (int l = 0; l < Grid.Height; l++)
			{
				for (int c = 0; c < Grid.Width; c++)
				{
					if (Grid[l, c] != Color.Transparent)
					{
						spriteBatch.Draw(Square,
							new Vector2(
									Grid.X + c * Square.Width,
									Grid.Y + l * Square.Height
								), Grid[l, c]);
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
