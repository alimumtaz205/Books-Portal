
using BookPortalAPI.Models;
using BookPortalAPI.Models.Users;
using BookPortalAPI.Models.Users.Request;
using BookPortalAPI.Models.Users.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BookPortalAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        bool isSuccess = false;
        string Message = string.Empty;
        //TranCodes tranCode = TranCodes.Exception;

        public UserRepository(IConfiguration configuration)
        {
            //_GlobalInfo = apiGlobalInfo;
            _configuration = configuration;
        }

        public GetUserResponse GetUser()
        {
            /* ........................................................................
               PROCEDURE `PRC_USERS_GET`(
                        IN USERNAME varchar(100),
                        OUT PCODE varchar(2),
                        OUT PDESC varchar(1000),
                        OUT PMSG  varchar(2)                                                  
)
           ........................................................................ */
            DataTable dt = new DataTable();
            MySqlCommand cmd = null;
            MySqlConnection con = null;


            List<Users> ListUsers = new List<Users>();

            try
            {
                //var aa = _configuration.GetConnectionString("CONN_STR");
                using (cmd = new MySqlCommand("PRC_USERS_GET", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
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
                                Users obj = new Users();

                                obj.userName = Convert.ToString(dr["userName"]);
                                obj.fullName = Convert.ToString(dr["fullName"]);
                                obj.uPassword = Convert.ToString(dr["uPassword"]);
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
            return new GetUserResponse { IsSuccess = isSuccess, Message = Message, Data = ListUsers };
        }

        public AddUserResponse AddUser(AddUserRequest request)
        {
            /*.........................................................................
                       PROCEDURE `PRC_USER_ADD`(
                         IN FULLNAME varchar(1000),
                         IN USERNAME varchar(1000),
                         IN UPASSWORD varchar(1000),
                         OUT PCODE varchar(2),
                         OUT PDESC varchar(1000),
                         OUT PMSG  varchar(2)  
                     );
             .............................................................................*/


            #region variable  

            DataTable dt = new DataTable();
            MySqlCommand cmd = null;
            MySqlConnection con = null;
            bool isSuccess = false;
            string Message = string.Empty;
            TranCodes tranCode = TranCodes.Exception;

            #endregion


            try
            {
                var aa = _configuration.GetConnectionString("CONN_STR");
                using (cmd = new MySqlCommand("PRC_USER_ADD", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //input
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "USERNAME", Value = request.userName, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "FULLNAME", Value = request.fullName, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "UPASSWORD", Value = request.uPassword, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });


                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PCODE", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PDESC", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PMSG", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
                    {
                        isSuccess = true;
                        tranCode = TranCodes.Success;
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

            return new AddUserResponse { IsSuccess = isSuccess, Message = Message };

        }

        public DeleteUserResponse DeleteUser(DeleteUserRequest request)
        {
            /*.............................................................
                  PROCEDURE `PRC_USERS_DELETE`(
                       IN USERNAME varchar(100),
                       OUT PCODE varchar(200),
                       OUT PDESC varchar(1000),
                       OUT PMSG  varchar(200)  
                ............................................................. */

            #region Variables
            DataTable dt = new DataTable();
            MySqlCommand cmd = null;
            MySqlConnection con = null;
            bool isSuccess = false;
            string Message = string.Empty;
            TranCodes tranCode = TranCodes.Exception;

            #endregion 

            try
            {
                var aa = _configuration.GetConnectionString("CONN_STR");
                using (cmd = new MySqlCommand("PRC_USERS_DELETE", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //input
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "USERNAME", Value = request.userName, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 1000 });

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
                        tranCode = TranCodes.Success;
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

            return new DeleteUserResponse { IsSuccess = isSuccess, Message = Message };

        }

        public UpdateUserResponse UpdateUser(UpdateUserRequest request)
        {

            /*.........................................................................
                      PROCEDURE `PRC_USER_UPDATE`(
						IN P_USERNAME varchar(1000),
                        IN P_FULLNAME varchar(1000),
                        OUT PCODE varchar(20),
                        OUT PDESC varchar(1000),
						OUT PMSG  varchar(20) 
             .............................................................................*/


            #region variable  

            DataTable dt = new DataTable();
            MySqlCommand cmd = null;
            MySqlConnection con = null;
            bool isSuccess = false;
            string Message = string.Empty;
            TranCodes tranCode = TranCodes.Exception;

            #endregion


            try
            {
                var aa = _configuration.GetConnectionString("CONN_STR");
                using (cmd = new MySqlCommand("PRC_USER_UPDATE", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //input
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_USERNAME", Value = request.userName, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_FULLNAME", Value = request.fullName, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                 

                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PCODE", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PDESC", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PMSG", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
                    {
                        isSuccess = true;
                        tranCode = TranCodes.Success;
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

            return new UpdateUserResponse { IsSuccess = isSuccess, Message = Message };

        }

    }
}


