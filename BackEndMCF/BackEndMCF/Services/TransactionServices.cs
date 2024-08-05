using BackEndMCF.Helpers;
using BackEndMCF.Interface;
using BackEndMCF.Models.TechnicalTest_MCF;
using BackEndMCF.ViewModels;

namespace BackEndMCF.Services
{
    public class TransactionServices : ITransactionServices
    {
        public readonly ITokenManager _tokenManger;
        public readonly TechnicalTest_MCFContext _dbcontext;

        public TransactionServices(ITokenManager tokenManager, TechnicalTest_MCFContext dbcontext)
        {
            _tokenManger = tokenManager;
            _dbcontext = dbcontext;
        }

        public async Task<(bool status, string message)> AddDataBPKB(ReqAddBPKB req)
        {
            DateTime aDate = DateTime.Now;

            try
            {
                var currentUser = _tokenManger.GetPrincipal();
                var checkdata = _dbcontext.TrBpkbs.Where(x => x.AgreementNumber == req.AGREEMENT_NUMBER).FirstOrDefault();
                if (checkdata != null ) // data ditemukan dan akan di update
                {
                    checkdata.BpkbNo = req.BPKB_NO;
                    checkdata.BranchId = req.BRANCH_ID;
                    checkdata.BpkpDate = req.BPKB_DATE;
                    checkdata.FakturNo = req.FAKTUR_NO;
                    checkdata.FakturDate = req.FAKTUR_DATE;
                    checkdata.LocationId = req.LOCATION_ID;
                    checkdata.PoliceNo = req.POLICE_NO;
                    checkdata.BpkbDateIn = req.BPKB_DATE_IN;
                    checkdata.LastUpdateBy = currentUser.User_id;
                    checkdata.LastUpdateOn = aDate;
                    _dbcontext.SaveChanges();
                    return (true, "Data Berhasil Diperbarui");
                }
                else
                { // data tidak ada dan akan di insert
                    TrBpkb insert = new TrBpkb
                    {
                        AgreementNumber = req.AGREEMENT_NUMBER,
                        BpkbNo = req.BPKB_NO,
                        BranchId = req.BRANCH_ID,
                        BpkpDate = req.BPKB_DATE,
                        FakturNo = req.FAKTUR_NO,
                        FakturDate = req.FAKTUR_DATE,
                        LocationId = req.LOCATION_ID,
                        PoliceNo = req.POLICE_NO,
                        BpkbDateIn = req.BPKB_DATE_IN,
                        CreatedBy = currentUser.User_id,
                        CreatedOn = aDate,
                        LastUpdateBy = currentUser.User_id,
                        LastUpdateOn = aDate
                    };
                    _dbcontext.TrBpkbs.Add(insert);
                    _dbcontext.SaveChanges();
                    return (true, "Data Berhasil Ditambahkan");
                }
            }
            catch (Exception e) 
            {
                return (false, "Terjadi Kesalahan");
            }
        }

        public async Task<(bool status, string message, List<ResDropdown> data)> DropdownStorage()
        {
            List<ResDropdown> res = new List<ResDropdown>();
            try
            {
                var getlist = _dbcontext.MsStorageLocations.ToList();
                foreach (var item in getlist)
                {
                    ResDropdown dataitem = new ResDropdown
                    {
                        ID = item.LocationId,
                        DESCRIPTION = item.LocationName
                    };
                    res.Add(dataitem);
                }
                return (true, "Dropdown", res);
            }catch(Exception e)
            {
                return (false, "Terjadi Kesalahan", res);
            }
        }
    }
}
