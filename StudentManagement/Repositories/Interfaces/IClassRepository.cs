using StudentManagement.DTOs.Input;
using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces
{
    public interface IClassRepository : IRepository<Class>
    {
        public void AddStudentToClass(List<int> _studentIds, int _classId);

        public void RemoveStudentFromClass(int _studentId, int _classId);

        public List<Class> FindClassByKeyWord(string keyword);

        List<ClassInfoOutput> GetClassesByStudentId(int studentId);

        public List<Class> GetAllClasses();

        public Class GetClassById(int classId);

        public void DeleteClass(int classId);

        public void AddNewClass(NewClassInput newClassInput);

        List<Class> GetClassesByDate(DateTime date);

        List<StudentAttendanceOutput> GetStudentBySlotAndDate(int slotId, DateTime date);

        void TakeAttendance(int slotId, DateTime date, List<StudentAttendanceInput> studentAttendanceInputs);
        void TakeMakeUpAttendance(int slotId, DateTime date, List<StudentAttendanceInput> studentAttendanceInputs);
        List<StudentAttendanceOutput> GetStudentMakeUpBySlotAndDate(DateTime date);
    }
}