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
            mockRepo.Setup(x => x.GetInvoices(searchText)).Returns(invoicesUsingSearchText);

            var invoiceService = new InvoiceService(mockRepo.Object, null);
            var result = invoiceService.GetInvoices(searchText);
            Assert.AreEqual(result, invoicesUsingSearchText);

        }

        [TestMethod]
        public void GetInvoicesEmptyText()
        {
            //arrange
            var invoicesUsingEmptySearchText = new List<InvoiceDto>();
            var searchText = string.Empty;

            var mockRepo = new Mock<IInvoiceRepository>();
            mockRepo.Setup(x => x.GetOpenInvoices()).Returns(invoicesUsingEmptySearchText);

            var invoiceService = new InvoiceService(mockRepo.Object, null);
            var result = invoiceService.GetInvoices(searchText);
            Assert.AreEqual(result, invoicesUsingEmptySearchText);

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
    }
}
