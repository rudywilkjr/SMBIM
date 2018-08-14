
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.DTO;
using DataAccess.Repositories;

namespace InventoryManagerService.Transfer
{
    public class TransferService
    {
        private readonly TransferRepository _transferRepository = new TransferRepository();
        private readonly ProductRepository _inventoryRepository = new ProductRepository();
        private readonly LocationRepository _locationRepository = new LocationRepository();
        private readonly InvoiceRepository _invoiceRepository = new InvoiceRepository();

        public TransferDto GetTransfer(int transferId)
        {
            return _transferRepository.GetTransfer(transferId);
        }

        public List<TransferDto> GetAllTransfers()
        {
            return _transferRepository.GetTransfers();
        }

        public List<TransferDto> GetTransfers(DateTimeOffset? beginDate, DateTimeOffset? endDate)
        {
            return _transferRepository.GetTransfers(beginDate, endDate);
        }

        public void TransferOutProducts(List<InvoiceProductDto> products, int leavingLocationId, int arrivingLocationId, int quantity)
        {
            foreach (var product in products)
            {
                Transfer(product.Id, leavingLocationId, arrivingLocationId, quantity);
            }
        }

        public void ReceiveInvoiceProduct(int invoiceProductId, int sourceLocationId, int destinationLocationId, short quantity)
        {
            try
            {
                if (_locationRepository.GetLocation(sourceLocationId) == null)
                {
                    throw new ApplicationException("Invalid source location.");
                }

                if (_locationRepository.GetLocation(destinationLocationId) == null)
                {
                    throw new ApplicationException("Invalid destination location.");
                }

                _transferRepository.ReceiveInvoiceInventory(invoiceProductId, sourceLocationId, destinationLocationId, quantity);
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        public void Transfer(int productId, int sourceLocationId, int destinationLocationId, int quantity)
        {
            var productItem = _inventoryRepository.GetProduct(productId);
            var originalLocationItem = _locationRepository.GetLocation(sourceLocationId);
            var newLocationItem = _locationRepository.GetLocation(destinationLocationId);

            if (productItem == null)
            {
                throw new ApplicationException("Inventory item not found.");
            }

            if (originalLocationItem == null)
            {
                throw new ApplicationException("Source location not found.");
            }

            if (newLocationItem == null)
            {
                throw new ApplicationException("Destination location not found.");
            }

            try
            {
                _transferRepository.CreateTransfer(productId, sourceLocationId, destinationLocationId, quantity);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Transfer reverted. Please validate and try again.");
            }

        }
    }
}
