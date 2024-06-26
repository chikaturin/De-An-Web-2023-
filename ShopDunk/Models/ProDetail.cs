//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopDunk.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProDetail()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
        public int SumStorage { get; set; }
        public int ProDeID { get; set; }
        public int ProID { get; set; }
        public int ColorID { get; set; }
        public int RemainQuantity { get; set; }
        public Nullable<int> SoldQuantity { get; set; }
        public Nullable<int> ViewQuantity { get; set; }
        public Nullable<int> IDVoucher { get; set; }
        public string ConnectNetwork { get; set; }
        public int IDMemory { get; set; }
    
        public virtual Color Color { get; set; }
        public virtual Memory Memory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Product Product { get; set; }
        public virtual Voucher Voucher { get; set; }
    }
}
