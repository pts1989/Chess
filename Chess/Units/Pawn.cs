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
            // verify the target doesn't contain a piece of our own
            if (base.validateDestination((Placeholder)spaces[(int)dest.X][(int)dest.Y]))
            {
                Unit target = (Unit)spaces[(int)dest.X][(int)dest.Y].chessPiece;

                //can only move straight vertical, except when capturing
                // first check the rules for vertical movement
                if (Math.Abs(movementCoordinates.X) == 0 && target == null)
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
                // check if it truly is a diagonal movement of 1x1
                // if so, see if the target destination has an enemy piece
                else if(Math.Abs(movementCoordinates.X) == Math.Abs(movementCoordinates.Y) && target != null)
                {
                    if (pieceID <= 6 && target.pieceID > 6)
                    {
                        return true;
                    }
                    else if (pieceID > 6 && target.pieceID <= 6)
                    {
                        return true;
                    }
                    else
                    {
                        return false; // no enemy piece, so invalid move 
                    }        
                }
                else
                {
                    return false; // invalid amount of movement
                }
            }
            else
            {
                return false; // target is our own piece
            }
        }
    }
}
