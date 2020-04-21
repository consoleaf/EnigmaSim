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
using EnigmaLib;

namespace WPFApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Button> inputButtons;
        List<Button> outputButtons;
        EnigmaLib.Enigma enigma;

        public MainWindow()
        {
            InitializeComponent();
            inputButtons = InputGrid.Children.OfType<Button>().ToList();
            outputButtons = OutputGrid.Children.OfType<Button>().ToList();
            foreach (Button button in inputButtons)
            {
                button.Click += InputButtonClickHandler;
            }
            this.enigma = new Enigma(PreMade.A, PreMade.III, PreMade.II, PreMade.I);
        }

        private void InputButtonClickHandler(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            EncodeLetter((btn.Content as String)[0]);
        }

        private void EncodeLetter(char letter)
        {
            log.Text += letter;  // TODO remove
            foreach (Button btn in inputButtons)
                if ((btn.Content as String)[0] != letter)
                    btn.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
                else
                    btn.Background = Brushes.Yellow;

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString().Length != 1)
                return;
            EncodeLetter(e.Key.ToString()[0]);
        }
    }
}
