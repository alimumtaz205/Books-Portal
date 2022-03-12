using BookPortalAPI.Models.Finance.Request;
using BookPortalAPI.Models.Finance.Response;

namespace BookPortalAPI.Repositories
{
    public interface IFinanceRepository
    {
        public GetFinanceResponse GetFinance();
        public AddFinanceResponse AddFinance(AddFinanceRequest request);
        public DeleteFinanceResponse DeleteFinance(DeleteFinanceRequest request);
        public UpdateFinanceResponse UpdateFinance(UpdateFinanceRequest request);
    }
}