using BookPortalAPI.Models;
using BookPortalAPI.Models.Users;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortalAPI.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private string Code;
        private bool IsSuccess;
        private string Message;
        public readonly IConfiguration _configuration;
        public LoginRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public LoginUserResponse LoginUser(LoginUserRequest request)
        {
            /*.............................................................................................
                     PROCEDURE `PCR_LOGIN`(
			                IN P_USERNAME VARCHAR(100),
                            IN P_PASSWORD VARCHAR(100),
                            OUT PCODE varchar(20),
			                OUT PDESC varchar(1000),
			                OUT PMSG  varchar(20)  )
              .............................................................................................*/


            //UserLogin userInfo = new UserLogin();
            //LoginResponses loginResponse = new LoginResponses();
            DataTable dt = new DataTable();
            MySqlCommand cmd = null;
            MySqlConnection con = null;
            bool isSuccess = false;
            string Message = string.Empty;

            // TransactionCodes tranCode = TransactionCodes.SUCCESS;
            List<UsersModel> listUserActivities = new List<UsersModel>();

            try
            {
                var aa = _configuration.GetConnectionString("CONN_STR");
                using (cmd = new MySqlCommand("PCR_LOGIN", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //input
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_USERNAME", Value = request.userName, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 1000 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_PASSWORD", Value = request.uPassword, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 1000 });

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

            return new LoginUserResponse { IsSuccess = isSuccess, Message = Message };

        }

       
    }
}
