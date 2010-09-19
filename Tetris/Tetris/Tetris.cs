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

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Texture2D SquareBlue { get; set; }

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
				new Piece_O(),
				new Piece_I(),
				new Piece_S(),
				new Piece_Z(),
				new Piece_L(),
				new Piece_J(),
				new Piece_T(),
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

			SquareBlue = Content.Load<Texture2D>("SquareBlue");
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
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			var keyboard = Keyboard.GetState();

			if(keyboard.IsKeyDown(Keys.Left) && CurrentPosition.X > 0)
				CurrentPosition = new Point(CurrentPosition.X - 1, CurrentPosition.Y);
			else if (keyboard.IsKeyDown(Keys.Right) && CurrentPosition.X < GridPosition.Width - 1)
				CurrentPosition = new Point(CurrentPosition.X + 1, CurrentPosition.Y);

			if (DateTime.Now.Subtract(LastTick) > TimeTick)
			{
				LastTick = DateTime.Now;

				if (CurrentPiece == null)
				{
					CurrentPiece = Pieces[Random.Next(Pieces.Count)];
					CurrentPosition = new Point(GridPosition.Width / 2, 0);
				}
				else CurrentPosition = new Point(CurrentPosition.X, CurrentPosition.Y + 1);
			}

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
							spriteBatch.Draw(SquareBlue,
								new Vector2(
									GridPosition.Left + (CurrentPosition.X + c - 2) * SquareBlue.Width,
									GridPosition.Top + (CurrentPosition.Y + l - 1) * SquareBlue.Height
								), Color.White);
					}
				}
			}
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
