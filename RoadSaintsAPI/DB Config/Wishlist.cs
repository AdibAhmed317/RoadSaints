//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RoadSaintsAPI.DB_Config
{
    using System;
    using System.Collections.Generic;
    
    public partial class Wishlist
    {
        public int wishlist_id { get; set; }
        public Nullable<int> customer_id { get; set; }
        public Nullable<int> product_id { get; set; }
    
        public virtual Customers Customers { get; set; }
        public virtual Products Products { get; set; }
    }
}