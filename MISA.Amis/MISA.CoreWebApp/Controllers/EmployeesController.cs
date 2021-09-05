using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Services;
using MISA.Core.Services;
using MySqlConnector;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseEntitysController<Employee>
    {
        #region Fields
        private readonly IEmployeeService _employeeService;
        private readonly Dictionary<string, string> _mappingColumnFileExcel;

        #endregion

        #region Constructors

        public EmployeesController(IEmployeeService employeeService, IBaseService<Employee> baseService) : base(baseService)
        {
            _employeeService = employeeService;
            _mappingColumnFileExcel = new Dictionary<string, string>();
            _mappingColumnFileExcel.Add("Mã nhân viên", "EmployeeCode");
            _mappingColumnFileExcel.Add("Tên nhân viên", "FullName");
            _mappingColumnFileExcel.Add("Giới tính", "GenderName");
            _mappingColumnFileExcel.Add("Ngày sinh", "DateOfBirth");
            _mappingColumnFileExcel.Add("Số CMND", "IdentityNumber");
            _mappingColumnFileExcel.Add("Chức danh", "PositionName");
            _mappingColumnFileExcel.Add("Tên đơn vị", "DepartmentName");
            _mappingColumnFileExcel.Add("Số tài khoản", "BankAccountNumber");
            _mappingColumnFileExcel.Add("Tên ngân hàng", "BankName");
            _mappingColumnFileExcel.Add("Chi nhành ngân hàng", "BankBranchName");
        }

        #endregion

        [HttpGet("employeeSearch")]
        public IActionResult GetEmployeesByCode([FromQuery] string employeeCode)
        {
            try
            {
                var serviceResult = _baseService.GetByColumn<string>(employeeCode, "EmployeeCode");
                return StatusCode(serviceResult.StatusCode, serviceResult.Data);
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = MISA.Core.Resources.Resources.MISAErrorMessage,
                    errorCode = "MISA_003",
                    traceId = Guid.NewGuid().ToString()
                };
                return StatusCode(500, response);
            }
        }

        [HttpGet("employeeFilter")]
        public IActionResult GetEmployeesFilter(int pageNumber, int pageSize, string employeeFilter)
        {
            try
            {
                var serviceResult = _employeeService.GetEmployeesFilter(pageNumber, pageSize, employeeFilter);
                return StatusCode(serviceResult.StatusCode, serviceResult.Data);
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = MISA.Core.Resources.Resources.MISAErrorMessage,
                    errorCode = "MISA_003",
                    traceId = Guid.NewGuid().ToString()
                };
                return StatusCode(500, response);
            }
        }

        [HttpGet("export")]
        public IActionResult ExportEmployees()
        {
            try
            {
                ServiceResult serviceResult = _baseService.ExportExcelFile(_mappingColumnFileExcel, "DANH SÁCH NHÂN VIÊN");
                string excelName = "Danh_sach_nhan_vien.xlsx";
                //using (var package = new ExcelPackage((Stream)serviceResult.Data))
                //{
                //    ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
                //    //workSheet.Cells["A1:K1"].Merge = true;
                //    //workSheet.Cells["A2:K2"].Merge = true;
                //    workSheet.Row(1).Height = 16;
                //    var row1 = workSheet.Row(1);
                //    row1.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //    row1.Style.Font.Bold = true;
                //    row1.Style.Font.Name = "Arial";
                //    row1.Style.Font.Size = 16;

                //}
                return File((Stream)serviceResult.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            catch (Exception e)
            {
                var response = new
                {
                    devMsg = e.Message,
                    userMsg = MISA.Core.Resources.Resources.MISAErrorMessage,
                    errorCode = "MISA_003",
                    traceId = Guid.NewGuid().ToString()
                };
                return StatusCode(500, response);
            }
        }

    }
}
