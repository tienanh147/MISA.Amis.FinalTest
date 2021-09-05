<template>
	<div
		@mouseenter="showWarning = true"
		@mouseleave="showWarning = false"
		tabindex="1"
		class="combo-box"
		:class="{
			'combo-box-show': dropdownShow,
			'combo-box-error': valueNotFound || (required && warning),
		}"
	>
		<div
			v-if="!this.required"
			class="icon-default icon-x"
			@click="iconXClick"
			v-show="selectedId != null"
		></div>
		<input
			:v-bind="$attrs"
			:placeholder="initContent"
			@keydown.enter="enterInput"
			@focus="focusInput"
			@blur="blurInput"
			@input="typing"
			v-model="inputValue"
			type="text"
			class="combo-box-input"
		/>
		<div class="warning" v-if="computedWarning && showWarning">
			{{ computedWarning }}
		</div>
		<div class="icon-arrow-wrapper" @click="dropdownClick">
			<div class="mi mi-16 mi-arrow-dropdown"></div>
		</div>

		<div
			v-if="dropdownShow"
			class="select-items"
			:class="{
				'select-hide': filterData.length == 0 && showMode != 1,
				'select-items-bottom': dropup,
			}"
		>
			<table>
				<thead>
					<tr class="option-item-titles">
						<th class="item-title">
							{{ mapTitle.get(codeField) }}
						</th>
						<th class="item-title">
							{{ mapTitle.get(nameField) }}
						</th>
					</tr>
				</thead>
				<tbody>
					<tr
						class="option-item"
						v-for="item in displayData"
						:key="item[idField]"
						@click="itemClick(item[idField], item[nameField])"
						:class="{ selected: item[idField] == selectedId }"
					>
						<td class="item-value">{{ item[codeField] }}</td>
						<td class="item-value">{{ item[nameField] }}</td>
					</tr>
				</tbody>
			</table>
		</div>
	</div>
</template>

