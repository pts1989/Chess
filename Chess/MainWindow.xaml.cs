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

namespace Chess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // initialise
            initialise_Board();
        }



        // Method that clears the board, then places all white and black pieces
        // at their respective starting positions
        public void initialise_Board()
        {
            
            UIElementCollection col = boardArea.Children;
            List<List<Placeholder>> spaces = new List<List<Placeholder>>();
            for (int i = 0; i < 8; i++)
            {
                // loop through columns A - H
                spaces.Add(new List<Placeholder>());
                for (int j = 0; j < 8; j++)
                {
                    spaces[i].Add((Placeholder)col[j + (8*i)]);
                }
            }

            // place pieces on board
            for (int i = 0; i < 8; i++)
            {
                // place pawns on 2nd rows from each side
                spaces[i][1].chessImage.Source = Chess.Utilities.Chesspieces.white_pawn;
                spaces[i][6].chessImage.Source = Chess.Utilities.Chesspieces.black_pawn;
            }
            for (int i = 0; i < 8; i++)
            {
                BitmapImage image_white = null, image_black = null;
                switch (i)
                {
                    case 0: // tower
                        image_white = Chess.Utilities.Chesspieces.white_tower;
                        image_black = Chess.Utilities.Chesspieces.black_tower;
                        break;
                    case 1:
                        image_white = Chess.Utilities.Chesspieces.white_horse;
                        image_black = Chess.Utilities.Chesspieces.black_horse;
                        break;
                    case 2:
                        image_white = Chess.Utilities.Chesspieces.white_bishop;
                        image_black = Chess.Utilities.Chesspieces.black_bishop;
                        break;
                    case 3:
                        image_white = Chess.Utilities.Chesspieces.white_queen;
                        image_black = Chess.Utilities.Chesspieces.black_queen;
                        break;
                    case 4:
                        image_white = Chess.Utilities.Chesspieces.white_king;
                        image_black = Chess.Utilities.Chesspieces.black_king;
                        break;
                    case 5:
                        image_white = Chess.Utilities.Chesspieces.white_bishop;
                        image_black = Chess.Utilities.Chesspieces.black_bishop;
                        break;
                    case 6:
                        image_white = Chess.Utilities.Chesspieces.white_horse;
                        image_black = Chess.Utilities.Chesspieces.black_horse;
                        break;
                    case 7:
                        image_white = Chess.Utilities.Chesspieces.white_tower;
                        image_black = Chess.Utilities.Chesspieces.black_tower;
                        break;
                }
                spaces[i][0].chessImage.Source = image_white;
                spaces[i][7].chessImage.Source = image_black;
            }
                
        }


        private ImageSource draggedImage;
        private Point mousePosition;
        private void placeholder_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var src = (Placeholder)e.Source;
            var image = src.chessImage.Source;

            if (image != null)
            {
                mousePosition = e.GetPosition(boardArea);
                draggedImage = image;
                src.chessImage.Source = null;
            }
        }

        private void placeholder_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (draggedImage != null)
            {
                var src = (Placeholder)e.Source;
                src.chessImage.Source = draggedImage;
            }
        }
    }
}
