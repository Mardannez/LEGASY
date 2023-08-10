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
    
    public partial class Partners
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Partners()
        {
            this.Partners1 = new HashSet<Partners>();
            this.Users = new HashSet<Users>();
        }
    
        public long entryCode { get; set; }
        public Nullable<long> entryCodeExternal { get; set; }
        public Nullable<long> entryCodeParent { get; set; }
        public Nullable<long> entryCodeCity { get; set; }
        public Nullable<long> entryCodeSubsidiary { get; set; }
        public Nullable<long> entryCodeClass { get; set; }
        public Nullable<long> entryCodePerson { get; set; }
        public Nullable<long> entryCodeType { get; set; }
        public Nullable<long> entryCodeGender { get; set; }
        public Nullable<long> entryCodeCivilStatus { get; set; }
        public Nullable<long> entryCodeCasesArea { get; set; }
        public bool entryCodeStatus { get; set; }
        public Nullable<long> entryType { get; set; }
        public string entryFirstName { get; set; }
        public string entryLastName { get; set; }
        public string entryForeingName { get; set; }
        public string entryDescription { get; set; }
        public string entryTitleContactPerson { get; set; }
        public string entryTitleJobPosition { get; set; }
        public string entryTitleProfession { get; set; }
        public string entryID1 { get; set; }
        public string entryID2 { get; set; }
        public string entryID3 { get; set; }
        public string entryID4 { get; set; }
        public string entryPhone1 { get; set; }
        public string entryPhone2 { get; set; }
        public string entryPhone3 { get; set; }
        public string entryPhone4 { get; set; }
        public string entryEmail { get; set; }
        public string entryComments { get; set; }
        public string entryAddress { get; set; }
        public Nullable<System.DateTime> entryWorkStartDate { get; set; }
        public Nullable<System.DateTime> entryWorkEndDate { get; set; }
        public Nullable<System.DateTime> entryWorkDemandDate { get; set; }
        public Nullable<decimal> entryWorkSalary { get; set; }
        public string entryWorkAddresses { get; set; }
        public Nullable<long> userIDsystem { get; set; }
        public Nullable<long> userIDcreate { get; set; }
        public Nullable<long> userIDupdate { get; set; }
        public Nullable<System.DateTime> dateCreate { get; set; }
        public Nullable<System.DateTime> dateUpdate { get; set; }
    
        public virtual CasesAreas CasesAreas { get; set; }
        public virtual Cities Cities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Partners> Partners1 { get; set; }
        public virtual Partners Partners2 { get; set; }
        public virtual Subsidiaries Subsidiaries { get; set; }
        public virtual PartnersClass PartnersClass { get; set; }
        public virtual PartnersPerson PartnersPerson { get; set; }
        public virtual PartnersType PartnersType { get; set; }
        public virtual PartnersGender PartnersGender { get; set; }
        public virtual PartnersCivilStatus PartnersCivilStatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users { get; set; }
    }
}
