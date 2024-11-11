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

namespace RotaceCastic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ParticleSimulator simulator;
        public MainWindow()
        {
            InitializeComponent();
            simulator = new ParticleSimulator(ParticleCanvas);

            // Přidání částic do simulace
            simulator.AddParticle(new ParticleSimulator(new Point(100, 100), new Vector(1, 1), 100, 10));
            simulator.AddParticle(new ParticleSimulator(new Point(200, 150), new Vector(-1, 0), 15, 10));
            
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            simulator.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            simulator.Stop();
        }
    }
}
