//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bank_Application.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Openingaccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Openingaccount()
        {
            this.accounts = new HashSet<account>();
        }
    
        public int CustomerID { get; set; }
        public string account_number { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Fathername { get; set; }
        public long Adharno { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public System.DateTime DOB { get; set; }
        public int Age { get; set; }
        public string accounttype { get; set; }
        public string Phoneno { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<account> accounts { get; set; }
    }
}