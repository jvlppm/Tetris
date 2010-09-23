using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tetris
{
	public class Grid
	{
		public int Width { get; private set; }
		public int Height { get; private set; }
		public int X { get; set; }
		public int Y { get; set; }

		Color[,] _grid;

		public Grid(int width, int height)
		{
			Width = width;
			Height = height;

			_grid = new Color[Height, Width];
		}

		public Color this[int line, int col]
		{
			get { return _grid[line, col]; }
			set { _grid[line, col] = value; }
		}
	}
}
