using Payscrow.Web.HttpAggregator.Models.PaymentInvite;
using System.Net.Http;
using System.Threading.Tasks;

namespace Payscrow.Web.HttpAggregator.Services
{
    public interface IPaymentInviteService
    {
        Task<CreateDealViewModel> GetCreateDealDataModel();
        Task<HttpResponseMessage> CreateNewDealAsync(CreateDealRequestModel model);
        Task<HttpResponseMessage> VerifyAnonymousDealAsync(VerifyDealRequestModel model);
    }
}
