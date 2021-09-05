<template>
	<div class="menu" :class="{ expand: expand }">
		<div class="logo-container">
			<router-link
				v-if="expand"
				class="mi mi-24 mi-menu-option menu-option"
				to="#"
			></router-link>
			<router-link class="logo-image" to="#" v-if="expand"></router-link>
			<div
				class="mi mi-24 mi-sidebar-three-stripes menu-expander"
				v-if="!expand"
				@click="expandMenu"
			></div>
		</div>
		<div class="menu-items-list">
			<router-link class="menu-item" to="/" data-title="Tổng quan">
				<div class="menu-item-icon">
					<div class="mi mi-24 mi-sidebar-dashboard"></div>
				</div>
				<div class="menu-item-title" v-if="expand">Tổng quan</div>
			</router-link>
			<router-link class="menu-item" to="#">
				<div class="menu-item-icon">
					<div class="mi mi-24 mi-sidebar-cash"></div>
				</div>
				<div class="menu-item-title" v-if="expand">Tiền mặt</div>
			</router-link>
			<router-link class="menu-item" to="#">
				<div class="menu-item-icon">
					<div class="mi mi-24 mi-sidebar-bank"></div>
				</div>
				<div class="menu-item-title" v-if="expand">Tiền gửi</div>
			</router-link>
			<router-link class="menu-item" to="#">
				<div class="menu-item-icon">
					<div class="mi mi-24 mi-sidebar-shopping"></div>
				</div>
				<div class="menu-item-title" v-if="expand">Mua hàng</div>
			</router-link>
			<router-link class="menu-item" to="#">
				<div class="menu-item-icon">
					<div class="mi mi-24 mi-sidebar-sale"></div>
				</div>
				<div class="menu-item-title" v-if="expand">Bán hàng</div>
			</router-link>
			<router-link class="menu-item" to="#">
				<div class="menu-item-icon">
					<div class="mi mi-24 mi-sidebar-invoice"></div>
				</div>
				<div class="menu-item-title" v-if="expand">Quản lý hóa đơn</div>
			</router-link>
			<router-link class="menu-item" to="#">
				<div class="menu-item-icon">
					<div class="mi mi-24 mi-sidebar-stock"></div>
				</div>
				<div class="menu-item-title" v-if="expand">Kho</div>
			</router-link>
			<router-link class="menu-item" to="#">
				<div class="menu-item-icon">
					<div class="mi mi-24 mi-sidebar-tools"></div>
				</div>
				<div class="menu-item-title" v-if="expand">Công cụ dụng cụ</div>
			</router-link>
			<router-link class="menu-item" to="#">
				<div class="menu-item-icon">
					<div class="mi mi-24 mi-sidebar-fixed-assets"></div>
				</div>
				<div class="menu-item-title" v-if="expand">Tài sản cố định</div>
			</router-link>
			<router-link class="menu-item" to="#">
				<div class="menu-item-icon">
					<div class="mi mi-24 mi-sidebar-tax"></div>
				</div>
				<div class="menu-item-title" v-if="expand">Thuế</div>
			</router-link>
			<router-link class="menu-item" to="#">
				<div class="menu-item-icon">
					<div class="mi mi-24 mi-sidebar-price"></div>
				</div>
				<div class="menu-item-title" v-if="expand">Giá thành</div>
			</router-link>
			<router-link class="menu-item" to="#">
				<div class="menu-item-icon">
					<div class="mi mi-24 mi-sidebar-general"></div>
				</div>
				<div class="menu-item-title" v-if="expand">Tổng hợp</div>
			</router-link>
			<router-link class="menu-item" to="#">
				<div class="menu-item-icon">
					<div class="mi mi-24 mi-sidebar-budget"></div>
				</div>
				<div class="menu-item-title" v-if="expand">Ngân sách</div>
			</router-link>
			<router-link class="menu-item" to="#">
				<div class="menu-item-icon">
					<div class="mi mi-24 mi-sidebar-report"></div>
				</div>
				<div class="menu-item-title" v-if="expand">Báo cáo</div>
			</router-link>
			<router-link class="menu-item" to="#">
				<div class="menu-item-icon">
					<div class="mi mi-24 mi-sidebar-finance"></div>
				</div>
				<div class="menu-item-title" v-if="expand">Phân tích tài chính</div>
			</router-link>
		</div>
	</div>
</template>

<script>
export default {
	name: "Menu",
	created() {
		this.$eventBus.$on("narrow-menu", () => {
			this.expand = false;
		});
	},
	data() {
		return {
			expand: true
		};
	},
	methods: {
		expandMenu() {
			this.expand = true;
			this.$eventBus.$emit("expand-menu");
		}
	}
};
</script>
<style scoped>
.menu {
	width: fit-content;
	/* min-width: 178px; */
	height: 100vh;
	background-color: #393a3d;
}

.menu.expand {
	width: 178px;
}

.menu .logo-container {
	display: flex;
	align-items: center;
	height: 48px;
	box-sizing: border-box;
}

.logo-container .menu-option {
	margin-right: 10px;
	display: block;
	margin-left: 24px;
}
.logo-container .logo-image {
	background-image: url("../../assets/icon/Logo_Module_TiengViet_White.66947422.svg");
	background-repeat: no-repeat;
	background-size: contain;
	height: 32px;
	width: 96px;
}

.logo-container .menu-expander {
	margin-left: 14px;
}

.menu .menu-items-list {
	box-sizing: border-box;
	padding-top: 14px;
	overflow: auto;
	height: calc(100vh - 48px);
}

.menu .menu-item {
	font-size: 13px;
	font-family: NotoSans-Regular;
	color: #ffffff !important;
	text-decoration: none;
	display: flex;
	align-items: center;
	height: 42px;
	width: 100%;
	background-color: #393a3d;
	padding-left: 8px;
	box-sizing: border-box;
	cursor: pointer;
	border-left: 6px solid transparent;
}

.menu .menu-item:hover {
	background-color: #5d5d61;
}

/* .menu .menu-item:hover > .menu-icon {
	filter: brightness(0);
} */

.menu .menu-item-active {
	font-family: NotoSans-Bold;
	background-color: #5d5d61;
	color: #ffffff !important;
	border-left-color: #2ca01c;
}

/* .menu .menu-item-active > .menu-icon {
	filter: brightness(3);
}

.menu .menu-item-active:hover > .menu-icon {
	filter: brightness(0);
} */

.menu .menu-item:visited {
	color: transparent;
}

.menu .menu-item .menu-item-icon {
	display: flex;
	width: 24px;
	justify-content: center;
	align-items: center;
	margin-right: 16px;
	background-size: contain;
}

.menu.expand .menu-item .menu-item-icon {
	margin-right: 8px;
}

/* custom-scrollbar */

::-webkit-scrollbar {
	width: 6px;
	height: 6px;
}

/* Track */

::-webkit-scrollbar-track {
	background: #393a3d;
}

/* Handle */

::-webkit-scrollbar-thumb {
	background: #393a3d;
	/* border-radius: 0.4em; */
}

/* Handle on hover */

::-webkit-scrollbar-thumb:hover {
	background: #767677;
}
</style>