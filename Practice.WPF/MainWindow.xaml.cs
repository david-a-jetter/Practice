using Practice.FizzBuzz;
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

namespace Practice.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FizzBuzzRunner _FizzBuzzRunner { get; }

        public MainWindow()
        {
            InitializeComponent();

            _FizzBuzzRunner = new FizzBuzzRunner(new FizzBuzzEvaluator());
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var maxValue = int.Parse(MaxValueInput.Text);

            var fizzBuzzResults = _FizzBuzzRunner.Run(0, maxValue);

            //FizzBuzzResultsDataGrid.ItemsSource = fizzBuzzResults;
        }
    }
}
