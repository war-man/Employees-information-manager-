namespace EmployeesInformationManager.Models
{
    ///<remarks>Used to define the many to many relation ship between employee and skill
    public class EmployeeSkill
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}