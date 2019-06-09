using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using Service.Model;

namespace Workshop2.Controllers
{
    public class BookController : Controller
    {
        BookService bookService = new BookService();
        
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Class()
        {
            return Json(bookService.GetBookClassList());
        }
        [HttpPost]
        public JsonResult Member()
        {
            return Json(bookService.GetMemberList());
        }
        public JsonResult Code()
        {
            return Json(bookService.GetCodeList());
        }

        public JsonResult Data()
        {
            var BOOK_NAME = Request.Params["BOOK_NAME"];
            var BOOK_CLASS_ID = Request.Params["BOOK_CLASS_ID"];
            var BOOK_KEEPER = Request.Params["BOOK_KEEPER"];
            var BOOK_STATUS = Request.Params["BOOK_STATUS"];
            return Json(bookService.GetBookDataList(BOOK_NAME, BOOK_CLASS_ID, BOOK_KEEPER, BOOK_STATUS));
        }
        public JsonResult DataById(int id)
        {
            return Json(bookService.GetBookData(id));
        }
        public JsonResult Insert(BOOK_DATA bookData)
        {
            return Json(bookService.InsertBook(bookData));
        }
        public JsonResult DeleteBook(int id)
        {
            return Json(bookService.DeleteBook(id));
        }
        public JsonResult EditBook(BOOK_DATA bookData)
        {
            return Json(bookService.EditBook(bookData));
        }
    }
}