<script>
export default {
	model: { prop: "selectedId", event: "combobox-select" },
	name: "BaseComboBox",
	props: {
		/** Dữ liệu của comboBox ****
		 * Bao gồm 3 phần:
		 * 1. initContent: nội dung mặc định
		 * 2. idField: tên của trường Id trong object lựa chọn
		 * 3. nameField: tên của trường Name trong object lựa chọn
		 * 4. dropdown data: 1 mảng gồm các lựa chọn,
		 * mỗi lựa chọn là 1 object có 2 trường bắt buộc là Id và Name
		 */
		comboBoxData: {
			type: Array
		},

		/**
		 * placeHolder
		 */
		initContent: {
			type: String
		},
		/**
		 * trường id trong option của comboBoxData
		 */
		idField: {
			type: String,
			default() {
				return "DepartmentId";
			}
		},

		/**
		 * trường nội dung trong option của comboBoxData
		 */
		nameField: {
			type: String,
			default() {
				return "DepartmentName";
			}
		},
		codeField: {
			type: String,
			default() {
				return "DepartmentCode";
			}
		},
		mapTitle: {
			type: Map
		},
		/**
		 * id đã được lựa chọn
		 */
		selectedId: {
			type: [String, Number],
			default() {
				return null;
			}
		},

		dropup: {
			type: Boolean,
			default() {
				return false;
			}
		},
		required: {
			type: Boolean,
			default() {
				return false;
			}
		},
		label:{
			type: String,
		}
	},
	data: function() {
		return {
			/**
			 * hiển thị dropdown hay không
			 * --true: Hiển thị
			 * --false: Ẩn
			 */
			dropdownShow: false,
			inputValue: null,
			valueNotFound: false,
			showMode: 1,
			showWarning: false,
			warning: null
		};
	},
	mounted() {
		/** Giữ nguyên dữ liệu khi chọn chưa đúng*/
		this.inputValue = this.computedInputValue;
	},
	methods: {
		/** Set sự kiện input */
		typing: function() {
			this.dropdownShow = true;
			this.showMode = 2;
			this.valueNotFound = false;
		},
		/**
		 * Sự kiện focus vào ô input
		 * CreatedBy: TTAnh (20/08/2021)
		 * @param {event} event
		 */
		focusInput() {
			this.dropdownShow = true;
			// this.valueNotFound = false;
			this.showMode = 1;
		},

		/**
		 * Sự kiện blur ra ngoài ô input
		 * CreatedBy: TTAnh (20/08/2021)
		 * @param {event} event
		 */
		blurInput(event) {
			if (event.relatedTarget !== null) {
				if (event.target.parentElement !== event.relatedTarget) {
					this.dropdownShow = false;
					//Nếu đã chọn thì khi blur sẽ giữ nguyên lựa chọn
					if (this.selectedId != null) {
						this.inputValue = this.computedInputValue;
					}
				}
			} else {
				this.dropdownShow = false;
				//Giữ nguyên lựa chọn
				this.inputValue = this.computedInputValue;
			}
			//Nếu thông tin này là bắt buộc
			if (this.required) {
				//Nếu giá trị nhập vào là rỗng hoặc null thì hiển thị cảnh báo
				if (this.inputValue == "" || this.inputValue == null) {
					this.computedWarning = `${this.label} không được bỏ trống.`;
				}
				//Nếu chưa chọn thì set valueNotFound = true để hiển thị cảnh báo
				else if (this.selectedId == null) {
					this.valueNotFound = true;
				} else {
					this.valueNotFound = false;
					this.computedWarning = null;
				}
			} else {
				// this.valueNotFound = false;
			}
		},
		/**
		 * Sự kiện nhấn enter ở input combobox
		 * CreatedBy: TTAnh (20/08/2021)
		 */
		enterInput() {
			var valueFound = false;
			this.filterData.forEach(item => {
				var itemValue = item[this.nameField].toLowerCase();
				var inputValue;
				if (this.inputValue == null || this.inputValue == "") {
					if (!this.required) valueFound = true;
					this.inputValue = null;
					this.sendEmit(null, "");
				} else {
					inputValue = this.inputValue.toLowerCase();
				}
				if (itemValue.includes(inputValue)) {
					this.sendEmit(item[this.idField], item[this.nameField]);
					valueFound = true;
				}
			});
			if (valueFound == false) this.valueNotFound = true;
		},
		/**
		 * set sự kiện cho nút xóa lựa chọn.
		 * CreatedBy: TTAnh(21/08/2021)
		 */
		iconXClick() {
			this.dropdownShow = false;
			this.inputValue = null;
			this.sendEmit(null);
		},
		/**
		 * set sự kiện ẩn hiện dropdown.
		 * CreatedBy: TTAnh(21/08/2021)
		 */
		dropdownClick() {
			this.dropdownShow = !this.dropdownShow;
			this.$el.querySelector("input").focus();
			this.showMode = 1;
		},
		/**
		 * hàm set sự kiện click vào 1 option
		 * @param {String} id id của option được click
		 * @param {String} name nội dung của option được click
		 * CreatedBy: TTAnh(21/08/2021)
		 */
		itemClick(id, name) {
			this.dropdownShow = false;
			this.valueNotFound = false;
			this.computedWarning = null;
			this.sendEmit(id, name);
		},

		/**
		 * Hàm emit sự kiện
		 * @param {String} id id của option được chọn
		 */
		sendEmit(id, name) {
			this.inputValue = name;
			return this.$emit("combobox-select", id);
		}
	},
	computed: {
		/** Dữ liệu đã được lọc theo input */
		filterData: {
			get: function() {
				if (this.inputValue == "" || this.inputValue == null) {
					return this.dropdownData;
				} else {
					var filterData = [];

					this.dropdownData.forEach(item => {
						var itemValue = item[this.nameField].toLowerCase();
						var inputValue = this.inputValue.toLowerCase();
						if (itemValue.includes(inputValue)) {
							filterData.push(item);
						}
					});
					return filterData;
				}
			}
		},
		/**
		 * do dữ liệu truyền vào bao gồm cả initContent, idField, nameField
		 * nên cần cắt đi 3 phần tử đầu của mảng.
		 * CreatedBy: TTAnh(21/08/2021)
		 */
		dropdownData: function() {
			return this.comboBoxData.slice(5);
		},

		/** Dữ liệu hiển thị lên cho người dùng */
		displayData: function() {
			if (this.showMode == 1) {
				return this.dropdownData;
			} else {
				return this.filterData;
			}
		},
		/**
		 * tìm ra nội dung của option với id là selectedId.
		 * CreatedBy: TTAnh(21/08/2021)
		 */
		computedInputValue: {
			get: function() {
				var name = "";
				this.dropdownData.forEach(item => {
					if (this.selectedId == item[this.idField]) {
						name = item[this.nameField];
					}
				});
				return name;
			}
		},
		computedWarning: {
			get: function() {
				if (this.dropdownShow) {
					return null;
				}
				if (this.valueNotFound) {
					if (this.inputValue == "" && this.required) {
						return `${this.label} không được bỏ trống.`;
					}
					return `Không tìm thấy trong danh mục.`;
				}
				return this.warning;
			},
			set: function(value) {
				this.warning = value;
			}
		}
	}
};
</script>

<style scoped>
.combo-box {
	outline: none;
	/* font-size: 13px; */
	height: 40px;
	width: 100%;
	background-color: #fff;
	/* top: 30%;
	left: 50%;
	transform: translate(-50%, -50%); */
	border-radius: 2px;
	border: 1px solid #bbb;
	display: flex;
	align-items: center;
	justify-content: space-between;
	position: relative;
	z-index: 2;
	box-sizing: border-box;
}

