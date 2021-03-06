﻿using DataAccess.DTO;
using DataAccess.Interface;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public InvoiceDto GetInvoice(int invoiceId)
        {
            Invoice invoice;
            using (var ctx = new StoreEntities())
            {
                invoice = ctx.Invoices
                    .Include("InvoiceProducts.Product")
                    .Include("Supplier.SupplierType")
                    .Where(x => x.Id.Equals(invoiceId)).SingleOrDefault();
            }

            return AutoMapper.Mapper.Map<InvoiceDto>(invoice);
        }

        public List<InvoiceDto> GetInvoices(string searchText)
        {
            List<Invoice> invoiceItems;
            using (var ctx = new StoreEntities())
            {
                int.TryParse(searchText, out int searchTextAsInt);
                invoiceItems = ctx.Invoices
                    .Include("InvoiceProducts.Product")
                    .Include("Supplier.SupplierType")
                    .Where(x => x.Id.Equals(searchTextAsInt) || x.InvoiceProducts.Any(product => product.Product.Name.Equals(searchText))).ToList();
            }

            return AutoMapper.Mapper.Map<List<InvoiceDto>>(invoiceItems);
        }

        public List<InvoiceDto> GetOpenInvoices()
        {
            List<Invoice> invoiceItems;
            using (var ctx = new StoreEntities())
            {
                invoiceItems = ctx.Invoices
                    .Include("InvoiceProducts.Product")
                    .Include("Supplier.SupplierType")
                    .Where(x => x.InvoiceProducts.Sum(y => y.ReceivedQuantity) < x.InvoiceProducts.Sum(y => y.OrderedQuantity)).OrderBy(x => x.CreationDate).ToList();
            }

            return AutoMapper.Mapper.Map<List<InvoiceDto>>(invoiceItems);
        }

        public InvoiceProductDto GetInvoiceProduct(int invoiceProductId)
        {
            InvoiceProduct invoiceProduct;
            using (var ctx = new StoreEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                invoiceProduct = ctx.InvoiceProducts
                    .SingleOrDefault(x => x.Id == invoiceProductId);
            }

            return AutoMapper.Mapper.Map<InvoiceProductDto>(invoiceProduct);
        }

        public InvoiceDto SaveNewPurchaseOrder(short supplierId)
        {
            Invoice invoice;
            using (var ctx = new StoreEntities())
            {
                invoice = new Invoice
                {
                    CreationDate = DateTime.Now,
                    SupplierId = supplierId
                };
                ctx.Invoices.Add(invoice);
                ctx.SaveChanges();
            }
            return AutoMapper.Mapper.Map<InvoiceDto>(invoice);
        }

        public void SaveNewPurchaseOrderProduct(int purchaseOrderId, int productId, decimal productCost, short orderedQuantity)
        {
            using (var ctx = new StoreEntities())
            {
                ctx.InvoiceProducts.Add(new InvoiceProduct
                {
                    InvoiceId = purchaseOrderId,
                    ProductId = productId,
                    Cost = productCost,
                    OrderedQuantity = orderedQuantity,
                    ReceivedQuantity = 0
                });

                ctx.SaveChanges();
            }
        }

    }
}
