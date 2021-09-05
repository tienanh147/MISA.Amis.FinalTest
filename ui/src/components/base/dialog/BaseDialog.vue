<template>
	<div class="modal-dialog">
		<div class="dialog animate" tabindex="0">
			<div class="dialog-header" v-if="header">{{ header }}</div>

			<div class="dialog-content">
				<div class="mi mi-36" :class="iconClass"></div>
				<div class="dialog-content-text" v-html="content"></div>
			</div>
			<div class="dialog-footer">
				<button
					class="button buttonCancel"
					@click="cancelBtn.function"
					tabindex="0"
					@keydown.right="focusConfirmBtn"
				>
					{{ cancelBtn.content }}
				</button>
				<button
					class="button"
					:class="btnClass"
					@click="confirmBtn.function"
					tabindex="0"
					@keydown.left="focusCancelBtn"
					@keydown.tab="focusDialog"
				>
					{{ confirmBtn.content }}
				</button>
			</div>
		</div>
	</div>
</template>
<script>
export default {
	name: "BaseDialog",
	props: {
		/** loại dialog:
		 * -- warning-yellow
		 * -- warning-red
		 */
		type: {
			type: String,
			default() {
				return "warning-yellow";
			}
		},
		/** Tiêu đề của dialog*/
		header: {
			type: String,
			default() {
				return null;
			}
		},
		/** Nội dung của dialog */
		content: {
			type: String,
			default() {
				return 'Bạn có chắc muốn đóng form nhập "Thông tin chung của thủ tục 603" hay không?';
			}
		},
		/** dữ liệu và sự kiện cho nút confirm của dialog */
		confirmBtn: {
			type: Object,
			default() {
				return {
					content: "Đóng",
					function: null,
					bgColor: "#019160",
					hover: "#01B075",
					color: "#fff"
				};
			}
		},
		/** dữ liệu và sự kiện cho nút cancel của dialog*/
		cancelBtn: {
			type: Object,
			default() {
				return {
					function: null,
					content: "Tiếp tục nhập",
					bgColor: "transparent",
					hover: "#E5E5E5",
					color: "#000"
				};
			}
		}
	},
	mounted() {
		this.focusDialog();
	},
	computed: {
		/** icon tương ứng với type dialog */
		iconClass() {
			if (this.type == "warning-yellow") return { "mi-warning-yellow": true };
			else if (this.type == "warning-red") return { "mi-warning-red": true };
			else return { "mi-warning-yellow": true };
		},

		/** class style cho button confirm tương ứng với type dialog */
		btnClass() {
			if (this.type == "warning-yellow") return { 'button-green': true };
			else if (this.type == "warning-red") return { 'button-red': true };
			else return { 'button-green': true };
		},
		/** style cho button cancel dựa theo dữ liệu truyền vào
		 * phần này để mở rộng nếu cần
		 */
		cancelBtnStyle() {
			return {
				backgroundColor: this.cancelBtn.bgColor,
				color: this.cancelBtn.color
			};
		},
		/** style cho button cancel dựa theo dữ liệu truyền vào
		 * . Phần này để mở rộng nếu cần
		 */
		confirmBtnStyle() {
			return {
				backgroundColor: this.confirmBtn.bgColor,
				color: this.confirmBtn.color
			};
		}
	},
	methods: {
		/** focus vào nút cancel */
		focusCancelBtn() {
			this.$el.querySelector(".btnCancel").focus();
		},
		/** focus vào nút confirm */
		focusConfirmBtn() {
			this.$el.querySelector(".btnCancel").nextElementSibling.focus();
		},
		/**
		 * focus vào dialog để có hể tab được vào 2 nút
		 * cancel và confirm mà không cần dùng chuột
		 * */
		focusDialog() {
			this.$el.querySelector(".dialog").focus();
		}
	}
};
</script>
<style scoped>
@import url("./dialog-variable.css");

.modal-dialog {
	position: fixed;
	z-index: 200;
	left: 0;
	top: 0;
	width: 100vw;
	height: 100vh;
	background-color: rgba(0, 0, 0, 0.4);
}

.dialog {
	outline: none;
	position: absolute;
	display: flex;
	flex-direction: column;
	width: var(--dialog-width);
	height: var(--dialog-height);
	left: calc(50% - var(--dialog-width) / 2);
	top: calc(50% - var(--dialog-height) / 2);
	border-radius: var(--border-radius);
	box-sizing: border-box;
	background-color: #fff;
}

.dialog .dialog-header {
	font-family: NotoSans-Bold;
	font-size: 15px;
	padding: 24px 24px 0 24px;
	height: var(--dialog-header-height);
	box-sizing: border-box;
}

.dialog .dialog-content {
	width: 100%;
	flex: 1;
	overflow: auto;
	padding: var(--dialog-padding-lr);
	box-sizing: border-box;
	display: flex;
	align-items: center;
}

.dialog-content .icon-warning {
	background-size: 25px;
	background-repeat: no-repeat;
	background-position: center 7px;
	margin-right: 10px;
	height: var(--icon-warning-size);
	width: var(--icon-warning-size);
}

.icon-warning.warning-yellow {
	background-image: url("../../../assets/icon/warning-yellow2.png");
}
.icon-warning.warning-red {
	background-image: url("../../../assets/icon/warning-red.png");
}
.dialog-content .dialog-content-text {
	line-height: 1.7;
	font-size: 13px;
	height: fit-content;
	max-height: 100%;
	width: 100%;
	flex: 1;
	overflow: auto;
	margin-left: 20px;
}

.dialog .dialog-footer {
	margin: 0 var(--form-padding-lr);
	border-top: 1px solid #e0e0e0;
	padding-top: 20px;
	height: calc(var(--dialog-footer-height));
	width: calc(100% - 2 * var(--dialog-padding-lr));
	border-radius: 0 0 var(--border-radius) var(--border-radius);
	display: flex;
	align-items: flex-start;
	justify-content: space-between;
	box-sizing: border-box;
	z-index: 1;
}

.dialog-footer .button {
	outline: none;
	display: flex;
	justify-content: center;
	align-items: center;
}
/* 
.dialog-footer .button.buttonCancel {
	background-color: transparent;
	border: 1px solid #8d9096;
	color: #000;
}
.dialog-footer .buttonCancel:hover,
.dialog-footer .buttonCancel:focus {
    background-color: #D2D3D6;
} */
/* .dialog-footer .btnCancel:hover {
  background-color: #e5e5e5;
}
.dialog-footer .btnCancel:focus {
  background-color: #e5e5e5;
} */

.animate {
	-webkit-animation: animatezoom 0.4s;
	animation: animatezoom 0.4s;
}

@-webkit-keyframes animatezoom {
	from {
		-webkit-transform: scale(0.7);
	}
	to {
		-webkit-transform: scale(1);
	}
}

@keyframes animatezoom {
	from {
		transform: scale(0.7);
	}
	to {
		transform: scale(1);
	}
}

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