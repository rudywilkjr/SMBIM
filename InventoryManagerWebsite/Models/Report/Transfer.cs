using System;
using System.Collections.Generic;
using DataAccess.DTO;

namespace InventoryManagerWebsite.Models.Report
{
    public class TransferReportModel
    {
        public List<TransferDto> Items { get; set; } 

        public DateTime? SelectedBeginDate { get; set; } 

        public DateTime? SelectedEndDate { get; set; }

    }
}