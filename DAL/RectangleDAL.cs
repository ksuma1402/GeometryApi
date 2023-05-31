using GeometryApi.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GeometryApi.DAL
{
    public class RectangleDAL
    {
        private SqlAccess _sqlAccess { get; set; }
        public RectangleDAL()
        {
            _sqlAccess = new SqlAccess();
        }

        public int CreateRectangle(Rectangle rectangleModel)
        {
            int returnValue = 0;
            try
            {
                // define INSERT query with parameters
                string query = "Insert into dbo.Rectangle  (RectanglName) OUTPUT INSERTED.RentangleId  " +
                               "VALUES (@RectangleName) ";
                // _sqlAccess.GetConnection();
                //initiate the adapter
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add("@RectangleName", SqlDbType.VarChar, 50).Value = rectangleModel.RectangleName;
                returnValue = _sqlAccess.ExecuteScalar(query, cmd);
                return returnValue;
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                //close the connection
                //  _sqlAccess.CloseConnection();   
            }
        }

        public int CreateRectangleCoOrd(RectangleCoOrd rectangleCoOrdModel)
        {
            int returnValue = 0;
            try
            {
                // define INSERT query with parameters
                string query = "Insert into dbo.RectangleCoOrd (XAxis, YAxis, Vertices, RectangleId) " +
                               "VALUES (@XAxis, @YAxis, @Vertices, @RectangleId) ";
                // _sqlAccess.GetConnection();
                //initiate the adapter
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add("@XAxis", SqlDbType.Float).Value = rectangleCoOrdModel.XAxis;
                cmd.Parameters.Add("@YAxis", SqlDbType.Float).Value = rectangleCoOrdModel.YAxis;
                cmd.Parameters.Add("@Vertices", SqlDbType.NVarChar).Value = rectangleCoOrdModel.Vertices;
                cmd.Parameters.Add("@RectangleId", SqlDbType.Int).Value = rectangleCoOrdModel.RectangleId;
                returnValue = _sqlAccess.ExecuteNonQuery(query, cmd);
                return returnValue;
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                //close the connection
                //  _sqlAccess.CloseConnection();

            }

        }

        public List<RectangleModel> SearchCoOrdinates(List<RectangleCoOrd> lstRectangularCoOrd)
        {
            List<RectangleModel> lstRectangleModel = new List<RectangleModel>();
            for (int j = 0; j < lstRectangularCoOrd.Count; j++)
            {

                DataSet ds = _sqlAccess.ExecuteDataSet("select * from dbo.RectangleCoOrd where XAxis = " + lstRectangularCoOrd[j].XAxis + " and YAxis = " + lstRectangularCoOrd[j].YAxis + "and Vertices = '" + lstRectangularCoOrd[j].Vertices + "'");

                RectangleModel rectangleModel = new RectangleModel();
                RectangleCoOrd rectangleCoOrd = new RectangleCoOrd();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                    {
                        rectangleCoOrd.RectangleCoOrdId = Convert.ToInt32(ds.Tables[0].Rows[k][0]);
                        rectangleCoOrd.XAxis =  Convert.ToSingle(ds.Tables[0].Rows[k][1]);
                        rectangleCoOrd.YAxis = Convert.ToSingle(ds.Tables[0].Rows[k][2]);
                        rectangleCoOrd.Vertices = Convert.ToString(ds.Tables[0].Rows[k][3]);
                        rectangleModel.objRectangleCoOrd.Add(rectangleCoOrd);
                    }
                    lstRectangleModel.Add(rectangleModel);
                }
            }
            return lstRectangleModel;
        }

        public int DeleteRectangleCoOrdinates()
        {
            int returnValue = 0;
            try
            {
                string query = "Delete from dbo.RectangleCoOrd; Delete from  dbo.Rectangle;";
                // _sqlAccess.GetConnection();   
                SqlCommand cmd = new SqlCommand();
                returnValue = _sqlAccess.ExecuteNonQuery(query, cmd);
                return returnValue;
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                //close the connection
                //  _sqlAccess.CloseConnection();

            }

        }
    }
}
