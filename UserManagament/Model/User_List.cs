//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UserManagament.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class User_List
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User_List()
        {
            this.Login_History = new HashSet<Login_History>();
        }
    
        public long ID { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string User_pass { get; set; }
        public string Status { get; set; }
        public byte[] User_image { get; set; }
        public string Phone_number { get; set; }
        public string Age { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Login_History> Login_History { get; set; }
    }
}