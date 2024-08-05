using System;
using System.Collections.Generic;

namespace BackEndMCF.Models.TechnicalTest_MCF
{
    public partial class TrBpkb
    {
        public string AgreementNumber { get; set; } = null!;
        public string? BpkbNo { get; set; }
        public string? BranchId { get; set; }
        public DateTime? BpkpDate { get; set; }
        public string? FakturNo { get; set; }
        public DateTime? FakturDate { get; set; }
        public string? LocationId { get; set; }
        public string? PoliceNo { get; set; }
        public DateTime? BpkbDateIn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? LastUpdateBy { get; set; }
        public DateTime? LastUpdateOn { get; set; }

        public virtual MsStorageLocation? Location { get; set; }
    }
}
