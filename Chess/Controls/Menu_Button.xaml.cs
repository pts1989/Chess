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
	/// Interaction logic for Menu_Button.xaml
	/// </summary>
	public partial class Menu_Button : UserControl
	{
        public string Text
        {
            get { return this.buttoncontent.Text; }
            set { this.buttoncontent.Text = value; }
        }
		public Menu_Button()
		{
			this.InitializeComponent();
            
		}
	}
}