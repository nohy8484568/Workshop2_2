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
        /// <summary>
        /// 取得Service
        /// </summary>
        BookService bookService = new BookService();
        
        /// <summary>
        /// 首頁
        /// </summary>
        /// <returns>首頁頁面</returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 取得書籍分類清單
        /// </summary>
        /// <returns>JSON 書籍分類 BOOK_CLASS</returns>
        [HttpPost]
        public JsonResult Class()
        {
            return Json(bookService.GetBookClassList());
        }
        /// <summary>
        /// 取得會員資料清單
        /// </summary>
        /// <returns>JSON 會員資料 MEMBER_M</returns>
        [HttpPost]
        public JsonResult Member()
        {
            return Json(bookService.GetMemberList());
        }
        /// <summary>
        /// 取得書籍狀態清單
        /// </summary>
        /// <returns>JSON 書籍狀態 BOOK_CODE</returns>
        public JsonResult Code()
        {
            return Json(bookService.GetCodeList());
        }
        /// <summary>
        /// 設定搜尋條件取得書籍資料清單
        /// </summary>
        /// <param name="BOOK_NAME">書籍名稱</param>
        /// <param name="BOOK_CLASS_ID">書籍分類ID</param>
        /// <param name="BOOK_KEEPER">使用者ID</param>
        /// <param name="BOOK_STATUS">書籍狀態碼</param>
        /// <returns>JSON 書籍資料 BOOK_DATA</returns>
        public JsonResult Data()
        {
            var BOOK_NAME = Request.Params["BOOK_NAME"];
            var BOOK_CLASS_ID = Request.Params["BOOK_CLASS_ID"];
            var BOOK_KEEPER = Request.Params["BOOK_KEEPER"];
            var BOOK_STATUS = Request.Params["BOOK_STATUS"];
            return Json(bookService.GetBookDataList(BOOK_NAME, BOOK_CLASS_ID, BOOK_KEEPER, BOOK_STATUS));
        }
        /// <summary>
        ///使用ID取得書籍資料
        /// </summary>
        /// <param name="id">BOOK_DATA.BOOK_ID</param>
        /// <returns>JSON 書籍資料 BOOK_DATA</returns>
        public JsonResult DataById(int id)
        {
            return Json(bookService.GetBookData(id));
        }
        /// <summary>
        /// 新增書籍資料
        /// </summary>
        /// <param name="bookData">BOOK_DATA</param>
        /// <returns>JSON 書籍編號 BOOK_DATA.BOOK_ID</returns>
        public JsonResult Insert(BOOK_DATA bookData)
        {
            return Json(bookService.InsertBook(bookData));
        }
        /// <summary>
        /// 刪除書籍資料
        /// </summary>
        /// <param name="id">BOOK_DATA.BOOK_ID</param>
        /// <returns>JSON 書籍編號 BOOK_DATA.BOOK_ID</returns>
        public JsonResult DeleteBook(int id)
        {
            return Json(bookService.DeleteBook(id));
        }
        /// <summary>
        /// 修改書籍資料
        /// </summary>
        /// <param name="bookData">BOOK_DATA</param>
        /// <returns>JSON 書籍編號 BOOK_DATA.BOOK_ID</returns>
        public JsonResult EditBook(BOOK_DATA bookData)
        {
            return Json(bookService.EditBook(bookData));
        }
    }
}