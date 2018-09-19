using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface ITransferRepository
    {
        List<TransferDto> GetTransfers();
        TransferDto GetTransfer(int id);
        List<TransferDto> GetTransfers(DateTimeOffset? startDate, DateTimeOffset? endDate);
        TransferDto CreateTransfer(int productId, int sourceLocationId, int destinationLocationId, int quantity);
        bool DeleteTransfer(long id);
        void ReceiveInvoiceInventory(int invoiceProductId, int sourceLocationId, int destinationLocationId, short quantity);

    }
}
