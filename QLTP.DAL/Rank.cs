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
    
    public partial class Rank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rank()
        {
            this.Customer = new HashSet<Customer>();
            this.Discount_by_Rank = new HashSet<Discount_by_Rank>();
            this.Discount_by_Rank1 = new HashSet<Discount_by_Rank>();
        }
    
        public string Rank_id { get; set; }
        public string Rank_name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Discount_by_Rank> Discount_by_Rank { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Discount_by_Rank> Discount_by_Rank1 { get; set; }
    }
}
