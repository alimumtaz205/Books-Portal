using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookPortalAPI.Models.Books
{
    public class BooksModel
    {
           public int bookId { get; set; }
           public int authorId       { get; set; }
           public string bookTitle      { get; set; }
           public byte[] coverPicture   { get; set; }
           public string synopsis       { get; set; }
           public string bookYear       { get; set; }
           public string genre          { get; set; }
           public string price          { get; set; }
           public byte[] otherPictures { get; set; }
    }
}
