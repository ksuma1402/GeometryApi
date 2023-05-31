using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryApi.Models
{
    public class RectangleResponse
    {
        public RectangleResponse()
        {
            lstRectangleModel = new List<RectangleModel>();
        }

        public List<RectangleModel> lstRectangleModel { get; set; }
    }
}
