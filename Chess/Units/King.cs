using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chess.Units
{
    class King : Unit
    {
        public King(int PIECE_COLOR)
        {
            pieceType = "King";

            if (PIECE_COLOR == Utilities.Chesspieces.WHITE)
            {
                base.pieceID = 6;
                chessImage.Source = Utilities.Chesspieces.white_king;
            }
            else if (PIECE_COLOR == Utilities.Chesspieces.BLACK)
            {
                base.pieceID = 12;
                chessImage.Source = Utilities.Chesspieces.black_king;
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
