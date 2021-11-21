using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tour.Entites
{
    public class Users
    {
        public System.Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(100,MinimumLength = 6,ErrorMessage ="Password should be between 6 and 100 character")]
        public string Password { get; set; }
        [Compare("Password",ErrorMessage ="Passwords do not match.")]
        public string ConfirmPassword { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(200)]
        public string FullName { get; set; }
        public int Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string BirthPlace { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(13,MinimumLength =10)]
        public string Phone { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedAt { get; set; }
        public virtual string UpdatedBy { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual int IsDeleted { get; set; }
    }
}