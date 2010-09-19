using Microsoft.Xna.Framework;

namespace Tetris
{
	public class Piece_O : Piece
	{
		public Piece_O(Color color) : base(color)
		{
			AddPosition(new bool[4,4]
			{
				{false, false, false, false},
				{false, true, true, false},
				{false, true, true, false},
				{false, false, false, false},
			});
		}
	}
}
