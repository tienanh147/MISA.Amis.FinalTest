<template>
	<div class="header">
		<div class="header-left">
			<div
				class="mi mi-24 mi-three-stripes menu-narrower"
				@click="narrowMenu"
				v-show="expandMenu"
			></div>
			<div class="list-dropdown">
				<div class="dropdown-text">Công ty cổ phần MISA</div>
				<i class="dropdown-arrow fas fa-solid fa-chevron-down"></i>
			</div>
		</div>
		<div class="header-right">
			<div class="account">
				<div class="avatar"></div>
				<div class="name">Trần Tiến Anh</div>
				<div class="more"></div>
			</div>
		</div>
	</div>
</template>

<script>
export default {
	name: "Header",
	created() {
		this.$eventBus.$on("expand-menu", () => {
			this.expandMenu = true;
		});
	},
	data() {
		return {
			expandMenu: true
		};
	},
	methods: {
		narrowMenu() {
			this.expandMenu = false;
			this.$eventBus.$emit("narrow-menu");
		}
	}
};
</script>

<style scoped>
.header {
	height: 48px;
	width: 100%;
	display: flex;
	justify-content: space-between;
}

.header .header-left {
	height: 100%;
	display: flex;
	align-items: center;
}

.header .header-left .list-dropdown {
	height: 100%;
	min-width: 300px;
	display: flex;
	align-items: center;
}

.header .header-left .menu-narrower {
	margin: 0 10px;
}

.header .header-left {
	display: flex;
	align-items: center;
	justify-content: space-between;
}

.header .header-left .list-dropdown {
	margin-left: 10px;
}

.header .header-left .list-dropdown .dropdown-text {
	/* padding-left: 10px; */
	overflow: hidden;
	white-space: nowrap;
	font-weight: 600;
	max-width: 600px;
	text-overflow: ellipsis;
	transition: all 0.2s ease;
	font-size: 14px;
	color: #111;
	text-transform: uppercase;
}

.header .header-left .list-dropdown .dropdown-arrow {
	color: black;
	font-size: 14px;
	margin-right: 20px;
	cursor: pointer;
}

.header .header-right {
	display: flex;
	align-items: center;
}

.header .header-right .account {
	display: grid;
	align-items: center;
	justify-content: flex-end;
	grid-template-columns: auto auto auto;
	/* height: 48px; */
}

.header .header-right .account .avatar {
	height: 30px;
	width: 30px;
	background-image: url("../../assets/icon/avatar-default.png");
	background-repeat: no-repeat;
	background-size: contain;
}

.header .header-right .account .name {
	margin: 0 16px 0 16px;
	font-family: NotoSans-SemiBold;
	font-size: 14px;
	white-space: nowrap;
}

.header .header-right .account .more {
	margin-right: 16px;
	height: 16px;
	width: 16px;
	background-image: url("../../assets/icon/option.png");
	background-size: contain;
	background-repeat: no-repeat;
	background-position: center;
	cursor: pointer;
}
</style>
