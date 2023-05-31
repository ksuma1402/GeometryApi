using GeometryApi.DAL;
using GeometryApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RectangleController : Controller
    {
        public RectangleDAL rectangleDAL;
        public RectangleController()
        {
            rectangleDAL = new RectangleDAL();
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            return Ok("Hello");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectangleModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[controller]/[action]")]
        public IActionResult CreateRectangleWithCoOrdinates(RectangleModel rectangleModel)
        {
            int resultRectangleId;
            int result = -1;
            resultRectangleId = InsertRectangle(rectangleModel.objRectangle);
            if (resultRectangleId > 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    rectangleModel.objRectangleCoOrd[i].RectangleId = resultRectangleId;
                    result = rectangleDAL.CreateRectangleCoOrd(rectangleModel.objRectangleCoOrd[i]);
                }
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public int InsertRectangle(Rectangle rectangle)
        {
            int result = rectangleDAL.CreateRectangle(rectangle);
            return result;
        }
        [HttpGet]
        [Route("[controller]/[action]")]
        public int InsertRectangleCoOrd(RectangleCoOrd rectangleCoOrd)
        {
            int result = rectangleDAL.CreateRectangleCoOrd(rectangleCoOrd);
            return result;
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public IActionResult SearchRectangleCoOrdinates(RectangleModel rectangleModel)
        {
            List<RectangleResponse> lstRectangleResponse = new List<RectangleResponse>();
            List<RectangleModel> result = new List<RectangleModel>();
            for (int i = 0; i < rectangleModel.objRectangleCoOrd.Count; i++)
            {
                result = rectangleDAL.SearchCoOrdinates(rectangleModel.objRectangleCoOrd);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        public IActionResult Insert200RectangleCoOrdinates()
        {
            int resultRectangleId;
            int result = -1;

            RectangleModel rectangleModel = new RectangleModel();


            for (int j = 0; j < 200; j++)
            {
                rectangleModel.objRectangle.RectangleName = "Rectangle " + j + " Auto Generated";
                resultRectangleId = InsertRectangle(rectangleModel.objRectangle);
                if (resultRectangleId > 0)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        RectangleCoOrd rectangleCoOrd = new RectangleCoOrd();
                        rectangleCoOrd.RectangleId = resultRectangleId;
                        rectangleCoOrd.XAxis = j * 0.5f;
                        rectangleCoOrd.YAxis = j * 1f;
                        switch (i)
                        {
                            case 0:
                                rectangleCoOrd.Vertices = "A";
                                break;
                            case 1:
                                rectangleCoOrd.Vertices = "B";
                                break;
                            case 2:
                                rectangleCoOrd.Vertices = "C";
                                break;
                            case 3:
                                rectangleCoOrd.Vertices = "D";
                                break;
                            default:
                                rectangleCoOrd.Vertices = "Default";
                                break;
                        }
                        rectangleModel.objRectangleCoOrd.Add(rectangleCoOrd);
                        result = rectangleDAL.CreateRectangleCoOrd(rectangleCoOrd);

                    }
                }
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("[controller]/[action]")]
        public IActionResult DeleteRectangleCoOrdinates()
        {
            int result = -1;
            result = rectangleDAL.DeleteRectangleCoOrdinates();

            return Ok(result);
        }
    }
}
