<template>
	<div id="employee-table" class="table-wrapper">
		<table>
			<thead class="table-header">
				<tr>
					<th style="width: 20px; padding: 0 !important"></th>
					<th style="width: 1.7em">
						<CheckBox v-model="checkAll" />
					</th>

					<th style="width: 10em">Mã nhân viên</th>
					<th style="width: 18em">Tên nhân viên</th>
					<th style="width: 7em">Giới tính</th>
					<th
						fieldName="DateOfBirth"
						style="width: 10em"
						class="text-align-center"
					>
						Ngày sinh
					</th>
					<th style="width: 10em">Số CMND</th>
					<th style="width: 12em">Chức Danh</th>
					<th style="width: 15em">Tên Đơn Vị</th>
					<th style="width: 10em">Số tài khoản</th>
					<th style="width: 18em">Tên Ngân Hàng</th>
					<th style="width: 15em">Chi nhánh ngân hàng</th>
					<th style="width: 9em">Chức năng</th>
					<th style="width: 20px; padding: 0 !important"></th>
				</tr>
			</thead>
			<tbody>
				<tr
					v-for="(employee, index) in employees"
					:id="employee.EmployeeId"
					:key="index"
					:class="{ checked: employee.IsChecked }"
					@dblclick="rowOnDblClick(employee.EmployeeId)"
				>
					<td></td>
					<td @dblclick.stop>
						<CheckBox
							:checked="employee.IsChecked"
							@change="checkboxOnChange(employee)"
						/>
					</td>
					<td :title="employee.EmployeeCode">{{ employee.EmployeeCode }}</td>
					<td :title="employee.FullName">{{ employee.FullName }}</td>
					<td>{{ employee.GenderName }}</td>
					<td class="text-align-center">
						{{ employee.DateOfBirth | dateFormat }}
					</td>
					<td :title="employee.IdentityNumber">
						{{ employee.IdentityNumber }}
					</td>
					<td :title="employee.PositionName">{{ employee.PositionName }}</td>
					<td :title="employee.DepartmentName">
						{{ employee.DepartmentName }}
					</td>
					<td :title="employee.BankAccountNumber">
						{{ employee.BankAccountNumber }}
					</td>
					<td :title="employee.BankName">{{ employee.BankName }}</td>
					<td :title="employee.BankBranchName">
						{{ employee.BankBranchName }}
					</td>
					<td @dblclick.stop>
						<div class="flex align-center">
							<div class="edit-button">Sửa</div>
							<div
								class="flex align-center ultilities-expander"
								@blur="hideUltilities"
								@click="showUltilities($event, employee)"
								tabindex="0"
							>
								<div class="mi mi-16 mi-arrow-down-blue"></div>
							</div>
						</div>
					</td>
					<td></td>
				</tr>
			</tbody>
		</table>
		<UltilitiesDropdown :ultilities="ultilities"/>
		<Paging
			v-show="totalRecord > 0"
			class="paging"
			nameRecord="bản ghi"
			:totalRecord="totalRecord"
			:totalPage="totalPage"
			:pageSize="pageSize"
			:pageNumber="pageNumber"
			@paging-select="getDataPaging"
		></Paging>
	</div>
</template>

<script>
// import Loader from "../loader/BaseLoader.vue";

