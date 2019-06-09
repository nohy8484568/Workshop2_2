using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model
{
    public class BOOK_CLASS
    {
        public string BOOK_CLASS_ID { get; set; }
        public string BOOK_CLASS_NAME { get; set; }
        public DateTime? CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public DateTime? MODIFY_DATE { get; set; }
        public string MODIFY_USER { get; set; }
    }
}
