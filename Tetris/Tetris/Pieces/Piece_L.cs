using Microsoft.Xna.Framework;

namespace Tetris
{
	public class Piece_L : Piece
	{
		public Piece_L(Color color) : base(color)
		{
			AddPosition(new bool[4, 4]
			{
				{false, false, false, false},
				{false, true, true, true},
				{false, true, false, false},
				{false, false, false, false},
			});

			AddPosition(new bool[4, 4]
			{
				{false, false, true, false},
				{false, false, true, false},
				{false, false, true, true},
				{false, false, false, false},
			});

			AddPosition(new bool[4, 4]
			{
				{false, false, false, true},
				{false, true, true, true},
				{false, false, false, false},
				{false, false, false, false},
			});

			AddPosition(new bool[4, 4]
			{
				{false, true, true, false},
				{false, false, true, false},
				{false, false, true, false},
				{false, false, false, false},
			});
		}
	}
}
