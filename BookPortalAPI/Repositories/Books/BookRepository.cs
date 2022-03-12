using BookPortalAPI.Models;
using BookPortalAPI.Models.Books;
using BookPortalAPI.Models.Books.Response;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortalAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IConfiguration _configuration;

        bool isSuccess = false;
        string Message = string.Empty;
        TranCodes tranCode = TranCodes.Exception;
        public GetBookResponse GetBooks()
        {
            /* ........................................................................
                     PROCEDURE `PRC_BOOK_GET`(
                         IN P_BOOKID varchar(1000),
                         OUT PCODE varchar(200),
                         OUT PDESC varchar(1000),
                         OUT PMSG  varchar(200)                                                 
                                                )
            ........................................................................ */
            DataTable dt = new DataTable();
            MySqlCommand cmd = null;
            MySqlConnection con = null;

            List <BooksModel> ListBook = new List<BooksModel>();

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
                                BooksModel obj = new BooksModel();

                                obj.bookId = Convert.ToInt32(dr["bookId"]);
                                obj.authorId = Convert.ToInt32(dr["authorId"]);
                                obj.bookTitle = Convert.ToString(dr["fullName"]);
                                obj.coverPicture = System.Text.Encoding.UTF8.GetBytes("coverPicture");
                                obj.synopsis = Convert.ToString(dr["synopsis"]);
                                obj.bookYear = Convert.ToString(dr["bookYear"]);
                                obj.genre = Convert.ToString(dr["genre"]);
                                obj.price = Convert.ToString(dr["price"]);
                                obj.otherPictures = System.Text.Encoding.UTF8.GetBytes("otherPictures");
                                ListBook.Add(obj);
                            }
                        }
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
            return new GetBookResponse { IsSuccess = isSuccess, Message = Message, Data = ListBook };

        }


    }
}
