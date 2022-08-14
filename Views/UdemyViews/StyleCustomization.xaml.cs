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
using System.Windows.Shapes;

namespace ReserRoom.Views.UdemyViews
{
    /// <summary>
    /// Lógica de interacción para StyleCustomization.xaml
    /// </summary>
    public partial class StyleCustomization : Window
    {
        public StyleCustomization()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)e.Source;
            button.Background = Brushes.Chocolate;
            MessageBox.Show("Testing");

            var element = (FrameworkElement)e.Source;
            if (TestContainer.Children.Contains(element))
            {
                TestContainer.Children.Remove(element);
            }

        }
    }
}
