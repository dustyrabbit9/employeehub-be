using System.ComponentModel.DataAnnotations;

namespace employeehub_api.Models
{
    public class UpdateEmployeeRequest
    {
        [Required]
        [StringLength(50, ErrorMessage = "First Name should not exceed 50 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name must contain letters with no spaces")]
        public string firstName { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Last Name should not exceed 50 characters")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name must contain letters with no spaces")]
        public string lastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Email address should not exceed 50 characters")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Please enter a valid email adress")]
        public string email { get; set; }

        [Required]
        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Salary can't include a leading zero")]
        public double salary { get; set; }

        [Required]
        public DateTime dob { get; set; }

        [Required]
        public Guid DepartmentId { get; set; }
    }
}

