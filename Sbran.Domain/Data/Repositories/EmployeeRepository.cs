using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities;
using Sbran.Domain.Models;
using Sbran.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories
{
	/// <summary>
	/// Репозиторий сотрудников
	/// </summary>
	public sealed class EmployeeRepository : IEmployeeRepository
    {
        private readonly DomainContext _domainContext;

        public EmployeeRepository(DomainContext databaseContext)
        {
            _domainContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        /// <summary>
        /// Получить всех сотрудников
        /// </summary>
        /// <returns><Сотрудники</returns>
        public Task<List<Employee>> GetAllAsync()
        {
            return _domainContext.Set<Employee>().ToListAsync();
        }

        /// <summary>
        /// Получить сотрудника по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор иностранца</param>
        /// <returns>Сотрудник</returns>
        public async Task<Employee> GetAsync(Guid id)
        {
            var employee = await _domainContext.Employees.Include<Employee, List<Invitation>>(test => test.Invitations).FirstOrDefaultAsync(empl => empl.Id == id);

            if (employee == null)
            {
                throw new Exception($"Сотрудник для {id} не найден");
            }

            return employee;
        }

        /// <summary>
        /// Получить сотрудника по идентификатору
        /// </summary>
        /// <param name="userId">Идентификатор иностранца</param>
        /// <returns>Сотрудник</returns>
        public async Task<Employee> GetByUserIdAsync(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentException(nameof(userId));
            }

            var employee = await _domainContext.Set<Employee>().FirstOrDefaultAsync(empl => empl.UserId == userId);

            if (employee == null)
            {
                throw new Exception($"Сущность не найдена для user id: {userId}");
            }

            return employee;
        }

        /// <summary>
        /// Создать сотрудника
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Идентификатор иностранца</returns>
        public Employee Create(Guid userId)
        {
            var createdEmployee = new Employee(userId);
            
            _domainContext.Set<Employee>().Add(createdEmployee);

            return createdEmployee;
        }

        /// <summary>
        /// Обновить данные по сотруднику
        /// </summary>
        /// <param name="employeeId">Идентификатор иностранца</param>
        /// <param name="scientificInfoDto">Данные по сотруднику</param>
        public async Task UpdateScientificInfoAsync(Guid employeeId, ScientificInfoDto scientificInfoDto)
        {
            Contract.Argument.IsNotNull(scientificInfoDto, nameof(scientificInfoDto));

            var updatedEmployee = await GetAsync(employeeId);

            updatedEmployee.SetAcademicDegree(scientificInfoDto.AcademicDegree);
            updatedEmployee.SetAcademicRank(scientificInfoDto.AcademicRank);
            updatedEmployee.SetEducation(scientificInfoDto.Education);
        }

        /// <summary>
        /// Обновить данные по сотруднику
        /// </summary>
        /// <param name="employeeId">Идентификатор иностранца</param>
        /// <param name="jobDto">Данные по сотруднику</param>
        public async Task UpdateJobAsync(Guid employeeId, JobDto jobDto)
        {
            Contract.Argument.IsNotNull(jobDto, nameof(jobDto));

            var updatedEmployee = await GetAsync(employeeId);

            updatedEmployee.SetWorkPlace(jobDto.WorkPlace);
            updatedEmployee.SetPosition(jobDto.Position);
        }

        /// <summary>
        /// Удалить сотрудника по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор иностранца</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            Contract.Argument.IsNotEmptyGuid(id, nameof(id));

            var deletedEmployee = await GetAsync(id);

            _domainContext.Set<Employee>().Remove(deletedEmployee);
        }
    }
}