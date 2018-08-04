
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
                TransferOut(product.Id, leavingLocationId, arrivingLocationId, quantity);
            }
        }

        public void TransferOut(int productId, int leavingLocationId, int arrivingLocationId, int quantity)
        {
            var productItem = _inventoryRepository.GetProduct(productId);
            var originalLocationItem = _locationRepository.GetLocation(leavingLocationId);
            var newLocationItem = _locationRepository.GetLocation(arrivingLocationId);
            LocationsWithProductDto inventoryAtSourceLocation;
            LocationsWithProductDto inventoryAtDestinationLocation;
            try
            {
                if (originalLocationItem.LocationType.Description == "Internal")
                {
                    inventoryAtSourceLocation =
                        _inventoryRepository.GetInternalLocationsWithProduct(productId, leavingLocationId).Single();
                }
                else
                {
                    inventoryAtSourceLocation = new LocationsWithProductDto
                    {
                        ProductId = productId,
                        ProductName = _inventoryRepository.GetProduct(productId).Name,
                        LocationDescription = originalLocationItem.Description,
                        LocationId = originalLocationItem.Id,
                        QuantityOnHand = null
                    };
                }
                
            }
            catch (ArgumentNullException)
            {
                throw new ApplicationException("No inventory found at this location.");
            }
            catch (Exception)
            {
                throw new ApplicationException("General error retrieving inventory at location.");
            }
            if (inventoryAtSourceLocation.QuantityOnHand < quantity)
            {
                throw new ApplicationException("Insufficient inventory at source location to fulfill this transfer.");
            }

            try
            {
                inventoryAtDestinationLocation =
                    _inventoryRepository.GetInternalAndExternalLocationsWithProduct(productId, arrivingLocationId).Single();
            }
            catch (InvalidOperationException)
            {
                inventoryAtDestinationLocation = new LocationsWithProductDto
                {
                    ProductId = productItem.Id,
                    LocationId = arrivingLocationId
                };
            }
            catch (Exception)
            {
                throw new ApplicationException("General error retrieving inventory at location.");
            }

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

            var incomingTransfer = new TransferDto
            {
                ActivityTime = DateTimeOffset.Now,
                DirectionId = 1, //Incoming
                ProductId = productItem.Id,
                LocationId = newLocationItem.Id,
                OriginalQuantity = inventoryAtDestinationLocation.QuantityOnHand.GetValueOrDefault(),
                NewQuantity = inventoryAtDestinationLocation.QuantityOnHand.GetValueOrDefault() + quantity
            };

            var outgoingTransfer = new TransferDto
            {
                ActivityTime = DateTimeOffset.Now,
                DirectionId = 2, //Outgoing
                ProductId = productItem.Id,
                LocationId = originalLocationItem.Id,
                OriginalQuantity = inventoryAtSourceLocation.QuantityOnHand.GetValueOrDefault(),
                NewQuantity = inventoryAtSourceLocation.QuantityOnHand.GetValueOrDefault() - quantity
            };

            try
            {
                switch (originalLocationItem.LocationType.Description)
                {
                    case "Internal":
                        incomingTransfer = _transferRepository.CreateTransfer(incomingTransfer);
                        outgoingTransfer = _transferRepository.CreateTransfer(outgoingTransfer);
                        _transferRepository.UpdateInventoryAtLocation(inventoryAtSourceLocation.ProductId,
                            inventoryAtSourceLocation.LocationId, inventoryAtSourceLocation.QuantityOnHand.GetValueOrDefault() - quantity);
                        break;
                    default:
                        incomingTransfer = _transferRepository.CreateTransfer(incomingTransfer);
                        break;
                }


                if (inventoryAtDestinationLocation.ProductName == null)
                {
                    _transferRepository.CreateInventoryAtLocation(inventoryAtDestinationLocation.ProductId,
                        inventoryAtDestinationLocation.LocationId,
                        quantity);
                }
                else
                {
                    _transferRepository.UpdateInventoryAtLocation(inventoryAtDestinationLocation.ProductId,
                        inventoryAtDestinationLocation.LocationId, inventoryAtDestinationLocation.QuantityOnHand.GetValueOrDefault() + quantity);
                }

            }
            catch (Exception e)
            {
                if (incomingTransfer.Id > 0)
                {
                    _transferRepository.DeleteTransfer(incomingTransfer.Id);
                }
                if (outgoingTransfer.Id > 0)
                {
                    _transferRepository.DeleteTransfer(outgoingTransfer.Id);
                }
                throw new ApplicationException("Transfer reverted. Please validate and try again.");
            }

        }
    }
}
