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
    /// Lógica de interacción para Templates.xaml
    /// </summary>
    public partial class Templates : Window
    {
        public Templates()
        {
            InitializeComponent();
            var tasks = new List<Task>() {
                new Task() { Name = "Task 1" ,  Description="Just a tests.", Priority =1},
                new Task() { Name = "Task 2",  Description="Just a tests.", Priority = 2 },
                new Task() { Name = "Task 3" ,  Description="Just a tests.", Priority =3} };
            MyListBox.ItemsSource = tasks;
        }
    }
    public class Task
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Priority { get; set; } = default!;

        public override string ToString() => this.Name;
    }
}
