//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LEGASY.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PartnersClass
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PartnersClass()
        {
            this.Partners = new HashSet<Partners>();
            this.PartnersPerson = new HashSet<PartnersPerson>();
            this.PartnersGender = new HashSet<PartnersGender>();
        }
    
        public long entryCode { get; set; }
        public bool entryCodeStatus { get; set; }
        public string entryName { get; set; }
        public string entryDescription { get; set; }
        public Nullable<long> userIDcreate { get; set; }
        public Nullable<long> userIDupdate { get; set; }
        public Nullable<System.DateTime> dateCreate { get; set; }
        public Nullable<System.DateTime> dateUpdate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Partners> Partners { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartnersPerson> PartnersPerson { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartnersGender> PartnersGender { get; set; }
    }
}