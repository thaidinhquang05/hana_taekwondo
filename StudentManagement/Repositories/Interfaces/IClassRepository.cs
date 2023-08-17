using StudentManagement.Models;

namespace StudentManagement.Repositories.Interfaces
{
    public interface IClassRepository : IRepository<Class>
{
        public void addStudentToClass(Student _student, Class _class);
        public void removeStudentFromClass(Student _student);
    }
    List<ClassInfoOutput> GetClassesByStudentId(int studentId);
}