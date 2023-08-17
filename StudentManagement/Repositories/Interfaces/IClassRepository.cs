using StudentManagement.DTOs.Output;

namespace StudentManagement.Repositories.Interfaces;

public interface IClassRepository
{
    List<ClassInfoOutput> GetClassesByStudentId(int studentId);
}