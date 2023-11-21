namespace DDB.ProgDec.UI.ViewModels
{
    public class StudentVM
    {
        public Student Student { get; set; }
        public List<Advisor> Advisors { get; set; } // ALL  of the Advisors
        public IEnumerable<int> AdvisorIds { get; set; } // The new Advisors for the student

        public StudentVM()
        {
            Advisors = AdvisorManager.Load();
        }

        public StudentVM(int id)
        {
            Advisors = AdvisorManager.Load();
            Student = StudentManager.LoadByID(id);
            AdvisorIds = Student.Advisors.Select(a => a.Id);
        }

    }
}
