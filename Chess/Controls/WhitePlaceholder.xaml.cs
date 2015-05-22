﻿using System;
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
	/// Interaction logic for WhitePlaceholder.xaml
	/// </summary>
	public partial class WhitePlaceholder : UserControl
	{
        public Image chessPiece
        {
            get { return chessImage; }
            set { chessImage = value; }
        }

		public WhitePlaceholder()
		{
			this.InitializeComponent();
		}
	}
}