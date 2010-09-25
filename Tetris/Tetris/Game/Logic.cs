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

			bool gravityTicking = DateTime.Now.Subtract(LastTick) > TimeTick;
			bool subTicking = DateTime.Now.Subtract(LastSubTick) > TimeSpan.FromMilliseconds(50);

			if (subTicking)
				LastSubTick = DateTime.Now;

			// Allows the game to exit
			if (Press(Keys.Escape))
				this.Exit();

			// Movimentação da peça
			if (CurrentPiece != null)
			{
				if (Press(Keys.Left) || (subTicking && Pressing(Keys.Left) > 0.3))
				{
					var leftPoint = new Point(CurrentPosition.X - 1, CurrentPosition.Y);
					if (ValidPosition(leftPoint))
						CurrentPosition = leftPoint;
				}
				else if (Press(Keys.Right) || (subTicking && Pressing(Keys.Right) > 0.3))
				{
					var rightPoint = new Point(CurrentPosition.X + 1, CurrentPosition.Y);
					if (ValidPosition(rightPoint))
						CurrentPosition = rightPoint;
				}

				if (Press(Keys.Down) || (subTicking && Pressing(Keys.Down) > 0.3))
				{
					Score += 3;
					gravityTicking = true;
				}

				if (Press(Keys.X) || Press(Keys.Up))
				{
					CurrentPiece.RotateCounterClockWise();
					if (!ValidPosition(CurrentPosition))
						CurrentPiece.RotateClockWise();
				}
				else if (Press(Keys.Z))
				{
					CurrentPiece.RotateClockWise();
					if (!ValidPosition(CurrentPosition))
						CurrentPiece.RotateCounterClockWise();
				}

				if (CurrentPosition.X - CurrentPiece.LeftWidth < 0)
					CurrentPosition = new Point(CurrentPiece.LeftWidth, CurrentPosition.Y);
				else if (CurrentPosition.X + CurrentPiece.RightWidth >= Grid.Width)
					CurrentPosition = new Point(Grid.Width - CurrentPiece.RightWidth - 1, CurrentPosition.Y);
			}

			// Descendo a peça atual
			if (gravityTicking)
			{
				LastTick = DateTime.Now;

				var nextPosition = new Point(CurrentPosition.X, CurrentPosition.Y + 1);
				if (ValidPosition(nextPosition))
					CurrentPosition = nextPosition;
				else
				{
					Score += 40;
					SolidifyCurrentPiece();
					ClearLines();
					CurrentPiece = NextPiece;
					NextPiece = CreatePiece();
					CurrentPosition = new Point(Grid.Width / 2, 0);
				}
			}

			UpdateKeyboardTimes();
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
			int cleared = 0;
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
					cleared++;
					for (int j = l - 1; j >= 0; j--)
					{
						for (int c = 0; c < Grid.Width; c++)
							Grid[j + 1, c] = Grid[j, c];
					}
					l++;
				}
			}

			Lines += cleared;

			switch(cleared)
			{
				case 1: Score += (int)Level * 40 + 40; break;
				case 2: Score += (int)Level * 100 + 100; break;
				case 3: Score += (int)Level * 300 + 300; break;
				case 4: Score += (int)Level * 1200 + 1200; break;
				default: break;
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

		#region Keyboard

		void UpdateKeyboardTimes()
		{
			foreach (Keys key in Enum.GetValues(typeof(Keys)))
			{
				if (Press(key))
					StartPressing.Add(key, DateTime.Now);
				else if (Release(key))
					StartPressing.Remove(key);
			}
		}

		double Pressing(Keys key)
		{
			if (StartPressing.ContainsKey(key))
				return DateTime.Now.Subtract(StartPressing[key]).TotalSeconds;

			return 0;
		}

		// Detecta se a tecla foi pressionada
		bool Press(Keys key)
		{
			return CurrentKeyboardState.IsKeyDown(key) && (OldKeyboardState == null || OldKeyboardState.IsKeyUp(key));
		}

		bool Release(Keys key)
		{
			return CurrentKeyboardState.IsKeyUp(key) && (OldKeyboardState != null && OldKeyboardState.IsKeyDown(key));
		}

		#endregion
	}
}
