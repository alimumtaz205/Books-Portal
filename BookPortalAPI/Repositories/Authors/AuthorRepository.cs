using BookPortalAPI.Models.Authors;
using BookPortalAPI.Models.Authors.Request;
using BookPortalAPI.Models.Authors.Response;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortalAPI.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IConfiguration _configuration;

        bool isSuccess = false;
        string Message = string.Empty;

        public AuthorRepository(IConfiguration configuration)
        {
            //_GlobalInfo = apiGlobalInfo;
            _configuration = configuration;
        }
        public GetAuthorsResponse GetAuthors()
        {
            /* ........................................................................
           PROCEDURE `PRC_AUTHORS_GET`(
                       OUT PCODE varchar(2),
                       OUT PDESC varchar(1000),
                       OUT PMSG  varchar(2)
            ........................................................................ */

            DataTable dt = new DataTable();
            MySqlCommand cmd = null;
            MySqlConnection con = null;

            List<AuthorsModel> AuthorsList = new List<AuthorsModel>();

            try
            {
                using (cmd = new MySqlCommand("PRC_AUTHORS_GET", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))

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
                                AuthorsModel obj = new AuthorsModel();

                                obj.authorId = Convert.ToInt32(dr["authorId"]);
                                obj.firstName = Convert.ToString(dr["firstName"]);
                                obj.middleName = Convert.ToString(dr["middleName"]);
                                obj.lastName = Convert.ToString(dr["lastName"]);
                                obj.authorName = Convert.ToString(dr["authorName"]);
                                obj.biography = Convert.ToString(dr["biography"]);
                                obj.picture = System.Text.Encoding.UTF8.GetBytes("picture");
                                AuthorsList.Add(obj);
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
            return new GetAuthorsResponse { IsSuccess = isSuccess, Message = Message, Data = AuthorsList };
        }

        public AddAuthorsResponse AddAuthor(AddAuthorsRequest request)
        {
            /*.........................................................................
                       PROCEDURE `PRC_AUTHORS_ADD`(
						IN AUTHORID varchar(1000),
                        IN FIRSTNAME varchar(1000),
                        IN MIDDLENAME varchar(100),
                        IN LASTENAME varchar(100),
						IN AUTHORNAME varchar(100),
						IN BIOGRAPHY varchar(100),
						IN PICTURE blob(500),
						OUT PCODE varchar(2),
                        OUT PDESC varchar(1000),
						OUT PMSG  varchar(2)    )
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
                using (cmd = new MySqlCommand("PRC_AUTHORS_ADD", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //input
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "AUTHORID", Value = request.authorId, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "FIRSTNAME", Value = request.firstName, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "MIDDLENAME", Value = request.middleName, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "LASTNAME", Value = request.lastName, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "AUTHORNAME", Value = request.authorName, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "BIOGRAPHY", Value = request.biography, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PICTURE", Value = request.picture, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });


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

            return new AddAuthorsResponse { IsSuccess = isSuccess, Message = Message };

        }

        public DeleteAuthorResponse DeleteAuthor(DeleteAuthorRequest request)
        {
            /*.............................................................
                  PROCEDURE `PRC_AUTHORS_DELETE`(
						IN AUTHORID int(100),
						OUT PCODE varchar(2),
                        OUT PDESC varchar(1000),
						OUT PMSG  varchar(2)                                                  
)
                ............................................................. */

            #region Variables
            DataTable dt = new DataTable();
            MySqlCommand cmd = null;
            MySqlConnection con = null;
            bool isSuccess = false;
            string Message = string.Empty;
          //  TranCodes tranCode = TranCodes.Exception;

            #endregion 

            try
            {
                var aa = _configuration.GetConnectionString("CONN_STR");
                using (cmd = new MySqlCommand("PRC_AUTHORS_DELETE", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //input
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "AUTHORID", Value = request.authorId, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 1000 });

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

            return new DeleteAuthorResponse { IsSuccess = isSuccess, Message = Message };

        }

        public UpdateAuthorsResponse UpdateAuthor(UpdateAuthorRequest request)
        {

            /*.........................................................................
                     PROCEDURE `PCR_AUTHORS_UPDATE`(
						IN AUTHORID INT,
                        IN FIRSTNAME varchar(1000),
                        IN MIDDLENAME varchar(100),
                        IN LASTENAME varchar(100),
						IN AUTHORNAME varchar(100),
						IN BIOGRAPHY varchar(100),
						IN PICTURE blob(500),
						OUT PCODE varchar(2),
                        OUT PDESC varchar(1000),
						OUT PMSG  varchar(2)    )
             .............................................................................*/


            #region variable  

            DataTable dt = new DataTable();
            MySqlCommand cmd = null;
            MySqlConnection con = null;
            bool isSuccess = false;
            string Message = string.Empty;
            
            #endregion


            try
            {
                var aa = _configuration.GetConnectionString("CONN_STR");
                using (cmd = new MySqlCommand("PCR_AUTHORS_UPDATE", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //input
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "AUTHORID", Value = request.authorId, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "FIRSTNAME", Value = request.firstName, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "MIDDLENAME", Value = request.middleName, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "LASTNAME", Value = request.lastName, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "AUTHORNAME", Value = request.authorName, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "BIOGRAPHY", Value = request.biography, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PICTURE", Value = request.picture, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });


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

            return new UpdateAuthorsResponse { IsSuccess = isSuccess, Message = Message };

        }
    }
}
