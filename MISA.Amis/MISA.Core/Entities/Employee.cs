using MISA.Core.Enum;
using MISA.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Employee : Person
    {

        #region Property
        [MISAPrimaryKey]
        [MISANotUpdate]
        public Guid EmployeeId { get; set; }

        [MISARequired("Mã nhân viên")]
        [MISAUnique("Mã nhân viên")]
        public string EmployeeCode { get; set; }

        [MISARequired("Đơn vị")]
        public Guid? DepartmentId { get; set; }
        [MISANotMap]
        public string DepartmentCode { get; set; }
        [MISANotMap]
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }

        public string IdentityNumber { get; set; }
        public DateTime? IdentityDate { get; set; }
        public string IdentityPlace { get; set; }
       
        [MISAValidate("Email", "Email")]
        public string Email { get; set; }

        public string MobilephoneNumber { get; set; }

        public string TelephoneNumber { get; set; }

        public string BankAccountNumber { get; set; }

        public string BankName { get; set; }

        public string BankBranchName { get; set; }

        public string BankProvinceName { get; set; }
        #endregion

        #region Contructor
        public Employee() : base() { }
        #endregion
    }
}
