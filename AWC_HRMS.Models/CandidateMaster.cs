using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AWC_HRMS.Models
{
    public partial class CandidateMaster
    {
        public CandidateMaster()
        {
            CandidateEducations = new HashSet<CandidateEducation>();
            CandidateEmployements = new HashSet<CandidateEmployement>();


            stateMasters = new List<StateMaster>();
            stateMasters.Add(new StateMaster
            {
                   StateId = 0,
                    StateName = "--All--"
                });

      
        cityMasters = new List<CityMaster>();
            cityMasters.Add(new CityMaster
            {
                   CityId = 0,
                    CityName = "--All--"
                });

    }

        public int Id { get; set; }
        [Required(ErrorMessage ="Enter Candidate Id")]
        public string CandidateId { get; set; } = null!;
        public string? CandidateName { get; set; }
        public string? CandidateEmailId { get; set; }
        public string? CandidateContactNumber { get; set; }
        public string? Gender { get; set; }
        public string? CandidatePhoto { get; set; }
        public string? CandidatePhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? Dob { get; set; }
        public string? MaritalStatus { get; set; }
        public string? FatherName { get; set; }
        public string? PermanentAddress { get; set; }
        public int? PermanentStateId { get; set; }
        public int? PermanentCityId { get; set; }
        public string? PermanentPinCode { get; set; }
        public string? CurrentAddress { get; set; }
        public int? CurrentStateId { get; set; }
        public int? CurrentCityId { get; set; }
        public string? CurrentPinCode { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactNo { get; set; }
        public string? EmergencyContactRelation { get; set; }
        public string? AadharNumber { get; set; }
        public string? PanNumber { get; set; }
        public string? PassportNumber { get; set; }
        public string? CandidateImage { get; set; }
        public string? AadharImage { get; set; }
        public string? PanCardImage { get; set; }
        public string? PassportImage { get; set; }
        public string? BankAccountHolderName { get; set; }
        public string? BankName { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? IfscCode { get; set; }
        public string? BankBranchAddress { get; set; }
        public string? PassBookCancelCheque { get; set; }
        //public string? PermanentState { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public virtual CityMaster? CurrentCity { get; set; }
        public virtual StateMaster? CurrentState { get; set; }
        public virtual CityMaster? PermanentCity { get; set; }
        public virtual StateMaster? PermanentState { get; set; }

        public List<CityMaster> cityMasters { get; set; }

        public List<StateMaster> stateMasters { get; set; }
        public virtual ICollection<CandidateEducation> CandidateEducations { get; set; }
        public virtual ICollection<CandidateEmployement> CandidateEmployements { get; set; }
    }
}
