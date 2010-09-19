﻿namespace Tetris
{
	public class Piece_J : Piece
	{
		public Piece_J()
		{
			AddPosition(new bool[4, 4]
			{
				{false, false, false, false},
				{false, true, true, true},
				{false, false, false, true},
				{false, false, false, false},
			});

			AddPosition(new bool[4, 4]
			{
				{false, false, true, true},
				{false, false, true, false},
				{false, false, true, false},
				{false, false, false, false},
			});

			AddPosition(new bool[4, 4]
			{
				{false, true, false, false},
				{false, true, true, true},
				{false, false, false, false},
				{false, false, false, false},
			});

			AddPosition(new bool[4, 4]
			{
				{false, false, true, false},
				{false, false, true, false},
				{false, true, true, false},
				{false, false, false, false},
			});
		}
	}
}
