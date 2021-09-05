<template>
	<div class="modal" v-show="isShow" @click="modalClick">
		<div class="form-wrapper animate" @keydown.esc="btnCancelClick">
			<div class="form-wrapper-header">
				<div class="header-title">Thông tin nhân viên</div>
				<div class="header-checkboxes">
					<CheckBox label="Là khách hàng" v-model="isCustomer" />
					<CheckBox label="Là nhà cung cấp" v-model="isSupplier" />
				</div>
				<div class="icon-default icon-x x-btn" @click="btnXClick"></div>
			</div>

			<div class="form-content" v-if="gotDetail">
				<div class="flex wrap justify-content-sb" tabindex="0">
					<div class="inputs-container width-1/2">
						<BaseInput
							style="width: 38.5%"
							ref="EmployeeCode"
							type="text"
							label="Mã"
							:required="true"
							v-model="formData.EmployeeCode"
						/>
						<BaseInput
							style="width: 60%"
							placeholder=""
							ref="FullName"
							label="Tên"
							:required="true"
							warningLabel="Thông tin này là bắt buộc"
							v-model="formData.FullName"
						/>
						<div class="input-wrapper" style="width: 100%">
							<label>Đơn vị <span style="color: red">*</span></label>
							<ComboBox
								v-if="gotSelectBoxes"
								label="Đơn vị"
								ref="DepartmentId"
								style="height: 32px"
								:required="true"
								:comboBoxData="selectBoxesData[0]"
								:idField="selectBoxesData[0][1]"
								:nameField="selectBoxesData[0][2]"
								:codeField="selectBoxesData[0][3]"
								:mapTitle="selectBoxesData[0][4]"
								v-model="formData.DepartmentId"
							/>
						</div>
						<BaseInput
							style="width: 100%"
							ref="PositionName"
							label="Chức danh"
							v-model="formData.PositionName"
						/>
					</div>
					<div class="inputs-container width-1/2">
						<BaseInput
							style="width: 38.5%"
							ref="DateOfBirth"
							type="date"
							label="Ngày sinh"
							v-model="formData.DateOfBirth"
						/>
						<div class="input-wrapper" style="width: 57%">
							<label>Giới tính</label>
							<div class="radio-buttons-group flex">
								<RadioButton
									:activatorValue="1"
									v-model="formData.Gender"
									label="Nam"
								/>
								<RadioButton
									:activatorValue="0"
									v-model="formData.Gender"
									label="Nữ"
								/>
								<RadioButton
									:activatorValue="2"
									v-model="formData.Gender"
									label="Khác"
								/>
							</div>
						</div>

						<BaseInput
							style="width: 60%"
							ref="IdentityNumber"
							label="Số CMTND/ Căn cước"
							v-model="formData.IdentityNumber"
						/>

						<BaseInput
							style="width: 38.5%"
							ref="IdentityDate"
							type="date"
							label="Ngày cấp"
							v-model="formData.IdentityDate"
						/>

						<BaseInput
							style="width: 100%"
							ref="IdentityPlace"
							:style="{ 'flex-basis': '100%' }"
							label="Nơi cấp"
							v-model="formData.IdentityPlace"
						/>
					</div>
				</div>

				<div class="flex wrap justify-content-sb">
					<BaseInput
						style="width: 100%"
						ref="Address"
						label="Địa chỉ"
						v-model="formData.Address"
					/>
					<BaseInput
						style="width: 25%"
						ref="MobilephoneNumber"
						label="ĐT di động"
						v-model="formData.MobilephoneNumber"
					/>
					<BaseInput
						style="width: 25%"
						ref="TelephoneNumber"
						label="ĐT cố định"
						v-model="formData.TelephoneNumber"
					/>
					<BaseInput
						style="width: 25%"
						ref="Email"
						label="Email"
						validate="email"
						v-model="formData.Email"
					/>
					<div style="width: calc(25% - 18px)"></div>
					<BaseInput
						style="width: 25%"
						ref="BankAccountNumber"
						label="Tài khoản ngân hàng"
						v-model="formData.BankAccountNumber"
					/>
					<BaseInput
						style="width: 25%"
						ref="BankName"
						label="Tên ngân hàng"
						v-model="formData.BankName"
					/>
					<BaseInput
						style="width: 25%"
						ref="BankBranchName"
						label="Chi nhánh"
						v-model="formData.BankBranchName"
					/>
					<div style="width: calc(25% - 18px)"></div>
				</div>
			</div>
			<div class="form-footer">
				<div class="left-footer">
					<button
						class="button"
						id="buttonCancel"
						@click="btnCancelClick"
						@keydown.right="focusBtnSave"
					>
						Hủy
					</button>
				</div>
				<div class="right-footer">
					<button
						class="button"
						id="buttonSave"
						@click="btnSaveClick"
						@keydown.right="focusBtnSaveAndAdd"
						@keydown.left="focusBtnCancel"
					>
						Cất
					</button>
					<button
						objectId
						class="button button-green"
						id="buttonSaveAndAdd"
						@click="btnSaveAndAddClick"
						@keydown.left="focusBtnSave"
						@keydown.tab="focusForm"
					>
						Cất và Thêm
					</button>
				</div>
			</div>
		</div>
		<Dialog
			v-if="dialog.isShow"
			:header="dialog.header"
			:content="dialog.content"
			:type="dialog.type"
			:confirmBtn="dialog.confirmBtn"
			:cancelBtn="dialog.cancelBtn"
		/>
	</div>
