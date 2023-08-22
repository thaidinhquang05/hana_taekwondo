using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces
{
    public interface IClassRepository : IRepository<Class>
    {
        public void AddStudentToClass(List<int> _studentIds, int _classId);
        public void RemoveStudentFromClass(Student _student);
        public List<Class> FindClassByKeyWord(string keyword);

        List<ClassInfoOutput> GetClassesByStudentId(int studentId);
        public List<Class> GetAllClasses();
    }
}