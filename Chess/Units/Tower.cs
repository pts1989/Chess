using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chess.Units
{
    class Tower : Unit
    {
        public Tower(int PIECE_COLOR)
        {
            pieceType = "Tower";

            if (PIECE_COLOR == Utilities.Chesspieces.WHITE)
            {
                base.pieceID = 2;
                chessImage.Source = Utilities.Chesspieces.white_tower;
            }
            else if (PIECE_COLOR == Utilities.Chesspieces.BLACK)
            {
                base.pieceID = 8;
                chessImage.Source = Utilities.Chesspieces.black_tower;
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
