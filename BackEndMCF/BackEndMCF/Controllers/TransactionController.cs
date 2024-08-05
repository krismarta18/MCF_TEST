using BackEndMCF.Interface;
using BackEndMCF.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeewashAPICore.ViewModels;
using System.Security.Principal;

namespace BackEndMCF.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionServices _transaction;
        public TransactionController(ITransactionServices transaction)
        {
            _transaction = transaction;
        }

        [Authorize]
        [HttpPost("AddDataBPKB")]
        public async Task<IActionResult> AddDataBPKB([FromBody] ReqAddBPKB req)
        {
            var res = new ServiceResponseSingle<string>();
            try
            {
                var _ = await _transaction.AddDataBPKB(req);

                if (!_.status)
                {
                    res.CODE = 0;
                    res.MESSAGE = _.message;
                }
                else
                {
                    res.CODE = 1;
                    res.MESSAGE = _.message;
                    //res.DATA = _.data;
                }
            }
            catch (Exception ex)
            {
                res.CODE = 0;
                res.MESSAGE = ex.Message == null ? ex.InnerException.ToString() : ex.Message.ToString();
                return new BadRequestObjectResult(res);
            }

            return new OkObjectResult(res);
        }

        [Authorize]
        [HttpPost("DropdownStorage")]
        public async Task<IActionResult> DropdownStorage()
        {
            var res = new ServiceResponse<ResDropdown>();
            try
            {
                var _ = await _transaction.DropdownStorage();

                if (!_.status)
                {
                    res.CODE = 0;
                    res.MESSAGE = _.message;
                }
                else
                {
                    res.CODE = 1;
                    res.MESSAGE = _.message;
                    res.DATA = _.data;
                }
            }
            catch (Exception ex)
            {
                res.CODE = 0;
                res.MESSAGE = ex.Message == null ? ex.InnerException.ToString() : ex.Message.ToString();
                return new BadRequestObjectResult(res);
            }

            return new OkObjectResult(res);
        }

    }
}
