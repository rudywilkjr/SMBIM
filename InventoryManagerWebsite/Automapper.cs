using AutoMapper;
using DataAccess.DTO;
using DataAccess.Models;

namespace InventoryManagerWebsite
{
    public static class Automapper
    {
        public static void RegisterMappings()
        {
#pragma warning disable 618
            Mapper.CreateMap<Product, ProductDto>();
            Mapper.CreateMap<Location, LocationDto>();
            Mapper.CreateMap<Transfer, TransferDto>();
            Mapper.CreateMap<Direction, DirectionDto>();
            Mapper.CreateMap<LocationType, LocationTypeDto>();
            Mapper.CreateMap<Invoice, InvoiceDto>();
            Mapper.CreateMap<InvoiceProduct, InvoiceProductDto>();
            Mapper.CreateMap<Supplier, SupplierDto>();
            Mapper.CreateMap<SupplierType, SupplierTypeDto>();
#pragma warning restore 618
        }

        
    }
}
