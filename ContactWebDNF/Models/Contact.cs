using System;
using System.ComponentModel.DataAnnotations;

namespace ContactWebDNF.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(ContactWebConstants.MAX_FIRST_NAME_LENGTH)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "last name is required")]
        [StringLength(ContactWebConstants.MAX_LAST_NAME_LENGTH)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage ="Email is invalid")]
        [StringLength(ContactWebConstants.MAX_EMAIL_LENGTH)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone no is required")]
        [Phone(ErrorMessage ="Phone is invalid")]
        public string Phone { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(ContactWebConstants.MAX_ADDRESS_LENGTH)]
        public string Address1 { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(ContactWebConstants.MAX_ADDRESS_LENGTH)]
        public string Address2 { get; set; }

        [Required]
        [StringLength(ContactWebConstants.MAX_CITY_LENGTH)]
        public string City { get; set; }

        [Required]
        [Display(Name ="Postcode")]
        [StringLength(ContactWebConstants.MAX_POSTCODE_LENGTH),MinLength(ContactWebConstants.MIN_POSTCODE_LENGTH)]
        public string Postcode { get; set; }

        public int StateId { get; set; }
        public virtual State State { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Display(Name="Full Name")]
        public string ContactFullName => $"{FirstName} {LastName}";

        public string ContactFullAddress => string.IsNullOrWhiteSpace(Address2)
            ? $"{Address1} , {City} {State.Abbreviation}, {Postcode}"
            : $"{Address1} - {Address2}, {City} {State.Abbreviation}, {Postcode}";
    }
}