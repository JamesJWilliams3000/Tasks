//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tasks
{
    using System;
    using System.Collections.Generic;
    
    public partial class DatesSubJob
    {
        public int Date_ID { get; set; }
        public int Job_ID { get; set; }
        public int Total_Done { get; set; }
    
        public virtual Date Date { get; set; }
        public virtual SubJob SubJob { get; set; }
    }
}