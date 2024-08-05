using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndMCF.Models
{
    public class ServiceResponse<T>
    {
        public int CODE { get; set; }
        public string MESSAGE { get; set; }
        public IEnumerable<T> DATA { get; set; }
    }

    public class ServiceResponseSingle<T>
    {
        public int CODE { get; set; }
        public string MESSAGE { get; set; }
        public T DATA { get; set; }
    }

    public class ResLogin
    {
        public string TOKEN { get; set; }
    }
    public class ResDropdown
    {
        public string ID { get; set; }
        public string DESCRIPTION { get; set; }
    }




}
