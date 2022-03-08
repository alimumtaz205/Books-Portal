using BookPortalAPI.Models;
using BookPortalAPI.Models.Requests;
using BookPortalAPI.Models.Responses;
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

        private string Code;
        private bool IsSuccess;
        private string Message;

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
            using MySqlConnection con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"));
            // TransactionCodes tranCode = TransactionCodes.GET_DEVICE_ERROR;
            List<GetUsers> ListUsers = new List<GetUsers>();
            try
            {
                using (cmd = new MySqlCommand("PRC_USERS_GET"))
                //using (cmd = new MySqlCommand("Select * from users", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
                {
                    //if (request.userName == string.Empty || request.userName == null)
                    //{
                    //    request.userName = DBNull.Value.ToString();
                    //}


                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add(new MySqlParameter { ParameterName = "PCURSOR", MySqlDbType = MySqlDbType.VarChar  , Direction = ParameterDirection.Output, Size = 1000 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PCODE", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PDESC", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PMSG", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 100 });

                    con.Open();
                    MySqlDataAdapter myDA = new MySqlDataAdapter(cmd);
                    myDA.Fill(dt);
                    con.Close();

                    if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
                    {
                        // tranCode = TransactionCodes.SUCCESS;
                        IsSuccess = true;
                        Code = Convert.ToString(cmd.Parameters["PCODE"].Value);
                        Message = Convert.ToString(cmd.Parameters["PDESC"].Value);

                        if (dt != null && dt.Rows.Count > 0)
                        {

                            foreach (DataRow dr in dt.Rows)
                            {
                                GetUsers obj = new GetUsers();


                                obj.userName = Convert.ToString(dr["userName"]);
                                obj.fullName = Convert.ToString(dr["fullName"]);
                                obj.uPassword = Convert.ToString(dr["uPassword"]);
                                ListUsers.Add(obj);

                            }
                        }
                        else
                        {
                            IsSuccess = false;
                            Code = Convert.ToString(cmd.Parameters["PCODE"].Value);
                            Message = Convert.ToString(cmd.Parameters["PDESC"].Value);
                        }
                    }
                    else
                    {
                        IsSuccess = false;
                        Code = Convert.ToString(cmd.Parameters["PCODE"].Value);
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
            return new GetUserResponse { IsSuccess = IsSuccess, Message = Message, Data = ListUsers };
        }

    }

}

