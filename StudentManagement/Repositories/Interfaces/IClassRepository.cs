using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces
{
    public interface IClassRepository : IRepository<Class>
    {
        public void AddStudentToClass(int _studentId, int _classId);
        public void RemoveStudentFromClass(Student _student);
        public List<Class> FindClassByKeyWord(string keyword);

        List<ClassInfoOutput> GetClassesByStudentId(int studentId);
    }
}