.combo-box.combo-box-error {
	border: 1.6px solid red !important;
}
.combo-box.combo-box-show .select-items {
	display: block;
}
.combo-box.combo-box-show .icon-chevron {
	transform: rotate(180deg);
	transition: all 0.3s ease;
}

.combo-box.combo-box-show .icon-arrow-wrapper {
	/* border: none; */
	background-color: #e0e0e0;
}

.combo-box.combo-box-show {
	border: 1px solid #2ca01c;
}

/* 1. combo-box INPUT */
.combo-box-input {
	outline: none;
	width: calc(100% - 60px);
	border: 0;
	border-radius: 4px;
	outline: none;
	font-size: 13px;
	padding: 0 10px;
	background-color: #fff;
}

.combo-box .warning {
	display: flex;
	align-items: center;
	justify-content: center;
	position: absolute;
	bottom: -10px;
	left: calc(5%);
	height: 20px;
	width: fit-content;
	padding: 0 3px;
	font-size: 11px;
	color: white;
	background-color: #292a2d;
	word-wrap: break-word;
}

::placeholder {
	/* Chrome, Firefox, Opera, Safari 10.1+ */
	/* font-family: GoogleSans-Regular; */
	color: #000;
	opacity: 1; /* Firefox */
}

:-ms-input-placeholder {
	/* Internet Explorer 10-11 */
	/* font-family: GoogleSans-Regular; */
	color: #000;
}

::-ms-input-placeholder {
	/* Microsoft Edge */
	/* font-family: GoogleSans-Regular; */
	color: #000;
}

.combo-box .icon-x {
	background-color: #e5e5e5;
	border-radius: 50%;
	position: absolute;
	right: 50px;
	height: 15px;
	width: 15px;
	z-index: 1;
	cursor: pointer;
}

.combo-box .icon-x:hover {
	background-color: rgb(252, 140, 140);
}
.combo-box .icon-arrow-wrapper {
	border-radius: 0px 2px 2px 0px;
	position: absolute;
	right: 0;
	width: 32px;
	height: 100%;
	display: flex;
	align-items: center;
	justify-content: center;
}

.combo-box .icon-arrow-wrapper:hover {
	background-color: #e0e0e0;
}

/*point the arrow upwards when the select box is open (active):*/

.combo-box .icon-chevron-active {
	/* background-image: url('../../Resource/icon/chevron-up-solid.svg'); */
	transform: rotate(180deg);
	border: none;
	/* background-color: #e9ebee;s */
	border-radius: 4px 0px 0px 4px;
}

.combo-box .select-items {
	border-radius: 2px;
	cursor: pointer;
	box-shadow: 0 0 5px rgba(0, 0, 0, 0.4);
	height: fit-content;
	background-color: #ffffff;
}

/*style items (options):*/

.combo-box .select-items {
	display: none;
	position: absolute;
	top: calc(100% + 3px);
	left: 0;
	right: 0;
	max-height: 520%;
	overflow-y: auto;
}

.combo-box .select-items-bottom {
	top: auto;
	bottom: calc(100% + 1px) !important;
}

.select-items table {
	width: 100%;
	border-collapse: separate;
	border-spacing: 0;
}

.select-items table thead {
	position: sticky;
	top: 0;
	z-index: 20;
}

.select-items table tr {
	width: 100%;
	/* display: flex; */
}

.option-item-titles {
	height: 32px;
	background-color: #f4f5f8;
}

.option-item-titles .item-title {
	font-family: NotoSans-Bold;
	text-align: left;
	padding: 0 10px;
	/* padding: 0 10px; */
}

/* custom list-item */

.select-items .option-item {
	height: 32px;
	cursor: pointer;
}

.option-item .item-value {
	padding: 0 10px;
}

.selected {
	background-color: #e9ebee;
	color: #35bf22;
}

/* hover option */

.select-items .option-item:hover {
	background-color: #e9ebee;
	color: #2ca01c;
}

.select-items .option-item:focus {
	background-color: #e9ebee;
	color: #35bf22;
}

/*hide the items when the select box is closed:*/

.select-hide {
	display: none !important;
}

::-webkit-scrollbar {
	width: 0.6em;
	height: 0.6em;
	position: absolute;
	/* top: 40px; */
	left: 10px;
}

/* Track */

::-webkit-scrollbar-track {
	background: #f4f5f8;
}

/* Handle */

::-webkit-scrollbar-thumb {
	background: rgb(172, 169, 169);
	/* border-radius: 0.4em; */
}

/* Handle on hover */

::-webkit-scrollbar-thumb:hover {
	background: rgb(112, 112, 112);
}
</style>