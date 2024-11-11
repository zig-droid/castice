using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RotaceCastic
{
    internal class ParticleSimulator
    {

        Canvas ParticleCanvas = new Canvas();
        private Point position;
        private Vector velocity;
        private double mass;
        private List<ParticleSimulator> particles = new List<ParticleSimulator>();
        public Point Position { get => position; set => position = value; }
        public Vector Velocity { get => velocity; set => velocity = value; }
        public double Mass { get => mass; set => mass = value; }
        public double Radius { get => radius; set => radius = value; }
        DispatcherTimer timer;
        private double radius;

        public ParticleSimulator(Point position, Vector velocity, double mass, double radius)
        {
            Position = position;
            Velocity = velocity;
            Mass = mass;
            Radius = radius;
        }

        public ParticleSimulator(Canvas particleCanvas)
        {
            ParticleCanvas = particleCanvas;
        }

        public void UpdatePosition()
        {
            Position += Velocity;
        }
        private Vector CalculateGravity(ParticleSimulator p1, ParticleSimulator p2)
        {
            const double G = 6.67430e-11; // gravitační konstanta

            Vector direction = p2.Position - p1.Position;
            double distance = direction.Length;
            if (distance == 0) return new Vector(0, 0);
            // G = (m1*m2)/r^2
            double forceMagnitude = G * p1.Mass * p2.Mass / (distance * distance);
            Vector force = direction;
            force.Normalize();
            force *= forceMagnitude;

            return force;
        }
        public void UpdateParticles(List<ParticleSimulator> particles)
        {
            foreach (var p1 in particles)
            {
                Vector netForce = new Vector(0, 0);
                foreach (var p2 in particles)
                {
                    if (p1 == p2) continue;
                    netForce += CalculateGravity(p1, p2);
                }

                // Aktualizace rychlosti částice
                p1.Velocity += netForce / p1.Mass;
                p1.UpdatePosition();
            }
        }
        public void RenderPaarticles(List<ParticleSimulator> particles)
        {
            ParticleCanvas.Children.Clear();
            foreach (var particle in particles)
            {
                Ellipse ellipse = new Ellipse
                {
                    Width = 5,
                    Height = 5,
                    Fill = Brushes.White,
                };
                Canvas.SetLeft(ellipse, particle.Position.X);
                Canvas.SetTop(ellipse, particle.Position.Y);
                ParticleCanvas.Children.Add(ellipse);
            }
        }
        public void AddParticle(ParticleSimulator particle)
        {
            particles.Add(particle);
        }
        public void Start()
        {
            timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(16) };
            timer.Tick += (s, e) =>
            {
                UpdateParticles(particles);
                RenderPaarticles(particles);
            };
            timer.Start();
        }
        public void Stop()
        {
            timer = new DispatcherTimer { Interval = TimeSpan.Zero };
            timer.Tick -= (s, e) =>
            {
                UpdateParticles(particles);
                RenderPaarticles(particles);
            };
            timer.Stop();
        }
    }
}
