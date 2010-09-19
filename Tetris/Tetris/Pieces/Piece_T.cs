using Microsoft.Xna.Framework;

namespace Tetris
{
	public class Piece_T: Piece
	{
		public Piece_T(Color color) : base(color)
		{
			AddPosition(new bool[4, 4]
			{
				{false, false, false, false},
				{false, true, true, true},
				{false, false, true, false},
				{false, false, false, false},
			});

			AddPosition(new bool[4, 4]
			{
				{false, false, true, false},
				{false, false, true, true},
				{false, false, true, false},
				{false, false, false, false},
			});

			AddPosition(new bool[4, 4]
			{
				{false, false, true, false},
				{false, true, true, true},
				{false, false, false, false},
				{false, false, false, false},
			});

			AddPosition(new bool[4, 4]
			{
				{false, false, true, false},
				{false, true, true, false},
				{false, false, true, false},
				{false, false, false, false},
			});
		}
	}
}
