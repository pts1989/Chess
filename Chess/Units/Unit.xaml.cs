using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chess.Units
{
    /// <summary>
    /// Interaction logic for Unit.xaml
    /// </summary>
    public partial class Unit : UserControl
    {
        public int pieceID { get; set; }
        public string pieceType { get; set; }
        public Point origin { get; set; }

        public Unit()
        {
            InitializeComponent();
        }

        /**
         *  Returns the piece type specified in derived classes
         * */
        public string getPieceType()
        {
            return pieceType;
        }

        /**
         *  Virtual method whose default behaviour is to always validate a move as true
         *  supposed to be overridden in derived classes.
         * */
        public virtual bool validateMove(Point dest, List<List<Placeholder>> spaces)
        {
            return true;
        }

        /**
         * Validates wether the target destination contains no piece, an enemy piece
         * or a piece of our own.
         * */
        protected bool validateDestination(Placeholder dest)
        {
            Unit target = null;
            // check if the destination actually has a chesspiece
            if(dest.chessPiece != null){
                target = (Unit)dest.chessPiece;

                // check if its one of our own
                if (pieceID <= 6 && target.pieceID <= 6)
                {
                    return false;
                }
                else if (pieceID > 6 && target.pieceID > 6)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }

            
        }

        /**
         *  Returns a point that shows the difference between the 2 given Points.
         * */
        protected Point getMovementCoordinates(Point start, Point end)
        {
            return new Point(end.X - start.X, end.Y - start.Y);
        }
    }
}
