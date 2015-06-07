using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chess.Units
{
    class Horse : Unit
    {
        public Horse(int PIECE_COLOR)
        {
            pieceType = "Horse";

            if (PIECE_COLOR == Utilities.Chesspieces.WHITE)
            {
                base.pieceID = 3;
                chessImage.Source = Utilities.Chesspieces.white_horse;
            }
            else if (PIECE_COLOR == Utilities.Chesspieces.BLACK)
            {
                base.pieceID = 9;
                chessImage.Source = Utilities.Chesspieces.black_horse;
            }
        }

        /**
         * Override method of base Unit class. Checks wether the move with the given destination
         * is a valid one.
         * */
        public override bool validateMove(Point dest, List<List<Placeholder>> spaces)
        {
            return false;
        }
    }
}
