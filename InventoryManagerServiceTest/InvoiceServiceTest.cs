using DataAccess.DTO;
using DataAccess.Interface;
using InventoryManagerService.Invoice;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace InventoryManagerServiceTest
{
    [TestClass]
    public class InvoiceServiceTest
    {
        [TestMethod]
        public void GetInvoicesNonEmptyText()
        {
            //arrange
            var invoicesUsingSearchText = new List<InvoiceDto>();
            var searchText = "test";

            var mockRepo = new Mock<IInvoiceRepository>();
            mockRepo.Setup(x => x.GetInvoices(searchText)).Verifiable();

            var invoiceService = new InvoiceService(mockRepo.Object, null);
            var result = invoiceService.GetInvoices(searchText);
            mockRepo.Verify(x => x.GetInvoices(searchText), Times.Once);

        }

        [TestMethod]
        public void GetInvoicesEmptyText()
        {
            //arrange
            var invoicesUsingEmptySearchText = new List<InvoiceDto>();
            var searchText = string.Empty;

            var mockRepo = new Mock<IInvoiceRepository>();
            mockRepo.Setup(x => x.GetOpenInvoices()).Verifiable();

            var invoiceService = new InvoiceService(mockRepo.Object, null);
            var result = invoiceService.GetInvoices(searchText);
            mockRepo.Verify(x => x.GetOpenInvoices(), Times.Once);

        }

        [TestMethod]
        public void GetInvoicesNullText()
        {
            //arrange
            var invoicesUsingEmptySearchText = new List<InvoiceDto>();
            string searchText = null;

            var mockRepo = new Mock<IInvoiceRepository>();
            mockRepo.Setup(x => x.GetOpenInvoices()).Returns(invoicesUsingEmptySearchText);

            var invoiceService = new InvoiceService(mockRepo.Object, null);
            var result = invoiceService.GetInvoices(searchText);
            Assert.AreEqual(result, invoicesUsingEmptySearchText);

        }

        [TestMethod]
        [ExpectedException(typeof(System.ApplicationException))]
        public void SaveNewPurchaseOrderProductWithInvalidPurchaseOrderId()
        {
            var mockRepo = new Mock<IInvoiceRepository>();

            var invoiceService = new InvoiceService(mockRepo.Object, null);
            invoiceService.SaveNewPurchaseOrderProduct(0, 0, 0, 0);
            //should not get further
        }

        [TestMethod]
        [ExpectedException(typeof(System.ApplicationException))]
        public void SaveNewPurchaseOrderProductWithNullProduct()
        {
            ProductDto product = null;

            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(x => x.GetProduct(0)).Returns(product);

            var invoiceService = new InvoiceService(null, mockRepo.Object);
            invoiceService.SaveNewPurchaseOrderProduct(1, 0, 0, 0);
            //should not get further

        }

        [TestMethod]
        [ExpectedException(typeof(System.ApplicationException))]
        public void SaveNewPurchaseOrderProductWithInvalidCost()
        {
            var mockInvoiceRepo = new Mock<IInvoiceRepository>();
            var mockProductRepo = new Mock<IProductRepository>();

            var invoiceService = new InvoiceService(mockInvoiceRepo.Object, mockProductRepo.Object);
            invoiceService.SaveNewPurchaseOrderProduct(1, 1, (decimal) -0.10, 0);
            //should not get further

        }

        [TestMethod]
        [ExpectedException(typeof(System.ApplicationException))]
        public void SaveNewPurchaseOrderProductWithInvalidQuantity()
        {
            var mockInvoiceRepo = new Mock<IInvoiceRepository>();
            var mockProductRepo = new Mock<IProductRepository>();

            var invoiceService = new InvoiceService(mockInvoiceRepo.Object, mockProductRepo.Object);
            invoiceService.SaveNewPurchaseOrderProduct(1, 1, (decimal) 1.00, 0);
            //should not get further

        }

    }
}
