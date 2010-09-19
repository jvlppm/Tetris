using Microsoft.Xna.Framework;

namespace Tetris
{
	public class Piece_I : Piece
	{
		public Piece_I(Color color) : base(color)
		{
			AddPosition(new bool[4, 4]
			{
				{false, false, true, false},
				{false, false, true, false},
				{false, false, true, false},
				{false, false, true, false},
			});

			AddPosition(new bool[4, 4]
			{
				{false, false, false, false},
				{true, true, true, true},
				{false, false, false, false},
				{false, false, false, false},
			});
		}
	}
}
