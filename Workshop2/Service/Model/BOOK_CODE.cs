using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model
{
    public class BOOK_CODE
    {
        public string CODE_TYPE { get; set; }
        public string CODE_ID { get; set; }
        public string CODE_TYPE_DESC { get; set; }
        public string CODE_NAME { get; set; }
        public DateTime? CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public DateTime? MODIFY_DATE { get; set; }
        public string MODIFY_USER { get; set; }
    }
}
