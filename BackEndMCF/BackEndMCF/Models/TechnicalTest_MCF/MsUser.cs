using System;
using System.Collections.Generic;

namespace BackEndMCF.Models.TechnicalTest_MCF
{
    public partial class MsUser
    {
        public long UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; }
    }
}
