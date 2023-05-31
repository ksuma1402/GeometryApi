using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryApi.Models
{
    public class RectangleCoOrd
    {
        public int RectangleCoOrdId { get; set; }
        public float XAxis { get; set; }
        public float YAxis { get; set; }
        public string Vertices { get; set; }
        public int RectangleId { get; set; }
    }
}
