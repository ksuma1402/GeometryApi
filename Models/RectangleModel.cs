using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryApi.Models
{
    public class RectangleModel
    {
        public RectangleModel()
        {
            objRectangle = new Rectangle();
            objRectangleCoOrd = new List<RectangleCoOrd>();
        }
        public Rectangle objRectangle { get; set; }
        public List<RectangleCoOrd> objRectangleCoOrd { get; set; }

    }
}
