using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chess.Units
{
    class Queen : Unit
    {
        public Queen(int PIECE_COLOR)
        {
            pieceType = "Queen";

            if (PIECE_COLOR == Utilities.Chesspieces.WHITE)
            {
                base.pieceID = 5;
                chessImage.Source = Utilities.Chesspieces.white_queen;
            }
            else if (PIECE_COLOR == Utilities.Chesspieces.BLACK)
            {
                base.pieceID = 11;
                chessImage.Source = Utilities.Chesspieces.black_queen;
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
