﻿using System.ComponentModel.DataAnnotations;

namespace employeehub_api.Models
{
    public class AddEmployeeRequest
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
        //[RegularExpression(@"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$", ErrorMessage = "Salary can't include a leading zero")]
        public DateTime dob { get; set; }

        [Required]
        public Guid DepartmentId { get; set; }

    }
}
