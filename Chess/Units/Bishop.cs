using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chess.Units
{
    class Bishop : Unit
    {
        public Bishop(int PIECE_COLOR)
        {
            pieceType = "Bishop";

            if (PIECE_COLOR == Utilities.Chesspieces.WHITE)
            {
                base.pieceID = 4;
                chessImage.Source = Utilities.Chesspieces.white_bishop;
            }
            else if (PIECE_COLOR == Utilities.Chesspieces.BLACK)
            {
                base.pieceID = 10;
                chessImage.Source = Utilities.Chesspieces.black_bishop;
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
