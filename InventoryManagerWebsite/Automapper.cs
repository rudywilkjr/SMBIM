using AutoMapper;
using DataAccess.DTO;
using DataAccess.Models;
using InventoryManagerWebsite.Models.Invoice;

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
            Mapper.CreateMap<LocationType, LocationTypeDto>();
            Mapper.CreateMap<Invoice, InvoiceDto>();
            Mapper.CreateMap<InvoiceProduct, InvoiceProductDto>();
            Mapper.CreateMap<Supplier, SupplierDto>();
            Mapper.CreateMap<SupplierType, SupplierTypeDto>();
            Mapper.CreateMap<InvoiceDto, InvoiceModel>();
            Mapper.CreateMap<InvoiceProductDto, InvoiceProductModel>();
            Mapper.CreateMap<InvoiceProductDto, InvoiceProductModel>()
                .ForMember(destination => destination.Name,
                map => map.MapFrom(source => source.Product.Name));
                
#pragma warning restore 618
        }

        
    }
}
