//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLTP.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Discount_by_Rank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Discount_by_Rank()
        {
            this.Bill_detail = new HashSet<Bill_detail>();
        }
    
        public string Discount_id { get; set; }
        public string Rank_id { get; set; }
        public double Discount_percent { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill_detail> Bill_detail { get; set; }
        public virtual Rank Rank { get; set; }
    }
}