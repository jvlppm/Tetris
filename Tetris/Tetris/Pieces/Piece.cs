using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Tetris
{
	public class Piece
	{
		class PieceShape
		{
			public PieceShape(bool[,] shape)
			{
				Shape = shape;
				Width = 4;
				LeftWidth = 2;
				RightWidth = 1;

				for (int i = 0; i < 2; i++)
				{
					bool emptyCol = true;
					for (int j = 0; j < 4; j++)
					{
						if (shape[j, i])
						{
							emptyCol = false;
							break;
						}
					}

					if (emptyCol)
					{
						Width--;
						LeftWidth--;
					}
				}

				for (int i = 3; i > 2; i--)
				{
					bool emptyCol = true;
					for (int j = 0; j < 4; j++)
					{
						if (shape[j, i])
						{
							emptyCol = false;
							break;
						}
					}

					if (emptyCol)
					{
						Width--;
						RightWidth--;
					}
				}
			}

			public bool[,] Shape { get; private set; }
			public int LeftWidth { get; private set; }
			public int RightWidth { get; private set; }
			public int Width { get; private set; }
		}

		int CurrentPosition;
		List<PieceShape> Positions { get; set; }

		public bool[,] Shape { get { return Positions[CurrentPosition].Shape; } }
		public Color Color { get; private set; }

		public Piece(Color color)
		{
			Color = color;
			Positions = new List<PieceShape>();
		}

		protected void AddPosition(bool[,] shape)
		{
			Positions.Add(new PieceShape(shape));
		}

		public void RotateClockWise()
		{
			CurrentPosition++;
			if (CurrentPosition >= Positions.Count)
				CurrentPosition = 0;
		}

		public void RotateCounterClockWise()
		{
			CurrentPosition--;
			if (CurrentPosition < 0)
				CurrentPosition = Positions.Count - 1;
		}

		public int LeftWidth { get { return Positions[CurrentPosition].LeftWidth; } }

		public int RightWidth { get { return Positions[CurrentPosition].RightWidth; } }

		public int Width { get { return Positions[CurrentPosition].Width; } }
	}
}
