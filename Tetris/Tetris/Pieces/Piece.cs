using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Tetris
{
	public class Piece
	{
		int CurrentPosition;
		List<bool[,]> Positions { get; set; }

		public bool[,] Shape { get { return Positions[CurrentPosition]; } }

		public Piece()
		{
			Positions = new List<bool[,]>();
		}

		protected void AddPosition(bool[,] shape)
		{
			Positions.Add(shape);
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
	}
}
