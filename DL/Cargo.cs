//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cargo
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public byte[] Create_At { get; set; }
        public Nullable<System.DateTime> Update_At { get; set; }
        public string Company_Id { get; set; }
    
        public virtual Company Company { get; set; }
    }
}
