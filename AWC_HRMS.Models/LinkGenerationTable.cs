using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AWC_HRMS.Models
{
    public partial class LinkGenerationTable
    {
        public int CandidateId { get; set; }



        [Required(ErrorMessage = "Name Required")]
        public string? CandidateName { get; set; }



        [Required(ErrorMessage = " Email Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string? CandidateEmail { get; set; }
        public string? VerificationCode { get; set; }


        [MaxLength(10)]
        [Required(ErrorMessage = "Mobileno Required ")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong Mobileno")]
        public string? CandidateContactNumber { get; set; }
        public string? Link { get; set; }
        public DateTime DateOfJoining{ get; set; }
        public DateTime? LinkExpiration { get; set; }
        public string? LinkStatus { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