</template>

<script>
import RadioButton from "../radio/BaseRadio.vue";
import ComboBox from "../combobox/BaseComboBox.vue";
import Dialog from "../dialog/BaseDialog.vue";
import SelectBox from "../select-custom/BaseSelectBox.vue";
import BaseInput from "../input/BaseInput.vue";
import CurrencyInput from "../input/CurrencyInput.vue";
import CheckBox from "../checkbox-custom/BaseCheckBox.vue";
export default {
	name: "ModalForm",
	components: { BaseInput, Dialog, CheckBox, ComboBox, RadioButton },
	props: {
		/** trạng thái hiển thị của form
		 * --true: hiển thị
		 * --false: ẩn
		 */
		isShow: {
			type: Boolean,
			default() {
				return false;
			}
		},
		/** Id của dữ liệu cần lấy */
		formDataId: {
			type: String
		},
		// /** dữ liệu của các selectBox  */
		// selectBoxesData: {
		// 	type: Array
		// },

		formMode: {
			type: Number
		}
	},
	mounted() {
		this.getSelectBoxesData();
	},
	data() {
		return {
			gotSelectBoxes: false,
			formData: {},
			gotDetail: false,
			selectBoxesData: [],
			/**
			 * các thông số của dialog popup
			 */
			dialog: {
				isShow: false,
				header: "Xóa bản ghi",
				content: "Bạn có chắc muốn xóa các nhân viên hay không?",
				type: "warning-red",
				confirmBtn: { content: "Xóa", function: null },
				cancelBtn: { content: "Tiếp tục nhập", function: null }
			},

			isCustomer: false,
			isSupplier: false,
			saveAndAddMode: false
			// formMode: 0,
		};
	},
	methods: {
		modalClick(event) {
			if (event.target == this.$el) {
				this.btnCancelClick();
			}
		},

		btnXClick() {
			var vm = this;
			var fullName = this.formData.FullName;
			var code = this.formData.EmployeeCode;
			var dialogSetting = {
				type: "warning-yellow",
				content:
					vm.formMode == 2
						? `Bạn có muốn đóng form thông tin của nhân viên "<b>${fullName}</b>" không?`
						: `Bạn có muốn đóng form thêm mới nhân viên "<b>${code}</b>" không?`,
				cancelBtn: {
					content: "Tiếp tục nhập",
					function: function() {
						vm.dialog.isShow = false;
						vm.focusForm();
						// vm.$nextTick(()=>{});
					}
				},
				confirmBtn: {
					content: "Đóng",
					function: vm.hideModalForm
				},
				isShow: true
			};
			this.dialog = dialogSetting;
		},
		btnCancelClick() {
			this.hideModalForm();
			// var vm = this;
			// var fullName = this.formData.FullName;
			// var code = this.formData.EmployeeCode;
			// var dialogSetting = {
			// 	type: "warning-yellow",
			// 	content:
			// 		vm.formMode == 2
			// 			? `Bạn có muốn đóng form thông tin của nhân viên "<b>${fullName}</b>" không?`
			// 			: `Bạn có muốn đóng form thêm mới nhân viên "<b>${code}</b>" không?`,
			// 	cancelBtn: {
			// 		content: "Tiếp tục nhập",
			// 		function: function() {
			// 			vm.dialog.isShow = false;
			// 			vm.focusForm();
			// 			// vm.$nextTick(()=>{});
			// 		}
			// 	},
			// 	confirmBtn: {
			// 		content: "Đóng",
			// 		function: vm.hideModalForm
			// 	},
			// 	isShow: true
			// };
			// this.dialog = dialogSetting;
		},

		hideModalForm() {
			this.dialog.isShow = false;
			this.$emit("hideForm");
		},

		validateOnSave() {
			var refs = this.$refs;
			for (var prop in refs) {
				var ref = refs[prop];
				if (ref.required) {
					if (ref.inputValue == null || ref.inputValue == "") {
						ref.$el.querySelector("input").focus();
						ref.warning = `${ref.label} không được để trống.`;
						return false;
					}
				}
				if (ref.warning != null) {
					ref.$el.querySelector("input").focus();
					return false;
				}
			}
			return true;
		},

		btnSaveAndAddClick: function() {
			if (!this.validateOnSave()) return;
			this.saveAndAddMode = true;
			this.saveModalForm();
		},

		btnSaveClick: function() {
			if (!this.validateOnSave()) return;
			this.saveAndAddMode = false;
			this.saveModalForm();
			// var vm = this;
			// var fullName = this.formData.FullName;
			// var dialogSetting = {
			// 	type: "warning-yellow",
			// 	content:
			// 		vm.formMode == 2
			// 			? `Bạn có muốn cập nhật thông tin của nhân viên "<b>${fullName}</b>" không?`
			// 			: `Bạn có muốn thêm mới nhân viên "<b>${fullName}</b>" không?`,
			// 	cancelBtn: {
			// 		content: "Tiếp tục nhập",
			// 		function: function() {
			// 			vm.dialog.isShow = false;
			// 			vm.focusForm();
			// 		}
			// 	},
			// 	confirmBtn: {
			// 		content: vm.formMode == 2 ? "Cập nhật" : "Thêm mới",
			// 		function: function() {
			// 			if (vm.validateOnSave()) {
			// 				vm.saveModalForm();
			// 			}
			// 		}
			// 	},
			// 	isShow: true
			// };
			// this.dialog = dialogSetting;
		},

		saveModalForm() {
			this.dialog.isShow = false;
			this.formData["Gender"] = parseInt(this.formData["Gender"]);
			var dataPost = JSON.stringify(this.formData);
			console.log(dataPost);
			if (this.formMode == 0) {
				this.axios
					.post("https://localhost:44348/api/v1/Employees", dataPost, {
						headers: {
							// Overwrite Axios's automatically set Content-Type
							"Content-Type": "application/json"
						}
					})
					.then(res => {
						console.log(res);
						if (res.status == 201) {
							this.$eventBus.$emit("ToastMessage", {
								type: "success",
								content: `Thêm mới thành công: ${this.formData["FullName"]} - ${this.formData["EmployeeCode"]}`,
								duration: 3000
							});
							this.$emit("refreshData");
							this.resetForm();
							if (!this.saveAndAddMode) this.hideModalForm();
							else {
								this.getNewCode("EmployeeCode");
								this.$refs["EmployeeCode"].$el.querySelector("input").focus();
							}
						} else {
							this.$eventBus.$emit("ToastMessage", {
								type: "error",
								content: res.data.userMsg,
								duration: 3000
							});
						}
					})
					.catch(error => {
						this.$eventBus.$emit("ToastMessage", {
							type: "error",
							content: error.response.data.userMsg,
							duration: 3000
						});
					});
			} else if (this.formMode == 2) {
				this.axios
					.put(
						`https://localhost:44348/api/v1/Employees/${this.formDataId}`,
						dataPost,
						{
							headers: {
								// Overwrite Axios's automatically set Content-Type
								"Content-Type": "application/json"
							}
						}
					)
					.then(res => {
						if (res.status == 200) {
							this.$eventBus.$emit("ToastMessage", {
								type: "success",
								content: `Cập nhật thành công: ${this.formData["FullName"]}-${this.formData["EmployeeCode"]}`,
								duration: 3000
							});
							this.$emit("refreshData");
							this.resetForm();
							if (!this.saveAndAddMode) this.hideModalForm();
							else {
								this.getNewCode("EmployeeCode");
								this.$refs["EmployeeCode"].$el.querySelector("input").focus();
							}
							// this.resetForm();
						} else {
							this.$eventBus.$emit("ToastMessage", {
								type: "error",
								content: res.data.userMsg,
								duration: 3000
							});
						}
					})
					.catch(error => {
						this.$eventBus.$emit("ToastMessage", {
							type: "error",
							content: error.response.data.userMsg,
							duration: 3000
						});
					});
			}
		},

		focusBtnCancel() {
			this.$el.querySelector("#buttonCancel").focus();
		},

		focusBtnSave() {
			this.$el.querySelector("#buttonSave").focus();
		},
		focusBtnSaveAndAdd() {
			this.$el.querySelector("#buttonSaveAndAdd").focus();
		},

		focusForm() {
			this.$el.querySelector(".form-content .flex").focus();
		},

		resetForm() {
			// this.formDataId=null;
			// this.formMode = 0;
			this.$emit("configFormMode", 0, null);
			this.formData = {};
			this.$refs["DepartmentId"].inputValue = "";
		},
		getDetail: async function() {
			await this.axios
				.get(`https://localhost:44348/api/v1/Employees/${this.formDataId}`)
				.then(res => {
					if (res.status == 200) {
						this.formData = res.data;
						this.gotDetail = true;
					} else {
						this.hideModalForm();
						this.$eventBus.$emit("ToastMessage", {
							type: "error",
							content: "Không tìm thấy dữ liệu",
							duration: 3000
						});
					}
				})
				.catch(error => {
					this.$eventBus.$emit("ToastMessage", {
						type: "error",
						content: error.response.data.userMsg,
						duration: 3000
					});
				});
		},

		/** Hàm lấy dữ liệu cho các Select Box và thêm các trường init content, idField, nameField vào đầu mảng.
		 * --initContent: Nội dung hiển thị khi không chọn.
		 * --idField: trường id của object.
		 * --nameField: trường nội dung của object.
		 * CreatedBy: TTAnh(05/08/2021)
		 */
		getSelectBoxesData: function() {
			this.selectBoxesData = [];
			this.gotSelectBoxes = false;
			this.axios
				.get("https://localhost:44348/api/v1/Departments")
				.then(res => {
					const arrayData = res.data;
					const mapTitle = new Map();
					//#region Thêm trường nameField và idField
					mapTitle.set("DepartmentCode", "Mã đơn vị");
					mapTitle.set("DepartmentName", "Tên đơn vị");
					arrayData.unshift(mapTitle);
					arrayData.unshift("DepartmentCode");
					arrayData.unshift("DepartmentName");
					arrayData.unshift("DepartmentId");

					//#endregion

					//#region Thêm trường initContent
					arrayData.unshift("");
					//#endregion
					this.selectBoxesData.push(arrayData);
					this.gotSelectBoxes = true;
				})
				.catch(error => {
					console.log(error);
				});
		},

		getNewCode: function(codeField) {
			this.axios
				.get("https://localhost:44348/api/v1/Employees/NewCode")
				.then(res => {
					if (res.status == 200) this.$set(this.formData, codeField, res.data);
				})
				.catch(error => {
					console.log(error.response.status);
				});
			this.gotDetail = true;
		}
	},
	watch: {
		/**
		 * nếu hiển thị form(isShow=true) thì lấy code mới hoặc lấy thông tin chi tiết
		 * nếu ẩn form thì reset các trường
		 */
		isShow: async function(val) {
			if (val) {
				// this.gotSelectBoxes = true;
				if (this.formMode == 0) {
					await this.getNewCode("EmployeeCode");
				} else if (this.formMode == 2) {
					await this.getDetail();
				}
				this.$nextTick(function() {
					this.$refs.EmployeeCode.$el.querySelector("input").focus();
				});
			} else {
				// this.gotSelectBoxes = false;
				this.gotDetail = false;
				this.formData = {};
			}
		}
	}
};
</script>

<style scoped>
@import url("./modal-form-custom.css");
/* custom-scrollbar */

::-webkit-scrollbar {
	width: 0.6rem;
	height: 0.6rem;
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