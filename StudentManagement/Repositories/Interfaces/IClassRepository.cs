using StudentManagement.DTOs.Output;
using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces
{
    public interface IClassRepository : IRepository<Class>
    {
        public void AddStudentToClass(Student _student, Class _class);
        public void RemoveStudentFromClass(Student _student);
        public List<Class> FindClassByKeyWord(string keyword);

        List<ClassInfoOutput> GetClassesByStudentId(int studentId);
    }
}