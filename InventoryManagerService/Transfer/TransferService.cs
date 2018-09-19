
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.DTO;
using DataAccess.Interface;
using DataAccess.Repositories;
using InventoryManagerService.Interface;

namespace InventoryManagerService.Transfer
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepository transferRepository;
        private readonly IProductRepository productRepository;
        private readonly ILocationRepository locationRepository;
        private readonly IInvoiceRepository invoiceRepository;

        public TransferService(ITransferRepository transferRepository, IProductRepository productRepository, ILocationRepository locationRepository, IInvoiceRepository invoiceRepository)
        {
            this.transferRepository = transferRepository;
            this.productRepository = productRepository;
            this.locationRepository = locationRepository;
            this.invoiceRepository = invoiceRepository;
        }

        public TransferDto GetTransfer(int transferId)
        {
            return transferRepository.GetTransfer(transferId);
        }

        public List<TransferDto> GetAllTransfers()
        {
            return transferRepository.GetTransfers();
        }

        public List<TransferDto> GetTransfers(DateTimeOffset? beginDate, DateTimeOffset? endDate)
        {
            return transferRepository.GetTransfers(beginDate, endDate);
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
                if (locationRepository.GetLocation(sourceLocationId) == null)
                {
                    throw new ApplicationException("Invalid source location.");
                }

                if (locationRepository.GetLocation(destinationLocationId) == null)
                {
                    throw new ApplicationException("Invalid destination location.");
                }

                transferRepository.ReceiveInvoiceInventory(invoiceProductId, sourceLocationId, destinationLocationId, quantity);
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

        public void Transfer(int productId, int sourceLocationId, int destinationLocationId, int quantity)
        {
            var productItem = productRepository.GetProduct(productId);
            var originalLocationItem = locationRepository.GetLocation(sourceLocationId);
            var newLocationItem = locationRepository.GetLocation(destinationLocationId);

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
                transferRepository.CreateTransfer(productId, sourceLocationId, destinationLocationId, quantity);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Transfer reverted. Please validate and try again.");
            }

        }
    }
}
