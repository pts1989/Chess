using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Chess.Utilities
{
    public static class Chesspieces
    {
        public static BitmapImage black_pawn = new BitmapImage(new Uri("/Chess;component/images/black_pawn.png", UriKind.Relative));
        public static BitmapImage black_bishop = new BitmapImage(new Uri("/Chess;component/images/black_bishops.png", UriKind.Relative));
        public static BitmapImage black_horse = new BitmapImage(new Uri("/Chess;component/images/black_horse.png", UriKind.Relative));
        public static BitmapImage black_king = new BitmapImage(new Uri("/Chess;component/images/black_king.png", UriKind.Relative));
        public static BitmapImage black_queen = new BitmapImage(new Uri("/Chess;component/images/black_queen.png", UriKind.Relative));
        public static BitmapImage black_tower = new BitmapImage(new Uri("/Chess;component/images/black_tower.png", UriKind.Relative));

        public static BitmapImage white_pawn = new BitmapImage(new Uri("/Chess;component/images/white_pawn.png", UriKind.Relative));
        public static BitmapImage white_bishop = new BitmapImage(new Uri("/Chess;component/images/white_bishops.png", UriKind.Relative));
        public static BitmapImage white_horse = new BitmapImage(new Uri("/Chess;component/images/white_horse.png", UriKind.Relative));
        public static BitmapImage white_king = new BitmapImage(new Uri("/Chess;component/images/white_king.png", UriKind.Relative));
        public static BitmapImage white_queen = new BitmapImage(new Uri("/Chess;component/images/white_queen.png", UriKind.Relative));
        public static BitmapImage white_tower = new BitmapImage(new Uri("/Chess;component/images/white_tower.png", UriKind.Relative));
    }
}
