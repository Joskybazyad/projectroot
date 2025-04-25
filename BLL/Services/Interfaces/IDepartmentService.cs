using BLL.DTO.DepartmentDto;

namespace BLL.Services.Interfaces
{
    public interface IDepartmentService
    {
        int AddDepartment(CreatedDepartmentDto departmentDto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmenById(int id);
        int UpdateDepartment(UpdateDepartmentDto departmentDto);
    }
}