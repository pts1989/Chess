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
	/// Interaction logic for MessageBox_Item.xaml
	/// </summary>
	public partial class MessageBox_Item_p1 : UserControl
	{
        public string Content { 
            get { 
            return textbox.Text; 
        } 
            set {
                textbox.Text = value;
            } 
        }

		public MessageBox_Item_p1(string text)
		{
			this.InitializeComponent();
            textbox.Text = text;
		}
	}
}