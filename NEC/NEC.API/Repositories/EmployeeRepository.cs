using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Data;
using NEC.API.Models;

namespace NEC.API.Repositories
{
    public class EmployeeRepository : IRepository
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;

        public static IConfiguration Configuration { get; set; }

        /**
        * Retrieves Connection String from Configuration
        * This function retrieves the connection string from the 
        * application's configuration settings, typically stored in 
        * appsettings.json. 
        * 
        * @author: Nafiz Imtiaz Khan
        * @since: 20/1/2025
        */
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory
                ()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            return Configuration.GetConnectionString("NZWalksConnectionString");
        }

        /**
         * Inserts a New Employee into the Database
         * 
         * This function inserts a new employee record into the database using ADO.NET 
         * and a stored procedure named `[DBO].[usp_Insert_Employee]`. 
         * The function sets the `[id_employee_ver]` parameter to 0 and automatically 
         * populates the `[dtt_created]` field using the `GETDATE()` function within 
         * the stored procedure.
         * 
         * @author: Nafiz Imtiaz Khan
         * @since: 20/1/2025
         */
        public async Task<bool> CreateAsync(Employee employee)
        {
            int id = 0;
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync(); // Open the connection asynchronously

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[DBO].[usp_Insert_Employee]";
                    command.Parameters.AddWithValue("@Name", employee.tx_name);
                    command.Parameters.AddWithValue("@Age", employee.id_age);
                    command.Parameters.AddWithValue("@Date_Of_Birth", employee.dt_date_of_birth);
                    command.Parameters.AddWithValue("@Salary", employee.dec_salary);
                    command.Parameters.AddWithValue("@Join_Date", employee.dt_join_date);
                    command.Parameters.AddWithValue("@Email", employee.tx_email);
                    command.Parameters.AddWithValue("@Phone_No", employee.tx_phone_no);

                    id = await command.ExecuteNonQueryAsync(); // ExecuteNonQuery asynchronously
                }
            }

            return id == 0 ? false : true;
        }

        /**
         * Deletes an Employee Record from the Database
         * 
         * This function deletes an employee record from the database 
         * using the provided `Id` parameter and the stored procedure 
         * `[DBO].[usp_Delete_Employee]`.
         * 
         * @author: Nafiz Imtiaz Khan
         * @since: 20/1/2025
         */
        public async Task<bool> DeleteAsync(int id)
        {
            int deletedRowCount = 0;
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync(); // Open the connection asynchronously

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[usp_Delete_Employee]";
                    command.Parameters.AddWithValue("@Id", id);
                    deletedRowCount = await command.ExecuteNonQueryAsync(); // ExecuteNonQueryAsync is asynchronous
                }
            }
            return deletedRowCount == 0 ? false : true;
        }

        /**
        * Retrieves a List of All Employees from the Database
        * 
        * This function retrieves a complete list of all employees from the database 
        * by executing the stored procedure `[DBO].[usp_Get_Employees]`. 
        * The retrieved data is returned as a `List<Employee>`.
        * 
        * @author: Nafiz Imtiaz Khan
        * @since: 20/1/2025
        */
        public async Task<List<Employee>> GetAllAsync()
        {
            List<Employee> employeeList = new List<Employee>();
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync(); // Open the connection asynchronously

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[DBO].[usp_Get_Employees]";
                    using (var reader = await command.ExecuteReaderAsync()) // ExecuteReaderAsync is asynchronous
                    {
                        while (await reader.ReadAsync()) // ReadAsync is asynchronous
                        {
                            Employee employee = new Employee();
                            employee.id_employee_key = Convert.ToInt32(reader["id_employee_key"]);
                            employee.dtt_created = Convert.ToDateTime(reader["dtt_created"]);
                            employee.dtt_updated = reader["dtt_updated"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["dtt_updated"]);
                            employee.tx_name = reader["tx_name"].ToString();
                            employee.id_age = Convert.ToInt32(reader["id_age"]);
                            employee.dt_date_of_birth = Convert.ToDateTime(reader["dt_date_of_birth"]);
                            employee.dec_salary = Convert.ToDecimal(reader["dec_salary"]);
                            employee.dt_join_date = Convert.ToDateTime(reader["dt_join_date"]);
                            employee.tx_email = reader["tx_email"].ToString();
                            employee.tx_phone_no = reader["tx_phone_no"].ToString();
                            employeeList.Add(employee);
                        }
                    }
                }
            }
            return employeeList;
        }

        /**
        * Retrieves an Employee Record by ID
        * 
        * This function retrieves a single employee record from the database 
        * based on the provided `Id` parameter. 
        * It utilizes the stored procedure `[DBO].[usp_Get_EmployeeById]` to 
        * retrieve the employee data.
        * 
        * @author: Nafiz Imtiaz Khan
        * @since: 20/1/2025
        */
        public async Task<Employee> GetByIdAsync(int id)
        {
            Employee employee = null;
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                await connection.OpenAsync(); // Open the connection asynchronously

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[DBO].[usp_Get_EmployeeById]";
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = await command.ExecuteReaderAsync()) // ExecuteReaderAsync is asynchronous
                    {
                        if (await reader.ReadAsync()) // ReadAsync is asynchronous
                        {
                            employee = new Employee();
                            employee.id_employee_key = Convert.ToInt32(reader["Id"]);
                            employee.dtt_created = Convert.ToDateTime(reader["CreatedAt"]);
                            employee.dtt_updated = reader["UpdatedAt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["UpdatedAt"]);
                            employee.tx_name = reader["Name"].ToString();
                            employee.id_age = Convert.ToInt32(reader["Age"]);
                            employee.dt_date_of_birth = Convert.ToDateTime(reader["Date_Of_Birth"]);
                            employee.dec_salary = Convert.ToDecimal(reader["Salary"]);
                            employee.dt_join_date = Convert.ToDateTime(reader["Join_Date"]);
                            employee.tx_email = reader["Email"].ToString();
                            employee.tx_phone_no = reader["Phone_No"].ToString();
                        }
                    }
                }
            }
            return employee;
        }

        /**
        * Updates an Existing Employee Record
        * 
        * This function updates an existing employee record in the database. 
        * It takes an `Employee` object as input and utilizes the stored procedure 
        * `[dbo].[usp_Update_Employee]` to perform the update. 
        * The stored procedure increments the `[id_employee_ver]` field by 1 
        * and updates the `[dtt_updated]` field with the current date and time 
        * using the `GETDATE()` function.
        * 
        * @author: Nafiz Imtiaz Khan
        * @since: 20/1/2025
        */
        public async Task<bool> UpdateAsync(Employee employee)
        {
            Employee existingEmployee = await GetByIdAsync(employee.id_employee_key);
            int rowsAffected = 0;

            if (existingEmployee != null)
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    await connection.OpenAsync(); // Open the connection asynchronously

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "[dbo].[usp_Update_Employee]";
                        command.Parameters.AddWithValue("@Id", employee.id_employee_key);
                        command.Parameters.AddWithValue("@Name", employee.tx_name);
                        command.Parameters.AddWithValue("@Age", employee.id_age);
                        command.Parameters.AddWithValue("@Date_Of_Birth", employee.dt_date_of_birth);
                        command.Parameters.AddWithValue("@Salary", employee.dec_salary);
                        command.Parameters.AddWithValue("@Join_Date", employee.dt_join_date);
                        command.Parameters.AddWithValue("@Email", employee.tx_email);
                        command.Parameters.AddWithValue("@Phone_No", employee.tx_phone_no);

                        rowsAffected = await command.ExecuteNonQueryAsync(); // ExecuteNonQueryAsync is asynchronous
                    }
                }
            }
            return rowsAffected == 0 ? false : true;  // Return true if at least one row was affected
        }
    }
}
