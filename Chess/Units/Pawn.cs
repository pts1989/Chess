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
        public Pawn(int PIECE_COLOR, Point origin)
        {
            pieceType = "Pawn";
            base.origin = origin;
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
            // make sure no invalid coordinates are given
            if (dest.X < 0 || dest.Y < 0) { return false; }

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
            if (origin != dest)
            {
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
                            validMove = true;
                        }
                        else if (Math.Abs(movementCoordinates.Y) == 1)
                        {
                            validMove = true;
                        }
                    }
                    // check if it truly is a diagonal movement of 1x1
                    // if so, see if the target destination has an enemy piece
                    else if (Math.Abs(movementCoordinates.X) == Math.Abs(movementCoordinates.Y) && target != null)
                    {
                        if (pieceID <= 6 && target.pieceID > 6)
                        {
                            validMove = true;
                        }
                        else if (pieceID > 6 && target.pieceID <= 6)
                        {
                            validMove = true;
                        }
                    }
                }
            }
            return validMove;
        }
    }
}
