using ITravel.Domain.Commons;
using ITravel.Domain.Entities.Payme;
using ITravel.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Service.Interfaces
{
    public interface IPaymeService
    {
        order GetLastOrder();

        Task AddOrder(order paymeOrders);

        Task<PaymeResponse<CheckPerformTransactionResult>> CheckPerformTransactionAsync(decimal amount, Account account);
        Task<PaymeResponse<CreateTransactionResult>> CreateTransactionAsync(string id_transaction, long time, decimal amount, Account account);
        Task<PaymeResponse<PerformTransactionResult>> PerformTransactionAsync(string id_transaction);
        Task<PaymeResponse<CancelTransactionResult>> CancelTransactionAsync(string id_transaction, int reason);
        Task<PaymeResponse<CheckTransactionResult>> CheckTransactionAsync(string id_transaction);
        Task<dynamic> GetStatementAsync(long from, long to);
        Task<dynamic> DirectMethod(PaymeRequest request);
    }
}
