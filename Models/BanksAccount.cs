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
    
    public partial class BanksAccount
    {
        public long entryCode { get; set; }
        public long entryCodeAccountType { get; set; }
        public long entryCodeCurrency { get; set; }
        public long entryCodeBankType { get; set; }
        public bool entryCodeStatus { get; set; }
        public string entryBankName { get; set; }
        public string entryAcctName { get; set; }
        public string entryOwnerName { get; set; }
        public Nullable<long> userIDcreate { get; set; }
        public Nullable<long> userIDupdate { get; set; }
        public Nullable<System.DateTime> dateCreate { get; set; }
        public Nullable<System.DateTime> dateUpdate { get; set; }
    
        public virtual AccountTypes AccountTypes { get; set; }
        public virtual Banks Banks { get; set; }
        public virtual Currencies Currencies { get; set; }
    }
}
