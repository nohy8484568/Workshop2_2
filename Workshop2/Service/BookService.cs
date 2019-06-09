using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Service.Model;
using System.Xml;

namespace Service
{
    public class BookService
    {
        string _conn = @"Data Source = 34.80.215.162; Initial Catalog = GSSWEB; Integrated Security=False;User ID=sa;Password=aAzZ1234;";
        public List<BOOK_CLASS> GetBookClassList()
        {
            DataTable dt = new DataTable();
            string sql = @"Select BOOK_CLASS_ID, BOOK_CLASS_NAME
                           From dbo.BOOK_CLASS";
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                sda.Fill(dt);
                conn.Close();
            }

            List<BOOK_CLASS> bookClasses = new List<BOOK_CLASS>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BOOK_CLASS bookClass = new BOOK_CLASS();
                bookClass.BOOK_CLASS_ID = dt.Rows[i][0].ToString();
                bookClass.BOOK_CLASS_NAME = dt.Rows[i][1].ToString();
                bookClasses.Add(bookClass);
            }

            return bookClasses;
        }
        public List<MEMBER_M> GetMemberList()
        {
            DataTable dt = new DataTable();
            string sql = @"Select USER_ID, USER_CNAME
                           From dbo.MEMBER_M";
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                sda.Fill(dt);
                conn.Close();
            }

            List<MEMBER_M> members = new List<MEMBER_M>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MEMBER_M member = new MEMBER_M();
                member.USER_ID = dt.Rows[i][0].ToString();
                member.USER_CNAME= dt.Rows[i][1].ToString();
                members.Add(member);
            }
            
            return members;
        }
        public List<BOOK_CODE> GetCodeList()
        {
            DataTable dt = new DataTable();
            string sql = @"Select CODE_ID, CODE_NAME
                           From dbo.BOOK_CODE";
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                sda.Fill(dt);
                conn.Close();
            }

            List<BOOK_CODE> codes = new List<BOOK_CODE>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BOOK_CODE code = new BOOK_CODE();
                code.CODE_ID = dt.Rows[i][0].ToString();
                code.CODE_NAME = dt.Rows[i][1].ToString();
                codes.Add(code);
            }

            return codes;
        }
        public List<BOOK_DATA> GetBookDataList(string BOOK_NAME, string BOOK_CLASS_ID, string BOOK_KEEPER, string BOOK_STATUS)
        {
            DataTable dt = new DataTable();
            string sql = @"Select *
                           From dbo.BOOK_DATA";
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                sda.Fill(dt);
                conn.Close();
            }

            var data = new List<BOOK_DATA>();
            var ClassList = GetBookClassList();
            var MemberList = GetMemberList();
            var CodeList = GetCodeList();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BOOK_DATA _data = new BOOK_DATA();
                _data.BOOK_ID = Int32.Parse(dt.Rows[i][0].ToString()); 
                _data.BOOK_NAME = dt.Rows[i][1].ToString();
                _data.BOOK_CLASS_ID = ClassList.Find(x => x.BOOK_CLASS_ID == dt.Rows[i][2].ToString()).BOOK_CLASS_NAME;
                _data.BOOK_AUTHOR = dt.Rows[i][3].ToString();
                _data.BOOK_BOUGHT_DATE = DateTime.Parse(dt.Rows[i][4].ToString());
                _data.BOOK_PUBLISHER = dt.Rows[i][5].ToString();
                _data.BOOK_NOTE = dt.Rows[i][6].ToString();
                _data.BOOK_STATUS = CodeList.Find(x => x.CODE_ID == dt.Rows[i][7].ToString()).CODE_NAME;
                if (!string.IsNullOrEmpty(dt.Rows[i][8].ToString()))
                {
                    _data.BOOK_KEEPER = MemberList.Find(x => x.USER_ID == dt.Rows[i][8].ToString()).USER_CNAME;
                }
                data.Add(_data);
            }

            if (BOOK_NAME != null)
            {
                data = data.Where(x => x.BOOK_NAME.Contains(BOOK_NAME)).ToList();
            }
            if (BOOK_CLASS_ID != null)
            {
                data = data.Where(x => x.BOOK_CLASS_ID == BOOK_CLASS_ID).ToList();
            }
            if (BOOK_KEEPER != null)
            {
                data = data.Where(x => x.BOOK_KEEPER== BOOK_KEEPER).ToList();
            }
            if (BOOK_STATUS != null)
            {
                data = data.Where(x => x.BOOK_STATUS== BOOK_STATUS).ToList();
            }

            return data.OrderByDescending(x=>x.BOOK_BOUGHT_DATE).ToList();
        }
        public BOOK_DATA GetBookData(int BookId)
        {
            DataTable dt = new DataTable();
            string sql = @"Select *
                           From dbo.BOOK_DATA Where BOOK_ID = @BookId";
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BookId", BookId));
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                conn.Close();
            }

            BOOK_DATA _data = new BOOK_DATA();
            _data.BOOK_ID = Int32.Parse(dt.Rows[0][0].ToString());
            _data.BOOK_NAME = dt.Rows[0][1].ToString();
            _data.BOOK_CLASS_ID = dt.Rows[0][2].ToString();
            _data.BOOK_AUTHOR = dt.Rows[0][3].ToString();
            _data.BOOK_BOUGHT_DATE = DateTime.Parse(dt.Rows[0][4].ToString());
            _data.BOOK_PUBLISHER = dt.Rows[0][5].ToString();
            _data.BOOK_NOTE = dt.Rows[0][6].ToString();
            _data.BOOK_STATUS =  dt.Rows[0][7].ToString();
            _data.BOOK_KEEPER = dt.Rows[0][8].ToString();
            return _data;
        }

        public int InsertBook(BOOK_DATA bookData)
        {
            string sql = @" INSERT INTO BOOK_DATA
						 (
							 BOOK_NAME,BOOK_CLASS_ID,BOOK_AUTHOR,BOOK_BOUGHT_DATE
                            ,BOOK_PUBLISHER,BOOK_NOTE,BOOK_STATUS
						 )
						VALUES
						(
							 @BOOK_NAME,@BOOK_CLASS_ID, @BOOK_AUTHOR, @BOOK_BOUGHT_DATE, @BOOK_PUBLISHER, @BOOK_NOTE, 
                             @BOOK_STATUS
						)
						Select SCOPE_IDENTITY()";
            int id;
            using (SqlConnection conn = new SqlConnection(_conn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@BOOK_NAME", bookData.BOOK_NAME));
                cmd.Parameters.Add(new SqlParameter("@BOOK_CLASS_ID", bookData.BOOK_CLASS_ID));
                cmd.Parameters.Add(new SqlParameter("@BOOK_AUTHOR", bookData.BOOK_AUTHOR));
                cmd.Parameters.Add(new SqlParameter("@BOOK_BOUGHT_DATE", bookData.BOOK_BOUGHT_DATE));
                cmd.Parameters.Add(new SqlParameter("@BOOK_PUBLISHER", bookData.BOOK_PUBLISHER));
                cmd.Parameters.Add(new SqlParameter("@BOOK_NOTE", bookData.BOOK_NOTE));
                cmd.Parameters.Add(new SqlParameter("@BOOK_STATUS", "A"));
                id = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return id;
        }

        public int DeleteBook(int id)
        {
            try
            {
                string sql = "Delete FROM BOOK_DATA Where BOOK_ID=@BOOK_ID";
                using (SqlConnection conn = new SqlConnection(_conn))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@BOOK_ID", id));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                return id;
            }
            catch (Exception ex)
            {
                return 404;
            }
        }
        public int EditBook(BOOK_DATA bookData)
        {
            
            try
            {
                string sql = @"UPDATE BOOK_DATA SET BOOK_NAME = @BOOK_NAME, BOOK_CLASS_ID = @BOOK_CLASS_ID, BOOK_AUTHOR =@BOOK_AUTHOR
                            ,BOOK_BOUGHT_DATE=@BOOK_BOUGHT_DATE,BOOK_PUBLISHER=@BOOK_PUBLISHER
                            ,BOOK_NOTE=@BOOK_NOTE,BOOK_STATUS=@BOOK_STATUS,BOOK_KEEPER =@BOOK_KEEPER Where BOOK_ID=@BOOK_ID";
                using (SqlConnection conn = new SqlConnection(_conn))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@BOOK_ID", bookData.BOOK_ID));
                    cmd.Parameters.Add(new SqlParameter("@BOOK_NAME", bookData.BOOK_NAME));
                    cmd.Parameters.Add(new SqlParameter("@BOOK_CLASS_ID", bookData.BOOK_CLASS_ID));
                    cmd.Parameters.Add(new SqlParameter("@BOOK_AUTHOR", bookData.BOOK_AUTHOR));
                    cmd.Parameters.Add(new SqlParameter("@BOOK_BOUGHT_DATE", bookData.BOOK_BOUGHT_DATE));
                    cmd.Parameters.Add(new SqlParameter("@BOOK_PUBLISHER", bookData.BOOK_PUBLISHER));
                    cmd.Parameters.Add(new SqlParameter("@BOOK_NOTE", bookData.BOOK_NOTE));
                    cmd.Parameters.Add(new SqlParameter("@BOOK_STATUS", bookData.BOOK_STATUS));
                    
                    cmd.Parameters.Add(new SqlParameter("@BOOK_KEEPER", bookData.BOOK_KEEPER ?? (Object)DBNull.Value));
                    cmd.ExecuteScalar();
                    conn.Close();
                }

                return bookData.BOOK_ID;
            }
            catch (Exception ex)
            {
                return 404;
            }
        }
    }
}
