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
    
    public partial class Job
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Job()
        {
            this.DatesJobs = new HashSet<DatesJob>();
        }
    
        public int Job_ID { get; set; }
        public string Name { get; set; }
        public int Category { get; set; }
        public Nullable<System.DateTime> Last_Finished { get; set; }
        public System.DateTime Date_Added { get; set; }
        public Nullable<System.DateTime> Deadline { get; set; }
        public int Frequency { get; set; }
        public int Importance { get; set; }
        public bool Completed { get; set; }
        public int Job_Type { get; set; }
        public int Est_Time { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatesJob> DatesJobs { get; set; }
        public virtual SubJob SubJob { get; set; }
    }
}