import Paging from "../paging/BasePaging.vue";
import CheckBox from "../checkbox-custom/BaseCheckBox.vue";
import UltilitiesDropdown from "../dropdown/UltilitiesDropdown.vue";
export default {
	name: "EmployeeTable",
	components: { CheckBox, Paging, UltilitiesDropdown },

	props: {
		/** danh sách employees được truyền vào
		 */
		employees: {
			type: Array,
			required: true
		},

		/** Dùng để đánh số thứ tự
		 */
		pageSize: {
			type: Number,
			default: function() {
				return 20;
			},
			required: true
		},

		/** Dùng để đánh số thứ tự
		 */
		pageNumber: {
			type: Number,
			default: function() {
				return 1;
			},
			required: true
		},

		/**
		 * Số bản ghi
		 */
		totalRecord: {
			type: Number,
			default: function() {
				return 2000;
			},
			required: true
		},

		/**
		 * tổng số trang
		 */
		totalPage: {
			type: Number,
			default: function() {
				return 100;
			},
			required: true
		},

		loading: {
			type: Boolean,
			default() {
				return false;
			}
		},

		getDataPaging: {
			type: Function
		}
	},

	methods: {
		/** Hàm set sự kiên Click vào row
		 * CreatedBy: TTAnh(08/08/2021)
		 */
		checkboxOnChange(employee) {
			employee.IsChecked = !employee.IsChecked;
			var obj = {
				id: employee.EmployeeId,
				code: employee.EmployeeCode
			};

			if (employee.IsChecked) {
				this.selectedList.push(obj);
			} else {
				this.selectedList.forEach((item, index) => {
					if (obj.id == item.id) {
						this.selectedList.splice(index, 1);
					}
				});
			}
		},

		/** Hàm set sự kiện dblClick vào row
		 * CreatedBy: TTAnh(08/08/2021)
		 */
		rowOnDblClick(employeeId) {
			this.$emit("showDetail", employeeId);
		},

		/** Hàm emit thông tin của paging ra parent để lấy dữ liệu
		 * @param {Number} pageSize
		 * @param {Number} pageNumber
		 */
		pagingTable(pageSize, pageNumber) {
			this.$emit("paging-select", pageSize, pageNumber);
		},

		showUltilities(event, employee) {
			// e = Mouse click event.
			event.stopPropagation();
			var rect = event.target.getBoundingClientRect();
			var x = event.clientX - rect.left; //x position within the element.
			var y = event.clientY - rect.top; //y position within the element.
			console.log(
				"Left? : " + event.clientX + " ; right? : " + rect.right + "."
			);
			this.$eventBus.$emit("ultilities-dropdown-show", {
				top: rect.bottom + 5,
				left: rect.left - 90,
				affectObject: employee
			});
		},

		hideUltilities() {
			this.$eventBus.$emit("ultilities-dropdown-hide");
		},

		ultilityDeleteClick(employee){
			
		},

		deleteEmployee: function(employee) {
			this.axios
				.delete(
					"https://localhost:44348/api/v1/Employees/" + employee.EmployeeId
				)
				.then(response => {
					if (response.status == 200) {
						this.$eventBus.$emit("ToastMessage", {
							type: "success",
							content: `Xóa thành công bản ghi ${employee.FullName}`,
							duration: 5000
						});
					} else if (response.status == 204) {
						console.log(`not found ${employee.FullName}, maybe it is deleted`);
					}
				})
				.catch(error => {
					if (error.response.status == 400 || error.response.status == 500) {
						this.$eventBus.$emit("ToastMessage", {
							type: "error",
							content: `Xóa không thành công bản ghi ${employee.FullName}: ${error.response.userMsg}`,
							duration: 5000
						});
					}
				});
		},

		dupplicateEmployee: function(employee){
			console.log(employee);
		},

		disableEmployee: function(employee){
			console.log(employee);
		}
	},

	data() {
		return {
			/** danh sách cac hàng được chọn
			 */
			selectedList: [],

			ultilities: [
				{
					content: "Xóa",
					function: this.deleteEmployee
				},
				{
					content: "Nhân bản",
					function: this.dupplicateEmployee
				},
				{
					content: "Ngưng sử dụng",
					function: this.disableEmployee
				}
			],
			employeeSelected: null,
		};
	},

	computed: {
		/** Chọn tất cả các select-box trên bảng
		 * CreatedBy: TTAnh(02/09/2021)
		 */
		checkAll: {
			get: function() {
				var checkAll = true;
				if (this.employees.length == 0) checkAll = false;
				this.employees.forEach(e => {
					if (!e.IsChecked) {
						checkAll = false;
					}
				});
				return checkAll;
			},
			set: function(checked) {
				if (checked) {
					this.employees.forEach(employee => {
						if (!employee.IsChecked) {
							employee.IsChecked = true;
							var obj = {
								id: employee.EmployeeId,
								code: employee.EmployeeCode
							};
							this.selectedList.push(obj);
						}
					});
				} else {
					this.employees.forEach(employee => {
						employee.IsChecked = false;
						this.selectedList.forEach((item, index) => {
							if (employee.EmployeeId == item.id) {
								this.selectedList.splice(index, 1);
							}
						});
					});
				}
			}
		}
	},
	watch: {
		/** Hàm quan sát sự thay đổi của prop employees,
		 * khi danh sách employees thay đổi thì cần check xem có phần tử nào
		 * trong mảng này đã được chọn trước đó hay không
		 * CreatedBy: TTAnh(08/08/2021)
		 */
		employees: function(list) {
			for (var checked of this.selectedList) {
				for (var e of list) {
					if (checked.id == e.EmployeeId) {
						e.IsChecked = true;
						break;
					}
				}
			}
		},
		selectedList: function(list) {
			this.$emit("changeSelectedList", list);
		}
	}
};
</script>

