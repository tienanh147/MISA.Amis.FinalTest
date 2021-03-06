using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.Core.MISAAttribute;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MISA.Infrastructure.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Contructor
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion


        public Object GetEmployeesFilter(int pageOffset, int pageSize, string employeeFilter)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PageOffset", pageOffset);
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@EmployeeFilter", employeeFilter);
            parameters.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            IEnumerable<Employee> employees;
            using (IDbConnection dbconnection = new MySqlConnection(_connectionString))
            {
                employees = dbconnection.Query<Employee>("Proc_GetEmployeesFilterPaging",param: parameters, commandType: CommandType.StoredProcedure);
            }
            if(parameters.Get<int>("@TotalRecord") == 0)
            {
                return null;
            }

            var filterEmployees = new
            {
                TotalPage = parameters.Get<int>("@TotalPage"),
                TotalRecord = parameters.Get<int>("@TotalRecord"),
                Data = employees
            };
            return filterEmployees;
            throw new NotImplementedException();
        }
    
        
    }
}
