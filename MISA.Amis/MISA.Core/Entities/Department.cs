using MISA.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Department : BaseEntity
    {
        [MISAPrimaryKey]
        public Guid DepartmentId { get; set; }

        [MISAUnique("Mã phòng ban")]
        public string DepartmentCode { get; set; }
        [MISARequired("Tên phòng ban")]
        public string DepartmentName { get; set; }
        public string Description { get; set; }
    }
}
