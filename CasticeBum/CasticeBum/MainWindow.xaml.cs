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
using System.Windows.Threading;

namespace CasticeBum
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private Particle _particle1;
        private Particle _particle2;
        private Particle _particle3;
        private Particle _particle4;
        private Particle _particle5;

        private List<ElementaryParticle> _elementaryParticles = new List<ElementaryParticle>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeParticles();
            InitializeTimer();

        }
        private void InitializeParticles()
        {
            _particle1 = new Particle(580, 350, -4, -1);
            _particle2 = new Particle(680, 250, 3.5, 2);
            _particle3 = new Particle(480, 650, 4, -1);
            _particle4 = new Particle(680, 450, 1, 2);
            _particle5 = new Particle(800, 750, -1, -1);

            DrawParticle(_particle1);
            DrawParticle(_particle2);
            DrawParticle(_particle3);
            DrawParticle(_particle4);
            DrawParticle(_particle5);
        }

        private void InitializeTimer()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(20)
            };
            _timer.Tick += Update;
            _timer.Start();
        }

        private void Update(object sender, EventArgs e)
        {
            MoveParticle(_particle1);
            MoveParticle(_particle2);
            MoveParticle(_particle3);
            MoveParticle(_particle4);
            MoveParticle(_particle5);
            CheckCollision();

            // Pohyb elementárních částic
            foreach (var ep in _elementaryParticles)
            {
                MoveParticle(ep);
            }
        }

        private void MoveParticle(Particle particle)
        {
            particle.X += particle.VX;
            particle.Y += particle.VY;
            Canvas.SetLeft(particle.Shape, particle.X);
            Canvas.SetTop(particle.Shape, particle.Y);
        }

        private void CheckCollision()
        {
            double dx = _particle2.X - _particle1.X;
            double dy = _particle2.Y - _particle1.Y;
            double distance = Math.Sqrt(dx * dx + dy * dy);

            if (distance < _particle1.Radius + _particle2.Radius)
            {
                
                // Rozpad částic na elementární částice
                CreateElementaryParticles(_particle1.X, _particle1.Y);

                // Skryjeme původní částice
                _particle1.Shape.Visibility = Visibility.Hidden;
                _particle2.Shape.Visibility = Visibility.Hidden;
                _particle3.Shape.Visibility = Visibility.Hidden;
                _particle4.Shape.Visibility = Visibility.Hidden;
                _particle5.Shape.Visibility = Visibility.Hidden;
            }
        }

        private void CreateElementaryParticles(double x, double y)
        {
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                double angle = random.NextDouble() * 2 * Math.PI;
                double speed = random.Next(1, 3);
                double vx = Math.Cos(angle) * speed;
                double vy = Math.Sin(angle) * speed;

                var elementaryParticle = new ElementaryParticle(x, y, vx, vy);
                DrawParticle(elementaryParticle);
                _elementaryParticles.Add(elementaryParticle);
            }
        }

        private void DrawParticle(Particle particle)
        {
            particle.Shape = new Ellipse
            {
                Width = particle.Radius * 2,
                Height = particle.Radius * 2,
                Fill = particle is ElementaryParticle ? Brushes.Red : Brushes.Blue
            };

            MyCanvas.Children.Add(particle.Shape);
            Canvas.SetLeft(particle.Shape, particle.X);
            Canvas.SetTop(particle.Shape, particle.Y);
            Canvas.SetRight(particle.Shape, particle.X);
        }
    }
}
