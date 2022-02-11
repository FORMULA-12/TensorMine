using System;
using System.Windows;
using System.Windows.Controls;

namespace TensorMine
{
    public partial class ExitWindow : Window
    {
        public ExitWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            this.Cursor = ((TextBlock)this.Resources["MainCursor"]).Cursor;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.Owner.Close();
        }

        private void StayButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.Owner.Effect = null;
        }
    }
}

