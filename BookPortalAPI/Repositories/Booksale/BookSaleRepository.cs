using BookPortalAPI.Models.Booksale;
using BookPortalAPI.Models.Booksale.Request;
using BookPortalAPI.Models.Booksale.Response;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortalAPI.Repositories.Booksale
{
    public class BookSaleRepository : IBookSaleRepository
    {
        private readonly IConfiguration _configuration;

        bool isSuccess = false;
        string Message = string.Empty;

        public BookSaleRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public GetBookSaleResponse GetBookSale()
        {
            /* ........................................................................
             PROCEDURE `PCR_BOOKSALE_GET`(
						IN P_SALEID varchar(100),
						OUT PCODE varchar(20),
                        OUT PDESC varchar(1000),
						OUT PMSG  varchar(20)                                                    
)
           ........................................................................ */
            DataTable dt = new DataTable();
            MySqlCommand cmd = null;
            MySqlConnection con = null;


            List<BookSaleModel> ListUsers = new List<BookSaleModel>();

            try
            {
                //var aa = _configuration.GetConnectionString("CONN_STR");
                using (cmd = new MySqlCommand("PCR_BOOKSALE_GET", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
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
                                BookSaleModel obj = new BookSaleModel();

                                obj.saleId = Convert.ToInt32(dr["saleId"]);
                                obj.bookId = Convert.ToInt32(dr["bookId"]);
                                obj.price = Convert.ToString(dr["price"]);
                                obj.dateOfSale = Convert.ToString(dr["dateOfSale"]);
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
            return new GetBookSaleResponse { IsSuccess = isSuccess, Message = Message, Data = ListUsers };
        }

        public AddBookSaleResponse AddBookSale(AddBookSaleRequest request)
        {
            /*.........................................................................
                PROCEDURE `PCR_BOOKSALE_ADD`(
						IN SALEID varchar(100),
						IN BOOKID varchar(1000),
						IN PRICE varchar(100),
						IN DATEOFSALE varchar(200),
						OUT PCODE varchar(20),
                        OUT PDESC varchar(1000),
						OUT PMSG  varchar(20)    
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
                using (cmd = new MySqlCommand("PCR_BOOKSALE_ADD", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //input

                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "SALEID", Value = request.saleId, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "BOOKID", Value = request.bookId, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "PRICE", Value = request.price, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "DATEOFSALE", Value = request.dateOfSale, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });


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

            return new AddBookSaleResponse { IsSuccess = isSuccess, Message = Message };

        }

        public DeleteBookSaleResponse DeleteBookSale(DeleteBookSaleRequest request)
        {
            /*.............................................................
                 PROCEDURE `PCR_BOOKSALE_DELETE`(
						IN SALEID varchar(100),
						OUT PCODE varchar(20),
                        OUT PDESC varchar(1000),
						OUT PMSG  varchar(20) 
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
                using (cmd = new MySqlCommand("PCR_BOOKSALE_DELETE", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //input
                    cmd.Parameters.Add(new MySqlParameter { ParameterName = "BOOKID", Value = request.saleId, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 1000 });

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

            return new DeleteBookSaleResponse { IsSuccess = isSuccess, Message = Message };

        }

        //  public UpdateBookResponse UpdateBook(UpdateBookRequest request)
        //  {

        //      /*.........................................................................
        //            PROCEDURE `PRC_BOOK_UPDATE`(
        //IN BOOKID varchar(1000),
        //                  IN TITLE varchar(100),
        //IN CPICTURE varchar(100),
        //IN GENRE varchar(100),
        //IN PRICE varchar(100),
        //OUT PCODE varchar(20),
        //                  OUT PDESC varchar(1000),
        //OUT PMSG  varchar(20))
        //       .............................................................................*/


        //      #region variable  

        //      DataTable dt = new DataTable();
        //      MySqlCommand cmd = null;
        //      MySqlConnection con = null;
        //      bool isSuccess = false;
        //      string Message = string.Empty;

        //      #endregion
        //      try
        //      {
        //          var aa = _configuration.GetConnectionString("CONN_STR");
        //          using (cmd = new MySqlCommand("PRC_BOOK_UPDATE", con = new MySqlConnection(_configuration.GetConnectionString("CONN_STR"))))
        //          {
        //              cmd.CommandType = CommandType.StoredProcedure;

        //              //input
        //              cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_BOOKID", Value = request.bookId, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
        //              cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_TITLE", Value = request.bookTitle, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
        //              cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_CPICTURE", Value = request.coverPicture, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
        //              cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_SYNOPSIS", Value = request.synopsis, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
        //              cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_BOOKYEAR", Value = request.bookYear, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
        //              cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_GENRE", Value = request.genre, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
        //              cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_PRICE", Value = request.price, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });
        //              cmd.Parameters.Add(new MySqlParameter { ParameterName = "P_OTHERPICTURE", Value = request.otherPictures, MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Input, Size = 100 });


        //              cmd.Parameters.Add(new MySqlParameter { ParameterName = "PCODE", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });
        //              cmd.Parameters.Add(new MySqlParameter { ParameterName = "PDESC", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });
        //              cmd.Parameters.Add(new MySqlParameter { ParameterName = "PMSG", MySqlDbType = MySqlDbType.VarChar, Direction = ParameterDirection.Output, Size = 1000 });


        //              con.Open();
        //              cmd.ExecuteNonQuery();
        //              con.Close();

        //              if (Convert.ToString(cmd.Parameters["PCODE"].Value) == "00" || Convert.ToString(cmd.Parameters["PCODE"].Value) == "0")
        //              {
        //                  isSuccess = true;
        //                  //tranCode = TranCodes.Success;
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

        //      return new UpdateBookResponse { IsSuccess = isSuccess, Message = Message };

        //  }
    }
}
