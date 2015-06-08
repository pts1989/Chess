using System;
using System.Collections.Generic;
using System.Text;
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
	/// Interaction logic for BlackPlaceholder.xaml
	/// </summary>
	public partial class Placeholder : UserControl
	{

        public UserControl chessPiece {
            get { 
                if(piece_control.Children.Count != 0){
                    return (UserControl)piece_control.Children[0];}
                else
                {
                    return null;
                }
            }
            
            set {
                // make sure providing a null will clear the array
                if (value == null)
                {
                    piece_control.Children.Clear();
                }
                else
                {
                    if (value.Parent == null)
                    {
                        piece_control.Children.Clear();
                        piece_control.Children.Add(value);
                    }
                }
            } 
        }
        

		public Placeholder()
		{
			this.InitializeComponent();
            
		}
	}
}