namespace BackEndMCF.ViewModels
{
    public class RequestVM
    {
    }
    public class ReqLogin
    {
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
    }

    public class ReqAddBPKB
    {
        public string AGREEMENT_NUMBER { get; set; }
        public string BPKB_NO { get; set; }
        public string BRANCH_ID { get; set; }
        public DateTime BPKB_DATE { get; set; }
        public string FAKTUR_NO { get; set; }
        public DateTime FAKTUR_DATE { get; set; }
        public string LOCATION_ID { get; set; }
        public string POLICE_NO { get; set; }
        public DateTime BPKB_DATE_IN { get; set; }
       
    }
}
