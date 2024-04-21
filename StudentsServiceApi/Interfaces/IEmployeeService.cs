using StudentsServiceApi.Services;

namespace StudentsServiceApi.Interfaces
{
    public interface IEmployeeService
    {
        public Task<List<Employee>> GetEmployees();
        public Task<Employee> GetEmployeeById(int id);
        public Task AddEmployee(Employee employee);
        public Task UpdateEmployeeAsync(int id, Employee employee);
        public Task DeleteEmployee(int id);
    }
}
