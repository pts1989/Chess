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
	/// Interaction logic for Player_MessageBox.xaml
	/// </summary>
	public partial class Player_MessageBox : UserControl
	{
		public Player_MessageBox()
		{
			this.InitializeComponent();
		}

        public void addMessage(MessageBox_Item_p1 item)
        {
            LayoutRoot.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });
            item.SetValue(Grid.RowProperty, LayoutRoot.RowDefinitions.Count-1);
            LayoutRoot.Children.Add(item);
        }

        public void addMessage(MessageBox_Item_p2 item)
        {
            LayoutRoot.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(20) });
            item.SetValue(Grid.RowProperty, LayoutRoot.RowDefinitions.Count-1);
            LayoutRoot.Children.Add(item);
        }
	}
}