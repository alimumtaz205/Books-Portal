using BookPortalAPI.Models.Finance;
using BookPortalAPI.Models.Finance.Request;
using BookPortalAPI.Models.Finance.Response;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortalAPI.Repositories
{
    public class FinanceRepository : IFinanceRepository
    {
        private readonly IConfiguration _configuration;

        bool isSuccess = false;
        string Message = string.Empty;
        //TranCodes tranCode = TranCodes.Exception;

        public FinanceRepository(IConfiguration configuration)
        {
            //_GlobalInfo = apiGlobalInfo;
            _configuration = configuration;
        }

        public GetFinanceResponse GetFinance()
        {
            /* ........................................................................
              PROCEDURE `PCR_FINANCE_GET`(
                        OUT PCODE varchar(20),
                        OUT PDESC varchar(1000),
						OUT PMSG  varchar(20)                                               
)
           ........................................................................ */
            DataTable dt = new DataTable();
            MySqlCommand cmd = null;
            MySqlConnection con = null;

            List<FinanceModel> ListUsers = new List<FinanceModel>();

            try
            {
                //var aa = _configuration.GetConnectionString("CONN_STR");
                using (cmd = new MySqlCommand("PCR_FINANCE_GET", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PCODE", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PDESC", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PMSG", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });

                    con.Open();
                    MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                    myDA.Fill(dt);
                    con.Close();


                    if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
                    {
                        if (dt != null && dt.Rows.Count > 0)
                        {

                            foreach (DataRow dr in dt.Rows)
                            {
                                FinanceModel obj = new FinanceModel();

                                obj.financeId = Convert.ToInt32(dr["financeId"]);
                                obj.bookId = Convert.ToInt32(dr["bookId"]);
                                obj.authorId = Convert.ToInt32(dr["authorId"]);
                                obj.totalSale = Convert.ToString(dr["totalSale"]);
                                obj.revenue = Convert.ToString(dr["revenue"]);
                                ListUsers.Add(obj);
                            }
                        }
                        isSuccess = true;
                        //tranCode = TranCodes.Success;
                        Message = Convert.ToString(cmd.Parameters["PDESC"].Value);
                    }
                    else
                    {
                        Message = Convert.ToString(cmd.Parameters["PDESC"].Value);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (con != null) { con.Close(); con.Dispose(); }
            }
            return new GetFinanceResponse { IsSuccess = isSuccess, Message = Message, Data = ListUsers };
        }

        public AddFinanceResponse AddFinance(AddFinanceRequest request)
        {
            /*.........................................................................
                      PROCEDURE `PRC_FINANCE_ADD`(
						IN P_FINANCEID int(100),
                        IN P_BookID int(100),
                        IN P_AUTHORID int(100),
                        IN P_TOTALSALE varchar(1000),
                        IN P_REVENUE varchar(100),
                        OUT PCODE varchar(20),
                        OUT PDESC varchar(1000),
						OUT PMSG  varchar(20)                                                  
                                                    )
             .............................................................................*/
            #region variable  

            DataTable dt = new DataTable();
            MySqlCommand cmd = null;
            MySqlConnection con = null;
            bool isSuccess = false;
            string Message = string.Empty;
            // TranCodes tranCode = TranCodes.Exception;

            #endregion
            try
            {
                var aa = _configuration.GetConnectionString("CONN_STR");
                using (cmd = new MySqlCommand("PRC_FINANCE_ADD", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //input
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_FINANCEID", Value = request.financeId, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_BookID", Value = request.bookId, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_AUTHORID", Value = request.authorId, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_TOTALSALE", Value = request.totalSale, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_REVENUE", Value = request.revenue, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });

                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PCODE", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PDESC", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PMSG", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
                    {
                        isSuccess = true;
                        // tranCode = TranCodes.Success;
                        Message = Convert.ToString(cmd.Parameters["PDESC"].Value);
                    }
                    else
                    {
                        Message = Convert.ToString(cmd.Parameters["PDESC"].Value);
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (con != null) { con.Close(); con.Dispose(); }
            }

            return new AddFinanceResponse { IsSuccess = isSuccess, Message = Message };

            //  }

            //  public DeleteUserResponse DeleteUser(DeleteUserRequest request)
            //  {
            //      /*.............................................................
            //            PROCEDURE `PRC_USERS_DELETE`(
            //                 IN USERNAME varchar(100),
            //                 OUT PCODE varchar(200),
            //                 OUT PDESC varchar(1000),
            //                 OUT PMSG  varchar(200)  
            //          ............................................................. */

            //      #region Variables
            //      DataTable dt = new DataTable();
            //      MySqlCommand cmd = null;
            //      MySqlConnection con = null;
            //      bool isSuccess = false;
            //      string Message = string.Empty;
            //      TranCodes tranCode = TranCodes.Exception;

            //      #endregion 

            //      try
            //      {
            //          var aa = _configuration.GetConnectionString("CONN_STR");
            //          using (cmd = new MySqlCommand("PRC_USERS_DELETE", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
            //          {
            //              cmd.CommandType = CommandType.StoredProcedure;
            //              //input
            //              cmd.Parameters.Add(new MySqlParameter { ParameterName = "USERNAME", Value = request.userName, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 1000 });

            //              //output
            //              cmd.Parameters.Add(new MySqlParameter { ParameterName = "pCODE", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 100 });
            //              cmd.Parameters.Add(new MySqlParameter { ParameterName = "pDESC", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 100 });
            //              cmd.Parameters.Add(new MySqlParameter { ParameterName = "pMSG", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 100 });

            //              con.Open();
            //              cmd.ExecuteNonQuery();
            //              con.Close();

            //              if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
            //              {
            //                  isSuccess = true;
            //                  tranCode = TranCodes.Success;
            //                  Message = Convert.ToString(cmd.Parameters["PDESC"].Value);
            //              }
            //              else
            //              {
            //                  Message = Convert.ToString(cmd.Parameters["PDESC"].Value);
            //              }

            //          }
            //      }
            //      catch (Exception)
            //      {
            //          throw;
            //      }
            //      finally
            //      {
            //          if (con != null) { con.Close(); con.Dispose(); }
            //      }

            //      return new DeleteUserResponse { IsSuccess = isSuccess, Message = Message };

            //  }

            //  public UpdateUserResponse UpdateUser(UpdateUserRequest request)
            //  {

            //      /*.........................................................................
            //                PROCEDURE `PRC_USER_UPDATE`(
            //IN P_USERNAME varchar(1000),
            //                  IN P_FULLNAME varchar(1000),
            //                  OUT PCODE varchar(20),
            //                  OUT PDESC varchar(1000),
            //OUT PMSG  varchar(20) 
            //       .............................................................................*/


            //      #region variable  

            //      DataTable dt = new DataTable();
            //      MySqlCommand cmd = null;
            //      MySqlConnection con = null;
            //      bool isSuccess = false;
            //      string Message = string.Empty;
            //      TranCodes tranCode = TranCodes.Exception;

            //      #endregion


            //      try
            //      {
            //          var aa = _configuration.GetConnectionString("CONN_STR");
            //          using (cmd = new MySqlCommand("PRC_USER_UPDATE", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
            //          {
            //              cmd.CommandType = CommandType.StoredProcedure;

            //              //input
            //              cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_USERNAME", Value = request.userName, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
            //              cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_FULLNAME", Value = request.fullName, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });


            //              cmd.Parameters.Add(new MySqlParameter { ParameterName = "PCODE", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });
            //              cmd.Parameters.Add(new MySqlParameter { ParameterName = "PDESC", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });
            //              cmd.Parameters.Add(new MySqlParameter { ParameterName = "PMSG", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });


            //              con.Open();
            //              cmd.ExecuteNonQuery();
            //              con.Close();

            //              if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
            //              {
            //                  isSuccess = true;
            //                  tranCode = TranCodes.Success;
            //                  Message = Convert.ToString(cmd.Parameters["PDESC"].Value);
            //              }
            //              else
            //              {
            //                  Message = Convert.ToString(cmd.Parameters["PDESC"].Value);
            //              }

            //          }
            //      }
            //      catch (Exception)
            //      {
            //          throw;
            //      }
            //      finally
            //      {
            //          if (con != null) { con.Close(); con.Dispose(); }
            //      }

            //      return new UpdateUserResponse { IsSuccess = isSuccess, Message = Message };

            //  }

        }

        public DeleteFinanceResponse DeleteFinance(DeleteFinanceRequest request)
        {
            /*.............................................................
                 PROCEDURE `PCR_FINANCE_DELETE`(
						IN P_FINANCEID varchar(1000),
                        OUT PCODE varchar(20),
                        OUT PDESC varchar(1000),
						OUT PMSG  varchar(20))
                ............................................................. */

            #region Variables
            DataTable dt = new DataTable();
            MySqlCommand cmd = null;
            MySqlConnection con = null;
            bool isSuccess = false;
            string Message = string.Empty;
           // TranCodes tranCode = TranCodes.Exception;

            #endregion 

            try
            {
                var aa = _configuration.GetConnectionString("CONN_STR");
                using (cmd = new MySqlCommand("PCR_FINANCE_DELETE", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //input
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_FINANCEID", Value = request.financeId, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 1000 });

                    //output
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "pCODE", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "pDESC", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "pMSG", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 100 });

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
                    {
                        isSuccess = true;
                        //tranCode = TranCodes.Success;
                        Message = Convert.ToString(cmd.Parameters["PDESC"].Value);
                    }
                    else
                    {
                        Message = Convert.ToString(cmd.Parameters["PDESC"].Value);
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (con != null) { con.Close(); con.Dispose(); }
            }

            return new DeleteFinanceResponse { IsSuccess = isSuccess, Message = Message };

        }

        public UpdateFinanceResponse UpdateFinance(UpdateFinanceRequest request)
        {

            /*.........................................................................
                      PROCEDURE `PRC_FINANCE_UPDATE`(
						IN P_FINANCEID varchar(1000),
                        IN P_TOTALSALE varchar(1000),
                        IN P_REVENUE varchar(100),
                        OUT PCODE varchar(20),
                        OUT PDESC varchar(1000),
						OUT PMSG  varchar(20)   
            )
             .............................................................................*/


            #region variable  

            DataTable dt = new DataTable();
            MySqlCommand cmd = null;
            MySqlConnection con = null;
            bool isSuccess = false;
            string Message = string.Empty;
           // TranCodes tranCode = TranCodes.Exception;

            #endregion


            try
            {
                var aa = _configuration.GetConnectionString("CONN_STR");
                using (cmd = new MySqlCommand("PRC_FINANCE_UPDATE", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //input
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_FINANCEID", Value = request.financeId, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_TOTALSALE", Value = request.totalSale, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_REVENUE", Value = request.revenue, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });


                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PCODE", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PDESC", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PMSG", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
                    {
                        isSuccess = true;
                       // tranCode = TranCodes.Success;
                        Message = Convert.ToString(cmd.Parameters["PDESC"].Value);
                    }
                    else
                    {
                        Message = Convert.ToString(cmd.Parameters["PDESC"].Value);
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (con != null) { con.Close(); con.Dispose(); }
            }

            return new UpdateFinanceResponse { IsSuccess = isSuccess, Message = Message };

        }

    }
}
