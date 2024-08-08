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
        public async Task<(bool status, string message)> DeleteData(ReqIdAgreement req)
        {
            try
            {
                var getdata = _dbcontext.TrBpkbs.Where(e => e.AgreementNumber == req.id).FirstOrDefault();
                if (getdata != null)
                {
                    _dbcontext.Remove(getdata);
                    _dbcontext.SaveChanges();
                    return (true, "Data berhasil dihapus");
                }
                else
                {
                    return (false, "Data tidak ditemukan");
                }
            }catch(Exception e)
            {
                return (false, "Terjadi Kesalahan");
            }
        }

        public async Task<(bool status, string message, ResDataBPKB data)> GetDataById(ReqIdAgreement req)
        {
            ResDataBPKB res = new ResDataBPKB();
            try
            {   
                var getdata = _dbcontext.TrBpkbs.Where(e => e.AgreementNumber == req.id).FirstOrDefault();
                if (getdata != null)
                {
                    res.AGREEMENT_NUMBER = getdata.AgreementNumber;
                    res.BPKB_NO = getdata.BpkbNo;
                    res.BRANCH_ID = getdata.BranchId;
                    res.BPKB_DATE = (getdata.BpkpDate.HasValue ? getdata.BpkpDate.Value.ToString("yyyy-MM-dd") : null) ;
                    res.FAKTUR_NO = getdata.FakturNo;
                    res.FAKTUR_DATE = (getdata.FakturDate.HasValue ? getdata.FakturDate.Value.ToString("yyyy-MM-dd") : null);
                    res.LOCATION_ID = getdata.LocationId;
                    res.POLICE_NO = getdata.PoliceNo;
                    res.BPKB_DATE_IN = (getdata.BpkbDateIn.HasValue ? getdata.BpkbDateIn.Value.ToString("yyyy-MM-dd") : null);
                    return (true, "List By Id", res);
                }
                else
                {
                    return (false, "Data tidak ditemukan",res);
                }
            }
            catch(Exception e ) {
                return (false, "Terjadi Kesalahan",res);
            }
        }

        public async Task<(bool status, string message, List<ResDataBPKB> data)> GetList()
        {
            List<ResDataBPKB> res = new List<ResDataBPKB>();
            try
            {
                var currentUser = _tokenManger.GetPrincipal();
                var getdata = _dbcontext.TrBpkbs.Where(e => e.CreatedBy == currentUser.User_id).ToList();
                if (getdata != null)
                {
                    foreach (var item in getdata)
                    {
                        ResDataBPKB dataitem = new ResDataBPKB
                        {
                            AGREEMENT_NUMBER = item.AgreementNumber,
                            BPKB_NO = item.BpkbNo,
                            BRANCH_ID = item.BranchId,
                            BPKB_DATE = (item.BpkpDate.HasValue ? item.BpkpDate.Value.ToString("yyyy-MM-dd") : null) ,
                        FAKTUR_NO = item.FakturNo,
                            FAKTUR_DATE = (item.FakturDate.HasValue ? item.FakturDate.Value.ToString("yyyy-MM-dd") : null),
                            LOCATION_ID = item.LocationId,
                            POLICE_NO = item.PoliceNo,
                            BPKB_DATE_IN = (item.BpkbDateIn.HasValue ? item.BpkbDateIn.Value.ToString("yyyy-MM-dd") : null),
                        };
                        res.Add(dataitem);
                        
                    }
                    
                    return (true, "List data", res);
                }
                else
                {
                    return (false, "Data tidak ditemukan", res);
                }
            }
            catch (Exception e)
            {
                return (false, "Terjadi Kesalahan", res);
            }
        }
    }
}
