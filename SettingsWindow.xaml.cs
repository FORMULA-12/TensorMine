using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

namespace TensorMine
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
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
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void VersionButton_Checked(object sender, RoutedEventArgs e)
        {
            VersionTextBlock.Text = "OptiFine";
        }

        private void VersionButton_Unchecked(object sender, RoutedEventArgs e)
        {
            VersionTextBlock.Text = "Classic";
        }

        private void ExtraGraphicsButton_Checked(object sender, RoutedEventArgs e)
        {
            ExtraGraphicsTextBlock.Text = "Включено";
        }

        private void ExtraGraphicsButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ExtraGraphicsTextBlock.Text = "Выключено";
        }

        private void CookieButton_Checked(object sender, RoutedEventArgs e)
        {
            CookieTextBlock.Text = "Включено";
        }

        private void CookieButton_Unchecked(object sender, RoutedEventArgs e)
        {
            CookieTextBlock.Text = "Выключено";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Owner.Effect = null;
        }

        private void RAMTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void LinkButton_Checked(object sender, RoutedEventArgs e)
        {
            LinkTextBox.Text = "Создан";

            try
            {

            }
            catch
            {

            }
        }

        private void LinkButton_Unchecked(object sender, RoutedEventArgs e)
        {
            LinkTextBox.Text = "Создать";
        }
    }
}

