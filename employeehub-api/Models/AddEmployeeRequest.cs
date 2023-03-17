namespace employeehub_api.Models
{
    public class AddEmployeeRequest
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public double salary { get; set; }
        public DateTime dob { get; set; }

        public string departmentName { get; set; }
    }
}
