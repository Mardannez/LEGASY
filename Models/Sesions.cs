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
    
    public partial class Sesions
    {
        public long entryCode { get; set; }
        public Nullable<long> entryCodeUser { get; set; }
        public string DispositivoCliente { get; set; }
        public string PlataformaCliente { get; set; }
        public string NavegadorCliente { get; set; }
        public string HostCliente { get; set; }
        public string Token { get; set; }
        public string Key { get; set; }
        public Nullable<System.DateTime> Creacion { get; set; }
        public Nullable<System.DateTime> Expiracion { get; set; }
        public Nullable<int> Estado { get; set; }
    
        public virtual Users Users { get; set; }
    }
}
