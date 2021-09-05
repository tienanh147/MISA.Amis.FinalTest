<template>
	<div
		class="combobox"
		tabindex="10"
		:class="{
			'combobox--show': comboboxState,
			'combobox--error': comboboxInvalid,
		}"
	>
		<input
			@keydown.enter="inputSearchOnKeydownEnter($event)"
			@focus="inputSearchOnFocus($event)"
			@input="inputSeachOnInput($event)"
			@blur="inputSearchOnBlur($event)"
			v-model="inputValue"
			:placeholder="placeHolder"
			v-bind="$attrs"
			type="text"
			class="combobox__input"
		/>
		<!-- <div @click="cancelInputOnClick($event)" class="combobox__input-cancel">
			<i class="fas fa-times-circle"></i>
		</div> -->
		<!-- <div @click="comboboxDropdownOnClick($event)" class="combobox__dropdown">
			<i class="fas fa-chevron-down combobox__icon"></i>
		</div> -->
		<ul class="combobox__list">
			<li
				@click="itemOnClick(index)"
				v-for="(item, index) in comboboxData"
				:key="index"
				class="combobox__item"
				:class="{ 'combobox__item--active': index == currIdx }"
			>
				{{ item[comboboxField + "Name"] }}
			</li>
		</ul>
	</div>
</template>
<script>
export default {
	name: "BaseCombobox",
	props: {
		comboboxData: {
			type: Array,
			default: function() {
				return [];
			}
		},
		comboboxField: {
			type: String,
			default: ""
		},
		placeHolder: {
			type: String,
			default: ""
		}
	},
	data() {
		return {
			comboboxState: false,
			comboboxInvalid: false,
			currIdx: -1,
			inputValue: ""
		};
	},
	methods: {
		/**
		 * Bắt sự kiện click vào từng option của combobox
		 * CreatedBy: NTDUNG (02/08/2021)
		 * @param {number} index
		 */
		itemOnClick(index) {
			this.currIdx = index;
			this.comboboxState = false;
			if (index != -1) {
				this.inputValue = this.comboboxData[index][this.comboboxField + "Name"];
				this.$emit("changeComboboxValue", {
					ComboboxField: this.comboboxField,
					Value: this.comboboxData[index][this.comboboxField + "Id"]
				});
			}
		},
		/**
		 * Bắt sự kiện click vào nút cancel trong ô input
		 * CreatedBy: NTDUNG (02/08/2021)
		 * @param {event} event
		 */
		cancelInputOnClick(event) {
			let input = event.target.parentElement.parentElement.querySelector(
				"input"
			);
			input.focus();
			this.inputValue = "";
			this.resetListItem(event);
		},
		/**
		 * Sự kiện nhập vào ô tìm kiếm ở combobox
		 * CreatedBy: NTDUNG (02/08/2021)
		 * @param {event} event
		 */
		inputSeachOnInput(event) {
			let inputValue = this.inputValue.toLowerCase().trim();
			let liElements = event.target.parentElement.querySelectorAll("ul li");
			liElements.forEach(liElement => {
				let liVal = liElement.innerText.toLowerCase().trim();
				if (!liVal.includes(inputValue)) {
					liElement.style.display = "none";
				} else {
					liElement.style.display = "block";
				}
			});
		},
		/**
		 * Bắt sự kiện click vào nút dropdown ở combobox
		 * CreatedBy: NTDUNG (02/08/2021)
		 * @param {event} event
		 */
		comboboxDropdownOnClick(event) {
			this.comboboxState = !this.comboboxState;
			if (this.comboboxState) {
				this.comboboxInvalid = false;
				let input = event.target.parentElement.parentElement.querySelector(
					"input"
				);
				input.focus();
			} else {
				this.checkValueInput();
			}
		},
		/**
		 * Sự kiện focus vào ô input
		 * CreatedBy: NTDUNG (11/08/2021)
		 * @param {event} event
		 */
		inputSearchOnFocus(event) {
			this.comboboxState = true;
			this.comboboxInvalid = false;
			this.resetListItem(event);
		},
		/**
		 * Sự kiện blur ra ngoài ô input
		 * CreatedBy: NTDUNG (11/08/2021)
		 * @param {event} event
		 */
		inputSearchOnBlur(event) {
			if (event.relatedTarget !== null) {
				if (event.target.parentElement !== event.relatedTarget) {
					this.comboboxState = false;
				}
			} else {
				this.comboboxState = false;
			}
			console.log(event.relatedTarget);
		},
		/**
		 * Đưa danh sách lựa chọn về trạng thái đầy đủ
		 * CreatedBy: NTDUNG (11/08/2021)
		 * @param {event} event sự kiện khi tác động vào input
		 */
		resetListItem(event) {
			let liElements = event.target.parentElement.parentElement.querySelectorAll(
				"ul li"
			);
			liElements.forEach(liElement => {
				liElement.style.display = "block";
			});
		},
		/**
		 * Kiểm tra giá trị của input có hợp lệ hay không
		 * CreatedBy: NTDUNG (11/08/2021)
		 * @returns {boolean} trả về true là đúng, false là sai
		 */
		checkValueInput() {
			var foundIdx = this.comboboxData.findIndex(item => {
				return this.inputValue == item[this.comboboxField + "Name"];
			});
			if (foundIdx == -1) {
				this.itemOnClick(-1);
				if (this.inputValue != "") this.comboboxInvalid = true;
				else {
					this.$emit("changeComboboxValue", {
						ComboboxField: this.comboboxField,
						Value: ""
					});
				}
			} else {
				this.itemOnClick(foundIdx);
			}
		},
		/**
		 * Sự kiện nhấn enter ở input combobox
		 * CreatedBy: NTDUNG (11/08/2021)
		 */
		inputSearchOnKeydownEnter(event) {
			this.comboboxState = false;
			event.target.blur();
			this.checkValueInput();
		}
	}
};
</script>
<style scoped>
.combobox {
	height: 35px;
	width: 100%;
	background-color: #fff;
	/* top: 30%;
	left: 50%;
	transform: translate(-50%, -50%); */
	border-radius: 4px;
	border: 1px solid #bbb;
	display: flex;
	justify-content: space-between;
	position: relative;
	z-index: 100000;
	margin-top: 5px;
}

