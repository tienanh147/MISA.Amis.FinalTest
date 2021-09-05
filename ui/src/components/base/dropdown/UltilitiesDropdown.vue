<template>
	<div class="ultilities-dropdown" :style="positionStyle" v-show="true">
		<ul>
			<li
				v-for="(ultility, index) in ultilities"
				:key="index"
				@click="ultility.function(affectObject)"
			>
				{{ ultility.content }}
			</li>
		</ul>
	</div>
</template>

<script>
export default {
	name: "UltilitiesDropDown",
	props: {
		/**
		 *
		 */
		ultilities: {
			type: Array
		}
	},
	mounted() {
		this.$eventBus.$on("ultilities-dropdown-show", data => {
			this.setPosition(data);
		});

		this.$eventBus.$on("ultilities-dropdown-hide", () => {
			this.isShow = false;
		});
	},
	destroyed() {
		// Stop listening the event hello with handler
		this.$eventBus.$off("ultilities-dropdown-show");
	},
	data() {
		return {
			top: 0,
			left: 0,
			isShow: false,
      affectObject: null
		};
	},
	methods: {
		setPosition(data) {
			this.isShow = true;
			this.top = data.top;
			this.left = data.left;
      this.affectObject = data.affectObject;
		}
	},
	computed: {
		positionStyle() {
			return {
				top: this.top + "px",
				left: this.left + "px"
			};
		}
	}
};
</script>

<style>
.ultilities-dropdown {
	position: fixed;
	height: fit-content;
	width: fit-content;
	background-color: #fff;
	border: 1px solid #bbb;
	border-radius: 2px;
	box-sizing: border-box;
	padding: 2px 2px;
	/* transform: translate(-100%); */
	/* transition: opacity 0.25s, transform 0.25s, width 0.3s ease; */
}

.ultilities-dropdown ul {
	list-style-type: none;
	padding: 0;
	margin: 0;
}
.ultilities-dropdown ul li {
	cursor: pointer;
	height: 32px;
	display: flex;
	align-items: center;
	padding: 0 10px;
	white-space: nowrap;
	transition: all 0.2s ease;
}

.ultilities-dropdown ul li:hover {
	background-color: #eceef1;
	color: #2ca01c;
}
</style>