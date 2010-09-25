using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public partial class Tetris : Microsoft.Xna.Framework.Game
	{
		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();

			spriteBatch.Draw(Background, Vector2.Zero, Color.White);

			DrawCurrentPiece();
			DrawNextPiece();
			DrawGrid();
			DrawInfo();

			spriteBatch.End();

			base.Draw(gameTime);
		}

		// Desenha a peça atual
		void DrawCurrentPiece()
		{
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
		}

		void DrawInfo()
		{
			spriteBatch.DrawString(DefaultFont, "Pontos:", new Vector2(200, 15), Color.Black);
			spriteBatch.DrawString(DefaultFont, Score.ToString("#,##0"), new Vector2(200, 30), Color.Black);

			spriteBatch.DrawString(DefaultFont, "Linhas:", new Vector2(200, 90), Color.Black);
			spriteBatch.DrawString(DefaultFont, Lines.ToString(), new Vector2(280, 90), Color.Black);

			spriteBatch.DrawString(DefaultFont, "Level:", new Vector2(200, 110), Color.Black);
			spriteBatch.DrawString(DefaultFont, Level.ToString(), new Vector2(280, 110), Color.Black);
		}

		void DrawNextPiece()
		{
			spriteBatch.DrawString(DefaultFont, "Próxima:", new Vector2(200, 155), Color.Black);

			for (int l = 0; l < 4; l++)
			{
				for (int c = 0; c < 4; c++)
				{
					if (NextPiece.Shape[l, c])
						spriteBatch.Draw(Square,
							new Vector2(
								190 + (5 + c - 2) * Square.Width,
								150 + (3 + l - 1) * Square.Height
							), NextPiece.Color);
				}
			}
		}

		// Desenha as peças já fixas no grid
		void DrawGrid()
		{
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
		}
	}
}
