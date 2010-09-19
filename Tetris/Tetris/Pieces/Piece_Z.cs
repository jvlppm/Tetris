namespace Tetris
{
	public class Piece_Z : Piece
	{
		public Piece_Z()
		{
			AddPosition(new bool[4, 4]
			{
				{false, false, false, false},
				{false, true, true, false},
				{false, false, true, true},
				{false, false, false, false},
			});

			AddPosition(new bool[4, 4]
			{
				{false, false, false, true},
				{false, false, true, true},
				{false, false, true, false},
				{false, false, false, false},
			});
		}
	}
}
