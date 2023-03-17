using System.ComponentModel.DataAnnotations;

namespace employeehub_api.Models
{
    public class AddDepartmentRequest
    {
        [Required]
        [StringLength(50, ErrorMessage = "Department Name should not exceed 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Department Name can only contain letters and spaces")]
        public string departmentName { get; set; }
    }
}
