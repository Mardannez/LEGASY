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
    
    public partial class UsersAuthorization
    {
        public long entryCode { get; set; }
        public Nullable<long> entryCodeUser { get; set; }
        public Nullable<long> entryCodePermission { get; set; }
        public Nullable<long> userIDcreate { get; set; }
        public Nullable<long> userIDupdate { get; set; }
        public Nullable<System.DateTime> dateCreate { get; set; }
        public Nullable<System.DateTime> dateUpdate { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual UsersPermission UsersPermission { get; set; }
    }
}
