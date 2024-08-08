using BackEndMCF.ViewModels;

namespace BackEndMCF.Interface
{
    public interface ITransactionServices
    {
        Task<(bool status, string message)> AddDataBPKB(ReqAddBPKB req);
        Task<(bool status, string message, List<ResDropdown> data)> DropdownStorage();
        Task<(bool status, string message)> DeleteData(ReqIdAgreement AgreeNumber);
        Task<(bool status, string message, ResDataBPKB data)> GetDataById(ReqIdAgreement AgreeNumber);
        Task<(bool status, string message, List<ResDataBPKB> data)> GetList();
    }
}