.combobox.combobox--show .combobox__list {
	display: block;
}

.combobox.combobox--show .combobox__icon {
	transform: rotate(180deg);
	transition: all 0.3s ease;
}

.combobox.combobox--show {
	border: 1px solid #019160;
}

.combobox.combobox--show .combobox__dropdown {
	border-left: 1px solid #019160;
}

.combobox.combobox--show .combobox__input-cancel {
	display: block;
}

.combobox.combobox--error {
	border: 1px solid red;
}

.combobox.combobox--error .combobox__dropdown {
	border-left: 1px solid red;
}

/* 1. COMBOBOX INPUT */
.combobox__input {
	width: calc(100% - 92px);
	border: 0;
	border-radius: 4px;
	outline: none;
	font-size: 14px;
	padding-left: 10px;
	background-color: #fff;
}
/* 1.1. COMBOBOX INPUT CANCEL */
.combobox__input-cancel {
	position: absolute;
	right: 62px;
	top: 50%;
	transform: translateY(-50%);
	opacity: 0.5;
	display: none;
}

.combobox__input-cancel:hover {
	opacity: 0.8;
	cursor: pointer;
}

/* 2. COMBOBOX DROPDOWN */
.combobox__dropdown {
	flex-grow: 0;
	border-left: 1px solid #bbb;
	width: 50px;
	font-size: 16px;
	text-align: center;
	line-height: 40px;
	cursor: pointer;
}
.combobox__dropdown:hover {
	background-color: #f5f5f5;
}
.combobox__dropdown:active {
	background-color: #efefef;
}
/* 2.1. COMBOBOX ICON */
.combobox__icon {
	transform: rotate(0);
	transition: all 0.3s ease;
}

/* 3. COMBOBOX LIST */
.combobox__list {
	position: absolute;
	background-color: #fff;
	box-shadow: 0 0 10px rgba(0, 0, 0, 0.4);
	top: 100%;
	left: 0;
	right: 0;
	margin-top: 3px;
	border-radius: 4px;
	list-style: none;
	padding-left: 0;
	overflow: hidden;
	display: none;
}
/* 3.1. COMBOBOX ITEM */
.combobox__item {
	height: 30px;
	padding: 5px 10px;
}

.combobox__item:hover {
	background-color: #e9e9e9;
	cursor: pointer;
}

.combobox__item.combobox__item--active {
	background-color: #0fb67e;
	color: #fff;
}

.combobox__item.combobox__item--active .combobox__check {
	visibility: visible;
}
/* 3.1.1. COMBOBOX CHECK */
.combobox__check {
	font-size: 14px;
	margin-right: 10px;
	visibility: hidden;
}
</style>