using StudentsServiceApi.Interfaces;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Data;
namespace StudentsServiceApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        //private readonly string _connectionString = "server=localhost;port=3300;user=root;password=8josd12M;database=university;";
        private readonly string _connectionString;

        public EmployeeService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = new List<Employee>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT Id, Team, TabNumber, LastName, FirstName, SecondName FROM employees";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var employee = new Employee
                        {
                            Id = reader.GetInt32("Id"),
                            Team = reader.GetString("Team"),
                            TabNumber = reader.GetInt32("TabNumber"),
                            LastName = reader.GetString("LastName"),
                            FirstName = reader.GetString("FirstName"),
                            SecondName = reader.GetString("SecondName")
                        };
                        employees.Add(employee);
                    }
                }
            }
            return employees;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "SELECT * FROM employees WHERE Id = @id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Employee
                            {
                                Id = reader.GetInt32("Id"),
                                Team = reader.GetString("Team"),
                                TabNumber = reader.GetInt32("TabNumber"),
                                LastName = reader.GetString("LastName"),
                                FirstName = reader.GetString("FirstName"),
                                SecondName = reader.GetString("SecondName")
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public async Task AddEmployee(Employee employee)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "INSERT INTO employees (Team, TabNumber, LastName, FirstName, SecondName) VALUES (@team, @tabNumber, @lastName, @firstName, @secondName)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@team", employee.Team);
                    command.Parameters.AddWithValue("@tabNumber", employee.TabNumber);
                    command.Parameters.AddWithValue("@lastName", employee.LastName);
                    command.Parameters.AddWithValue("@firstName", employee.FirstName);
                    command.Parameters.AddWithValue("@secondName", employee.SecondName);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task UpdateEmployeeAsync(int id, Employee employee)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "UPDATE employees SET Team = @team, TabNumber = @tabNumber, LastName = @lastName, FirstName = @firstName, SecondName = @secondName WHERE Id = @id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@team", employee.Team);
                    command.Parameters.AddWithValue("@tabNumber", employee.TabNumber);
                    command.Parameters.AddWithValue("@lastName", employee.LastName);
                    command.Parameters.AddWithValue("@firstName", employee.FirstName);
                    command.Parameters.AddWithValue("@secondName", employee.SecondName);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteEmployee(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = "DELETE FROM employees WHERE Id = @id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Team { get; set; }
        public int TabNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }

    }
}
