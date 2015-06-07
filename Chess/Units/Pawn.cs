using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Chess.Units
{
    class Pawn : Unit
    {
        public Pawn(int PIECE_COLOR)
        {
            pieceType = "Pawn";

            if (PIECE_COLOR == Utilities.Chesspieces.WHITE)
            {
                base.pieceID = 1;
                chessImage.Source = Utilities.Chesspieces.white_pawn;
            }
            else if (PIECE_COLOR == Utilities.Chesspieces.BLACK)
            {
                base.pieceID = 7;
                chessImage.Source = Utilities.Chesspieces.black_pawn;
            }
        }

        /**
         * Override method of base Unit class. Checks wether the move with the given destination
         * is a valid one.
         * */
        public override bool validateMove(Point dest, List<List<Placeholder>> spaces)
        {
            bool validMove = false;

            Point movementCoordinates = getMovementCoordinates(origin, dest);
            int startPosition = -1;
            switch (pieceID)
            {
                case 1:
                    startPosition = 1;
                    break;
                case 7:
                    startPosition = 6;
                    break;
            }
            //can only move straight vertical, except when capturing
            // first verify it's no diagonal movement
            if (Math.Abs(movementCoordinates.X) == 0)
            {
                if (origin.Y == startPosition && Math.Abs(movementCoordinates.Y) <= 2)
                {
                    // starting position, so allow up to 2 spaces
                    return true;
                }
                else if (Math.Abs(movementCoordinates.Y) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }        
        }
    }
}