<style scoped>
.table-wrapper {
	position: relative;
	font-size: 13px;
}

.table-header {
	top: 0;
	z-index: 10;
	position: sticky;
	height: 34px;
	/* border-bottom: 1px solid #c7c7c7; */
}

.table-header th {
	text-transform: uppercase;
	font-size: 12px;
	border-right: 1px solid #c7c7c7;
	background-color: #eceef1;
}

.table-wrapper table {
	/* padding: 0 20px; */
	background-color: #fff;
	width: 100%;
	table-layout: fixed;
	border-collapse: separate;
	text-indent: initial;
	border-spacing: 0px;
}

.table-wrapper table th,
td {
	border-bottom: 1px solid #c7c7c7;
	padding: 0 10px;
	/* box-sizing: border-box; */
	overflow-x: hidden;
	text-overflow: ellipsis;
	text-align: left;
	white-space: nowrap;
}

.table-wrapper table tbody tr {
	height: 47px;
}

.table-wrapper table tbody td {
	border-right: 1px dotted #c7c7c7;
	background-color: #ffffff;
}

table tr > :first-child {
	position: sticky;
	left: 0px;
	background-color: #ffffff !important;
	border: none !important;
}

table tr > :last-child {
	position: sticky;
	right: 0;
	border: none !important;
	background-color: #ffffff !important;
}

table tbody tr:hover td {
	background-color: #f3f8f8;
}

table tr td:nth-child(2),
table tr th:nth-child(2) {
	position: sticky;
	left: 20px;
}

table tbody tr td:nth-last-child(2) {
	border-left: 1px dotted #c7c7c7;
	border-right: none;
	position: sticky;
	right: 20px;
	text-align: center;
}

table thead tr th:nth-last-child(2) {
	border-right: none !important;
	border-left: 1px solid #c7c7c7 !important;
	position: sticky;
	right: 20px;
	text-align: center;
}

.table-wrapper table tr:hover {
	background-color: #f3f8f8;
}

.table-wrapper table tr.checked {
	background-color: #e3f3ee;
}

.table-wrapper table .row-selected {
	background: #e3f3ee;
}

.table-wrapper .text-align-center {
	text-align: center;
}

.table-wrapper .text-align-right {
	text-align: right;
	padding-left: 0px !important;
}

.table-wrapper .text-align-left {
	text-align: left;
}
.table-wrapper .modal-loader {
	z-index: 50;
	position: absolute;
	top: 0;
	height: 100%;
	width: 100%;
}

.paging {
	z-index: 30;
	position: sticky;
	bottom: 0px;
	left: 0px;
	right: 0px;
	width: 100%;
	height: 46px;
}

.table-wrapper .modal-loader {
	z-index: 50;
	position: absolute;
	top: 223px;
	left: 40px;
	height: 100%;
	width: 100%;
}

.ultilities-expander-wrapper {
	padding: 8px 10px;
}

.ultilities-expander {
	margin-left: 8px;
	width: 26px;
	height: 16px;
	box-sizing: border-box;
}

.ultilities-expander .mi-arrow-down-blue {
	background-position-x: -892px;
	width: 24px !important;
}
.ultilities-expander:focus {
	border: 1px solid #0075c0;
}

/* custom-scrollbar */

::-webkit-scrollbar {
	width: 10px;
	height: 10px;
}
::-webkit-scrollbar:hover {
	width: 10px;
	height: 10px;
}

/* Track */

::-webkit-scrollbar-track {
	background: #fff;
}

/* Handle */

::-webkit-scrollbar-thumb {
	background: #b8bcc3;
	/* border-radius: 0.4em; */
}

/* Handle on hover */

::-webkit-scrollbar-thumb:hover {
	background: #888888;
}
</style>
