using System;
using System.Collections.Generic;

namespace AWC_HRMS.Models
{
    public partial class StateMaster
    {
        public StateMaster()
        {
            CandidateMasterCurrentStates = new HashSet<CandidateMaster>();
            CandidateMasterPermanentStates = new HashSet<CandidateMaster>();
            CityMasters = new HashSet<CityMaster>();
            EmployeeMasterCurrentStates = new HashSet<EmployeeMaster>();
            EmployeeMasterPermanentStates = new HashSet<EmployeeMaster>();
        }

        public int StateId { get; set; }
        public int? CountryId { get; set; }
        public string? StateName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public virtual CountryMaster? Country { get; set; }
        public virtual ICollection<CandidateMaster> CandidateMasterCurrentStates { get; set; }
        public virtual ICollection<CandidateMaster> CandidateMasterPermanentStates { get; set; }
        public virtual ICollection<CityMaster> CityMasters { get; set; }
        public virtual ICollection<EmployeeMaster> EmployeeMasterCurrentStates { get; set; }
        public virtual ICollection<EmployeeMaster> EmployeeMasterPermanentStates { get; set; }
    }
}
