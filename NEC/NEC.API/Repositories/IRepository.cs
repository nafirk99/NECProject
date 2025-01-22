using NEC.API.Models;

namespace NEC.API.Repositories
{
    public interface IRepository
    {
        public Task<List<Employee>> GetAllAsync();
        public Task<Employee> GetByIdAsync(int id);
        //public Task<bool> CreateAsync(Employee employee);
        public Task<bool> CreateAsync(CreateDTO createDTO);

        //public Task<bool> UpdateAsync(Employee employee);
        public Task<bool> UpdateAsync(UpdateDTO updateDTO);
        public Task<bool> DeleteAsync(int id);
    }
}
