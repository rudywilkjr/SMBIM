using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.DTO;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class TransferRepository
    {
        public List<TransferDto> GetTransfers()
        {
            using (var ctx = new StoreEntities())
            {
                var transferItems = ctx.Transfers.Include("Direction").Include("Location").Include("Inventory").ToList();
                return AutoMapper.Mapper.Map<List<TransferDto>>(transferItems);
            }
            
        }

        public TransferDto GetTransfer(int id)
        {
            Transfer transferItem;
            using (var ctx = new StoreEntities())
            {
                transferItem = ctx.Transfers.Include("Direction").Include("Location")
                    .Include("Inventory").Single(x => x.Id == id);
            }

            return AutoMapper.Mapper.Map<TransferDto>(transferItem);
        }

        public List<TransferDto> GetTransfers(DateTimeOffset? startDate, DateTimeOffset? endDate)
        {
            List<Transfer> transferItems;
            var dateStart = startDate.GetValueOrDefault();
            var dateEnd = endDate.GetValueOrDefault();
            using (var ctx = new StoreEntities())
            {
                var query = ctx.Transfers.Include("Direction").Include("Location").Include("Location.LocationType")
                    .Include("Product").AsQueryable();

                if (startDate.HasValue)
                {
                    query = query.Where(x => x.ActivityTime >= dateStart);
                }
                if (endDate.HasValue)
                {
                    query = query.Where(x => x.ActivityTime < dateEnd);
                }
                transferItems = query.ToList();
            }

            return AutoMapper.Mapper.Map<List<TransferDto>>(transferItems);
        }

        public TransferDto CreateTransfer(TransferDto transfer)
        {
            Transfer transferItem;
            using (var ctx = new StoreEntities())
            {
                transferItem = new Transfer
                {
                    ActivityTime = DateTimeOffset.Now,
                    SourceLocationId = transfer.SourceLocationId,
                    DestinationLocationId = transfer.DestinationLocationId,
                    ProductId = transfer.ProductId,
                    OriginalQuantity = transfer.OriginalQuantity,
                    NewQuantity = transfer.NewQuantity
                };

                ctx.Transfers.Add(transferItem);
                ctx.SaveChanges();
            }

            return AutoMapper.Mapper.Map<TransferDto>(transferItem);
        }

        public bool DeleteTransfer(long id)
        {
            using (var ctx = new StoreEntities())
            {
                try
                {
                    var transfer = ctx.Transfers.Single(x => x.Id == id);
                    if (transfer != null)
                    {
                        ctx.Transfers.Remove(transfer);
                        ctx.SaveChanges();
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool CreateInventoryAtLocation(int inventoryId, int locationId, int quantity)
        {
            using (var ctx = new StoreEntities())
            {
                var inventoryLocation = new ProductLocation
                {
                    InventoryId = inventoryId,
                    LocationId = locationId,
                    OnHand = quantity
                };

                ctx.ProductLocations.Add(inventoryLocation);
                ctx.SaveChanges();
            }
            return true;
        }

        public bool UpdateInventoryAtLocation(int inventoryId, int locationId, int quantity)
        {
            using (var ctx = new StoreEntities())
            {
                var inventoryLocation =
                    ctx.ProductLocations.Single(x => x.InventoryId == inventoryId && x.LocationId == locationId);

                inventoryLocation.OnHand = quantity;

                ctx.SaveChanges();
            }
            return true;
        }

        public void ReceiveInvoiceInventory(int invoiceProductId, int sourceLocationId, int destinationLocationId, short quantity)
        {
            using (var ctx = new StoreEntities())
            {
                var invoiceProduct = ctx.InvoiceProducts.Single(x => x.Id == invoiceProductId);
                var originalQuantity = invoiceProduct.ReceivedQuantity;
                invoiceProduct.ReceivedQuantity = quantity;

                if (originalQuantity != invoiceProduct.ReceivedQuantity)
                {
                    CreateTransfer(new TransferDto
                    {
                        ActivityTime = DateTimeOffset.Now,
                        ProductId = invoiceProduct.ProductId,
                        SourceLocationId = sourceLocationId,
                        DestinationLocationId = destinationLocationId,
                        OriginalQuantity = originalQuantity,
                        NewQuantity = invoiceProduct.ReceivedQuantity
                    });

                    ctx.SaveChanges();
                }

            }
        }
    }
}
