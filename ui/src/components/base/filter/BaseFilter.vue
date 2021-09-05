/* eslint-disable vue/no-unused-components */

<template>
	<div>
		<div></div>
		<div class="filter-bar">
			<div class="input-icon-after">
				<input
					tabindex="0"
					class="text-box-default"
					type="text"
					v-bind="$attrs"
					:value="filter.EmployeeFilter"
					@input="inputDebounce"
				/>
				<div class="mi mi-16 mi-search"></div>
			</div>
			<div class="ultility-buttons">
				<div class="mi mi-24 mi-refresh" @click="btnRefreshClick" data-title="Lấy lại dữ liệu"></div>
				<div class="mi mi-24 mi-excel" @click="downloadExcelFile" data-title="Xuất ra Excel"></div>
				<div class="mi mi-24 mi-setting-list"></div>
			</div>
			<!-- v-model="filter['EmployeeFilter']" -->
			<!-- <SelectBox
				v-for="(data, index) in selectBoxesData"
				style="40px"
				:selectBoxData="data"
				:key="index"
				:idField="data[1]"
				:nameField="data[2]"
				:initContent="data[0]"
				v-model="filter[data[1]]"
			/> -->
			<!-- <ComboBox
				tabindex="1"
				v-for="(data, index) in selectBoxesData"
				style="40px"
				:comboBoxData="data"
				:key="index"
				:idField="data[1]"
				:nameField="data[2]"
				:initContent="data[0]"
				v-model="filter[data[1]]"
			/> -->
		</div>
	</div>
</template>

<script>
import ComboBox from "../combobox/BaseComboBox.vue";
import SelectBox from "../select-custom/BaseSelectBox.vue";
export default {
	name: "FilterTable",
	components: {},
	props: {
		/** dữ liệu của các selectbox */
		selectBoxesData: {
			type: Array
		},
		/**	placeholder của input search */
		inputPlaceHolder: {
			type: String,
			default: function() {
				return "Tìm kiếm theo Mã, Tên hoặc Số điện thoại";
			}
		},
		/**	dạng của filterObject */
		filterObj: {
			type: Object,
			default: function() {
				return {
					EmployeeFilter: "n",
					DepartmentId: null,
					PositionId: null
				};
			},
			required: true
		}
	},
	data() {
		return {
			filter: this.filterObj,
			debounce: null
		};
	},
	computed: {
		/** xử lý bất đồng bộ */
		computedSelectBoxesData() {
			if (this.selectBoxesData.length < 2) return [];
			else return this.selectBoxesData;
		}
	},
	methods: {
		btnRefreshClick: function() {
			this.$emit("refreshBtn", this.filter);
		},

		sendEmit() {
			return this.$emit("filter-choose", this.filter);
		},

		inputDebounce(event) {
			var vm = this;
			var timer = 500;
			clearTimeout(this.debounce);
			this.debounce = setTimeout(function() {
				vm.filter.EmployeeFilter = event.target.value;
			}, timer);
		},

		focusFilter() {
			console.log("tab");
		},

		downloadExcelFile() {
			this.axios({
				url: "https://localhost:44348/api/v1/Employees/export",
				method: "GET",
				responseType: "blob"
			}).then(response => {
				var fileURL = window.URL.createObjectURL(new Blob([response.data]));
				var fileLink = document.createElement("a");
				fileLink.href = fileURL;
				fileLink.setAttribute("download", "Danh_sach_nhan_vien.xlsx");
				document.body.appendChild(fileLink);
				fileLink.click();
			});
		}
	},
	watch: {
		filter: {
			handler: function() {
				this.sendEmit();
			},
			deep: true
		}
	}
};
</script>

<style scope>
.filter .filter-bar {
	flex: 0.7;
	display: flex;
	justify-content: flex-end;
	align-items: center;
	flex-wrap: nowrap;
}

.filter .ultility-buttons {
	display: flex;
	justify-content: space-evenly;
	width: 100px;
	margin-right: 20px;
}

.filter-bar .input-icon-after {
	position: relative;
}

.input-icon-after .mi-search {
	position: absolute;
	top: 8px;
	right: 5px;
	margin-right: 4px;
}
.filter .filter-bar input {
	width: 240px;
	font-size: 13px;
	padding-right: 1.75em;
	padding-left: 0.85em;
	outline: none;
	box-sizing: border-box;
}

.filter-bar input::placeholder {
	font-size: 11.5px;
	font-style: italic;
}

.filter-bar input:focus {
	border: 1px solid #019160;
}

.filter .filter-bar .select-box,
.filter .filter-bar .combo-box {
	flex: 2;
	min-width: 200px;
	margin: 0px 10px 0px 10px;
}

.filter .refresh-btn {
	width: 40px;
	min-width: 40px;
	background-color: var(--object-color);
	background-image: url(../../../assets/icon/refresh.png);
	background-repeat: no-repeat;
	background-position: center;
	cursor: pointer;
}

.refresh-btn:hover {
	background-color: #e5e5e5;
}
</style>
 