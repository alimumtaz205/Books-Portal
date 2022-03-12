using BookPortalAPI.Models.Books;
using BookPortalAPI.Models.Books.Request;
using BookPortalAPI.Models.Books.Response;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortalAPI.Repositories.Books
{
    public class BookRepository : IBookRepository
    {
        private readonly IConfiguration _configuration;

        bool isSuccess = false;
        string Message = string.Empty;

        public BookRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public GetBookResponse GetBooks()
        {
            /* ........................................................................
              PROCEDURE `PRC_BOOK_GET`(
						OUT PCODE varchar(200),
                        OUT PDESC varchar(1000),
						OUT PMSG  varchar(200)                                                   
)
           ........................................................................ */
            DataTable dt = new DataTable();
            MySqlCommand cmd = null;
            MySqlConnection con = null;


            List<BooksModel> ListUsers = new List<BooksModel>();

            try
            {
                //var aa = _configuration.GetConnectionString("CONN_STR");
                using (cmd = new MySqlCommand("PRC_BOOK_GET", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
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
                                obj.bookTitle = Convert.ToString(dr["bookTitle"]);
                                obj.coverPicture = System.Text.Encoding.UTF8.GetBytes("coverPicture");
                                obj.synopsis = Convert.ToString(dr["synopsis"]);
                                obj.bookYear = Convert.ToString(dr["bookYear"]);
                                obj.genre = Convert.ToString(dr["genre"]);
                                obj.price = Convert.ToString(dr["price"]);
                                obj.otherPictures = System.Text.Encoding.UTF8.GetBytes("otherPictures");
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
            return new GetBookResponse { IsSuccess = isSuccess, Message = Message, Data = ListUsers };
        }

        public AddBookResponse AddBook(AddBookRequest request)
        {
            /*.........................................................................
                PROCEDURE `PRC_BOOK_ADD`(
                        IN BOOKID varchar(1000),
                        IN AUTHORID varchar(100),
                        IN TITLE varchar(100),
						IN CPICTURE varchar(100),
						IN SYNOPSIS varchar(100),
						IN BYEAR varchar(100),
						IN GENRE varchar(100),
						IN PRICE varchar(100),
						IN OPICTURE blob(500),
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

            #endregion

            try
            {
                var aa = _configuration.GetConnectionString("CONN_STR");
                using (cmd = new MySqlCommand("PRC_BOOK_ADD", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //input

                    cmd.Parameters.Add(new MySqlParameter { ParameterName ="BOOKID", Value = request.bookId, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName ="AUTHORID", Value = request.authorId, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName ="TITLE", Value = request.bookTitle, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
					cmd.Parameters.Add(new MySqlParameter { ParameterName ="CPICTURE", Value = request.coverPicture, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
					cmd.Parameters.Add(new MySqlParameter { ParameterName ="SYNOPSIS", Value = request.synopsis, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName ="BYEAR", Value = request.bookYear, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
					cmd.Parameters.Add(new MySqlParameter { ParameterName ="GENRE", Value = request.genre, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
					cmd.Parameters.Add(new MySqlParameter { ParameterName ="PRICE", Value = request.price, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
					cmd.Parameters.Add(new MySqlParameter { ParameterName = "OPICTURE", Value = request.otherPictures, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });


                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PCODE", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PDESC", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PMSG", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
                    {
                        isSuccess = true;
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

            return new AddBookResponse { IsSuccess = isSuccess, Message = Message };

        }

        public DeleteBookResponse DeleteBook(DeleteBookRequest request)
        {
            /*.............................................................
                 PROCEDURE `PRC_BOOK_DELETE`(
                        IN BOOKID varchar(1000),
						OUT PCODE varchar(200),
                        OUT PDESC varchar(1000),
						OUT PMSG  varchar(200) 
            );  
                ............................................................. */

            #region Variables
            DataTable dt = new DataTable();
            MySqlCommand cmd = null;
            MySqlConnection con = null;
            bool isSuccess = false;
            string Message = string.Empty;
           

            #endregion 

            try
            {
                var aa = _configuration.GetConnectionString("CONN_STR");
                using (cmd = new MySqlCommand("PRC_BOOK_DELETE", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //input
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "BOOKID", Value = request.bookId, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 1000 });

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

            return new DeleteBookResponse { IsSuccess = isSuccess, Message = Message };

        }

        public UpdateBookResponse UpdateBook(UpdateBookRequest request)
        {

            /*.........................................................................
                  PROCEDURE `PRC_BOOK_UPDATE`(
						IN BOOKID varchar(1000),
                        IN TITLE varchar(100),
						IN CPICTURE varchar(100),
						IN GENRE varchar(100),
						IN PRICE varchar(100),
						OUT PCODE varchar(20),
                        OUT PDESC varchar(1000),
						OUT PMSG  varchar(20))
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
                using (cmd = new MySqlCommand("PRC_BOOK_UPDATE", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //input
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_BOOKID", Value = request.bookId, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_TITLE", Value = request.bookTitle, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_CPICTURE", Value = request.coverPicture, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_SYNOPSIS", Value = request.synopsis, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_BOOKYEAR", Value = request.bookYear, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_GENRE", Value = request.genre, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_PRICE", Value = request.price, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_OTHERPICTURE", Value = request.otherPictures, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });


                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PCODE", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PDESC", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PMSG", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });


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

            return new UpdateBookResponse { IsSuccess = isSuccess, Message = Message };

        }

    }
}
