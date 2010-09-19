namespace Tetris
{
	public class Piece_S: Piece
	{
		public Piece_S()
		{
			AddPosition(new bool[4, 4]
			{
				{false, false, false, false},
				{false, false, true, true},
				{false, true, true, false},
				{false, false, false, false},
			});

			AddPosition(new bool[4, 4]
			{
				{false, false, true, false},
				{false, false, true, true},
				{false, false, false, true},
				{false, false, false, false},
			});
		}
	}
}
