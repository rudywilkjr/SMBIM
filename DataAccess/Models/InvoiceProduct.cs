//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class InvoiceProduct
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public decimal Cost { get; set; }
    
        public virtual Invoice Invoice { get; set; }
        public virtual Product Product { get; set; }
    }
}
