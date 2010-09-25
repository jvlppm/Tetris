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

		void DrawNextPiece()
		{
			for (int l = 0; l < 4; l++)
			{
				for (int c = 0; c < 4; c++)
				{
					if (NextPiece.Shape[l, c])
						spriteBatch.Draw(Square,
							new Vector2(
								190 + (5 + c - 2) * Square.Width,
								150 + (2 + l - 1) * Square.Height
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
