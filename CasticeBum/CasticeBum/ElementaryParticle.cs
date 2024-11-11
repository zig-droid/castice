using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasticeBum
{
    internal class ElementaryParticle : Particle
    {
        public ElementaryParticle(double x, double y, double vX, double vY) 
            : base (x, y, vX, vY)
        {
            Radius = 3;
        }
    }
}
