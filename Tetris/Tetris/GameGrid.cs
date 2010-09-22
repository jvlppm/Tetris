using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris
{
	public class GameGrid
	{
		public int Width { get; private set; }
		public int Height { get; private set; }
		public int X { get; set; }
		public int Y { get; set; }

		Color[,] Grid { get; set; }

		public GameGrid(int width, int height)
		{
			Width = width;
			Height = height;

			Grid = new Color[Height, Width];
		}

		public Color this[int line, int col]
		{
			get { return Grid[line, col]; }
			set { Grid[line, col] = value; }
		}
	}
}
