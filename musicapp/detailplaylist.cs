//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace musicapp
{
    using System;
    using System.Collections.Generic;
    
    public partial class detailplaylist
    {
        public int iddetailPL { get; set; }
        public int idsong { get; set; }
        public int idPlaylist { get; set; }
    
        public virtual playlist playlist { get; set; }
        public virtual song song { get; set; }
    }
}
