using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tetris
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public partial class Tetris : Microsoft.Xna.Framework.Game
	{
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

			// Movimentação da peça
			if (CurrentPiece != null)
			{
				if (Press(Keys.Left))
				{
					var leftPoint = new Point(CurrentPosition.X - 1, CurrentPosition.Y);
					if (ValidPosition(leftPoint))
						CurrentPosition = leftPoint;
				}
				else if (Press(Keys.Right))
				{
					var rightPoint = new Point(CurrentPosition.X + 1, CurrentPosition.Y);
					if (ValidPosition(rightPoint))
						CurrentPosition = rightPoint;
				}

				if (Press(Keys.X))
					CurrentPiece.RotateCounterClockWise();
				else if (Press(Keys.Z))
					CurrentPiece.RotateClockWise();

				if (CurrentPosition.X - CurrentPiece.LeftWidth < 0)
					CurrentPosition = new Point(CurrentPiece.LeftWidth, CurrentPosition.Y);
				else if (CurrentPosition.X + CurrentPiece.RightWidth >= Grid.Width)
					CurrentPosition = new Point(Grid.Width - CurrentPiece.RightWidth - 1, CurrentPosition.Y);
			}

			// Descendo a peça atual
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
					if (ValidPosition(nextPosition))
						CurrentPosition = nextPosition;
					else
					{
						SolidifyCurrentPiece();
						ClearLines();
						CurrentPiece = null;
					}
				}
			}

			OldKeyboardState = CurrentKeyboardState;
			base.Update(gameTime);
		}

		// Fixa a peça atual no grid
		private void SolidifyCurrentPiece()
		{
			for (int l = 0; l < 4; l++)
			{
				for (int c = 0; c < 4; c++)
				{
					var gridLine = CurrentPosition.Y + l - 1;
					if (gridLine >= 0 && CurrentPiece.Shape[l, c])
						Grid[gridLine, CurrentPosition.X + c - 2] = CurrentPiece.Color;
				}
			}
		}

		// Remove linhas completas
		private void ClearLines()
		{
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
		}

		// Confere se a peça atual pode estas na posição indicada
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

		// Detecta se a tecla foi pressionada
		bool Press(Keys key)
		{
			return CurrentKeyboardState.IsKeyDown(key) && (OldKeyboardState == null || OldKeyboardState.IsKeyUp(key));
		}
	}
}
