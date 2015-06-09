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
        public King(int PIECE_COLOR, Point origin)
        {
            pieceType = "King";
            base.origin = origin;
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
            Point movementCoordinates = base.getMovementCoordinates(origin, dest);
            bool validMove = false;

            if (origin != dest)
            {
                if (base.validateDestination((Placeholder)spaces[(int)dest.X][(int)dest.Y]))
                {
                    if (Math.Abs(movementCoordinates.X) <= 1 && Math.Abs(movementCoordinates.Y) <= 1)
                    {
                        // see if the move creates a 'check' status, delegates to checkmate, which would
                        // be worse, any case, 'check' is not allowed
                        if (!check(dest, spaces))
                        {
                            validMove = true;
                        }
                    }
                }
            }
            //can move in every direction, but can not move more than one place. Also he can not make a move that would result in a checkmate or check. Also 
            // can switch places with tower once'castling'.
            return validMove;
        }

        /**
         * Returns a bool based on wether the king has 'check' status
         * can be called on attempting to move the king, or from chessboard
         * determining if a king is checked after the other player moved a piece
         * */
        public bool check(Point position, List<List<Placeholder>> spaces)
        {
            int pawn, bishop, tower, horse, queen, king;
            switch (pieceID)
            {
                case 6: // check against black pieces
                    pawn = 7;
                    tower = 8;
                    horse = 9;
                    bishop = 10;
                    queen = 11;
                    king = 12;
                    break;
                case 12: // check against white pieces
                    pawn = 1;
                    tower = 2;
                    horse = 3;
                    bishop = 4;
                    queen = 5;
                    king = 6;
                    break;
                default:
                    return false;
            }

            #region pawns
            // first check any pawns in close proximity, diagonally, only in front of the king
            int y = 0;
            if (pieceID == 6) // only check spaces > position.Y
            {
                y = 1;
            }else if(pieceID == 12){
                y = -1;
            }
            if (spaces[(int)position.X + 1][(int)position.Y + y].chessPiece != null)
            {
                Unit piece = (Unit)spaces[(int)position.X + 1][(int)position.Y + y].chessPiece;
                if (piece.pieceID == pawn)
                {
                    return true;
                }
            }
            if (spaces[(int)position.X - 1][(int)position.Y + y].chessPiece != null)
            {
                Unit piece = (Unit)spaces[(int)position.X - 1][(int)position.Y + y].chessPiece;
                if (piece.pieceID == pawn)
                {
                    return true;
                }
            }
#endregion

            #region tower/queen
            // next, check for towers/queen
            // no towers/queen should be in a straight line with the king horizontally or 
            // vertically, without any other pieces in between
            for (int i = (int)position.X+1; i < spaces.Count; i++)
            {
                Unit piece = (Unit)spaces[i][(int)position.Y].chessPiece;
                if ((piece != null && piece.pieceID != queen) && (piece != null && piece.pieceID != tower))
                {
                    break; // any other piece found, so protected, break.
                }
                else if ((piece != null && piece.pieceID == queen) || (piece != null && piece.pieceID == tower))
                {
                    return true; // queen or tower found
                }
                else 
                {
                    continue; // just an empty space
                }
            }
            for (int i = (int)position.X-1; i >= 0; i--)
            {
                Unit piece = (Unit)spaces[i][(int)position.Y].chessPiece;
                if ((piece != null && piece.pieceID != queen) && (piece != null && piece.pieceID != tower))
                {
                    break;// any other piece found, so protected, break.
                }
                else if ((piece != null && piece.pieceID == queen) || (piece != null && piece.pieceID == tower))
                {
                    return true; // queen or tower found
                }
                else
                {
                    continue; // just an empty space
                }
            }
            for (int i = (int)position.Y+1; i < spaces[0].Count; i++)
            {
                Unit piece = (Unit)spaces[(int)position.X][i].chessPiece;
                if ((piece != null && piece.pieceID != queen) && (piece != null && piece.pieceID != tower))
                {
                    break;// any other piece found, so protected, break.
                }
                else if ((piece != null && piece.pieceID == queen) || (piece != null && piece.pieceID == tower))
                {
                    return true; // queen or tower found
                }
                else
                {
                    continue; // just an empty space
                }
            }
            for (int i = (int)position.Y-1; i >= 0; i--)
            {
                Unit piece = (Unit)spaces[(int)position.X][i].chessPiece;
                if ((piece != null && piece.pieceID != queen) && (piece != null && piece.pieceID != tower))
                {
                    break;// any other piece found, so protected, break.
                }
                else if ((piece != null && piece.pieceID == queen) || (piece != null && piece.pieceID == tower))
                {
                    return true; // queen or tower found
                }
                else
                {
                    continue; // just an empty space
                }
            }
#endregion

            #region horse
            // Next, check for horse
            // start by checking wether there's any possibility of horses being in range
            if ((position.X + 2) < spaces.Count)
            { // check the RIGHT side of the king
                if ((position.Y + 1) < spaces[0].Count)
                {
                    Unit piece = (Unit)spaces[(int)position.X + 2][(int)position.Y + 1].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        return true;
                    }
                }
                if ((position.Y - 1)  >= 0)
                {
                    Unit piece = (Unit)spaces[(int)position.X + 2][(int)position.Y - 1].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        return true;
                    }
                }
            }
            if ((position.X - 2) >= 0)
            { // check the LEFT side of the king
                if ((position.Y + 1) < spaces[0].Count)
                {
                    Unit piece = (Unit)spaces[(int)position.X - 2][(int)position.Y + 1].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        return true;
                    }
                }
                if ((position.Y - 1) >= 0)
                {
                    Unit piece = (Unit)spaces[(int)position.X - 2][(int)position.Y - 1].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        return true;
                    }
                }
            }
            if ((position.Y + 2) < spaces[0].Count)
            { // check the UPPER side of the king
                if ((position.Y + 1) < spaces[0].Count)
                {
                    Unit piece = (Unit)spaces[(int)position.X + 1][(int)position.Y + 2].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        return true;
                    }
                }
                if ((position.Y - 1) >= 0)
                {
                    Unit piece = (Unit)spaces[(int)position.X - 1][(int)position.Y + 2].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        return true;
                    }
                }
            }
            if ((position.Y - 2) > 0)
            { // check the LOWER side of the king
                if ((position.Y + 1) < spaces[0].Count)
                {
                    Unit piece = (Unit)spaces[(int)position.X + 1][(int)position.Y - 2].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        return true;
                    }
                }
                if ((position.Y - 1) >= 0)
                {
                    Unit piece = (Unit)spaces[(int)position.X - 1][(int)position.Y - 2].chessPiece;
                    if (piece != null && piece.pieceID == horse)
                    {
                        return true;
                    }
                }
            }
            #endregion

            #region bishop/queen
            // next, check for bishop/queen
            // Look at all diagonal spaces
            int posY = (int)position.Y+1;
            for (int i = (int)position.X+1; i < spaces.Count; i++)
            {   // check north-east first
                if (posY < spaces[0].Count)
                {
                    Unit piece = (Unit)spaces[i][posY].chessPiece;
                    if ((piece != null && piece.pieceID != bishop) && (piece != null && piece.pieceID != queen))
                    {
                        // any other piece is in-between
                        break;
                    }
                    else if ((piece != null && piece.pieceID == bishop) || (piece != null && piece.pieceID == queen))
                    {
                        return true; // queen or bishop
                    }
                    else
                    {
                        posY++;
                        continue; // empty space
                    }
                }
                else
                {
                    break; // reached the end of the board
                }
            }
            posY = (int)position.Y + 1;
            for (int i = (int)position.X - 1; i >= 0; i--)
            {   // check north-west
                if (posY < spaces[0].Count)
                {
                    Unit piece = (Unit)spaces[i][posY].chessPiece;
                    if ((piece != null && piece.pieceID != bishop) && (piece != null && piece.pieceID != queen))
                    {
                        // any other piece is in-between
                        break;
                    }
                    else if ((piece != null && piece.pieceID == bishop) || (piece != null && piece.pieceID == queen))
                    {
                        return true; // queen or bishop
                    }
                    else
                    {
                        posY++;
                        continue; // empty space
                    }
                }
                else
                {
                    break; // reached the end of the board
                }
                
            }
            posY = (int)position.Y - 1;
            for (int i = (int)position.X - 1; i >= 0; i--)
            {   // check south-west
                if (posY >= 0)
                {
                    Unit piece = (Unit)spaces[i][posY].chessPiece;
                    if ((piece != null && piece.pieceID != bishop) && (piece != null && piece.pieceID != queen))
                    {
                        // any other piece is in-between
                        break;
                    }
                    else if ((piece != null && piece.pieceID == bishop) || (piece != null && piece.pieceID == queen))
                    {
                        return true; // queen or bishop
                    }
                    else
                    {
                        posY--;
                        continue; // empty space
                    }
                }
                else
                {
                    break; // reached the end of the board
                }
                
            }
            posY = (int)position.Y - 1;
            for (int i = (int)position.X + 1; i < spaces.Count; i++)
            {   // check south-east
                if (posY >= 0)
                {
                    Unit piece = (Unit)spaces[i][posY].chessPiece;
                    if ((piece != null && piece.pieceID != bishop) && (piece != null && piece.pieceID != queen))
                    {
                        // any other piece is in-between
                        break;
                    }
                    else if ((piece != null && piece.pieceID == bishop) || (piece != null && piece.pieceID == queen))
                    {
                        return true; // queen or bishop
                    }
                    else
                    {
                        posY--;
                        continue; // empty space
                    }
                }
                else
                {
                    break; // reached the end of the board
                }
            }
            #endregion

            #region king
            // now check the king, validate all spaces around it
            for (int i = -1; i <= 1; i++)
            {
                Unit piece;
                if ((int)position.X - 1 >= 0) // check middle
                {
                    piece = (Unit)spaces[(int)position.X + i][(int)position.Y].chessPiece;
                    if (piece != null && piece.pieceID == king) { return true; }
                }
                if ((int)position.Y + 1 < spaces[0].Count) // check top
                {
                    piece = (Unit)spaces[(int)position.X + i][(int)position.Y + 1].chessPiece;
                    if (piece != null && piece.pieceID == king) { return true; }
                }
                if ((int)position.Y - 1 >= 0) // check bottom
                {
                    piece = (Unit)spaces[(int)position.X + i][(int)position.Y - 1].chessPiece;
                    if (piece != null && piece.pieceID == king) { return true; }
                }
            }

            #endregion
            
            return false;
        }


        /**
         * Returns a bool based on wether the king is in checkmate status.
         * Should be called after every turn to check if the game has ended.
         * */
        public bool checkMate(Point position, List<List<Placeholder>> spaces)
        {
            bool checkmate = false;

            if (check(position, spaces))
            {
                // confirmed that this king is 'check'
                // now verify every move this king kan do and see if still checked.

                // if that doesn't solve the problem, check if other pieces can block
                // first get the spaces that need to be blocked in order to resolve
                // a piece being able to attack the king.
                // try, for each available piece, to move to these positions. if any of them
                // are legal moves, it's not checkmate
            }

            return checkmate;
        }
    }
}
