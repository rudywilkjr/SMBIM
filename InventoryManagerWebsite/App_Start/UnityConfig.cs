using DataAccess.Interface;
using DataAccess.Repositories;
using InventoryManagerService.Interface;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace InventoryManagerWebsite
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            //DataAccess Layer
            container.RegisterType<IInvoiceRepository, InvoiceRepository>();
            container.RegisterType<ILocationRepository, LocationRepository>();
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<ISupplierRepository, SupplierRepository>();
            container.RegisterType<ITransferRepository, TransferRepository>();

            //Service Layer
            container.RegisterType<IInvoiceService, InventoryManagerService.Invoice.InvoiceService>();
            container.RegisterType<ILocationService, InventoryManagerService.Location.LocationService>();
            container.RegisterType<IProductService, InventoryManagerService.Product.ProductService>();
            container.RegisterType<ISupplierService, InventoryManagerService.Supplier.SupplierService>();
            container.RegisterType<ITransferService, InventoryManagerService.Transfer.TransferService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}