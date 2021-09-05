using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Interfaces.Services
{
    public interface IEmployeeService: IBaseService<Employee>
    {
        /// <summary>
        /// Lấy danh sách nhân viên phân trang và filter
        /// </summary>
        /// <param name="pageNumber">số thứ tự của trang</param>
        /// <param name="pageSize">kích thước của 1 trang</param>
        /// <param name="employeeFilter">chuối filter</param>
        /// <param name="departmentId">filter theo phòng ban</param>
        /// <param name="positionId">filter theo vị trí</param>
        /// <returns></returns>
        ServiceResult GetEmployeesFilter(int pageNumber, int pageSize, string employeeFilter);


        /// <summary>
        /// Xuất khẩu dữ liệu danh sách nhân viên
        /// </summary>
        /// <returns></returns>
        ServiceResult ExportEmployees(Dictionary<string,string> mappingColumnFileExcel);
    }
}
