using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace CasticeBum
{
    internal class Particle
    {
        private double x;
        private double y;
        private double vX;
        private double vY;
        private double radius = 10;
        Random random = new Random();
        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public double VX { get => vX; set => vX = value; }
        public double VY { get => vY; set => vY = value; }
        public double Radius { get => radius; set => radius = value; }
        public Ellipse Shape {  get; set; }

        public Particle(double x, double y, double vX, double vY)
        {
            X = x;
            Y = y;           
            VX = vX;
            VY = vY;
        }
    }
}
