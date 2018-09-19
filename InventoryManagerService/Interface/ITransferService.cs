using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagerService.Interface
{
    public interface ITransferService
    {
        TransferDto GetTransfer(int transferId);
        List<TransferDto> GetAllTransfers();
        List<TransferDto> GetTransfers(DateTimeOffset? beginDate, DateTimeOffset? endDate);
        void TransferOutProducts(List<InvoiceProductDto> products, int leavingLocationId, int arrivingLocationId, int quantity);
        void ReceiveInvoiceProduct(int invoiceProductId, int sourceLocationId, int destinationLocationId, short quantity);
        void Transfer(int productId, int sourceLocationId, int destinationLocationId, int quantity);

    }
}
