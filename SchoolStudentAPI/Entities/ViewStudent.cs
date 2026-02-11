using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolStudentAPI.Entities
{
    [Table("StudentMarksheet")]
    public class ViewStudent
    {
        [Key]
        public int Student_Id { get; set; }
        public string Student_Name { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int? Dept_Id { get; set; }
        public string? Dept {  get; set; }
        public string Blood_Group { get; set; }
        public string Mobile_Number { get; set; }
    }
}
