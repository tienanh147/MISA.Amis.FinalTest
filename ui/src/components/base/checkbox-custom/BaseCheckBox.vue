<template>
	<div class="checkbox-custom" :class="{'blue-outline': isFocus}">
		<div class="checkbox" :class="{ checked: checked }">
			<div class="mi mi-16 mi-checkmark" v-if="checked"></div>
		</div>
		<input
			type="checkbox"
			@change="$emit('change', $event.target.checked)"
			v-bind:checked="checked"
			@focus="isFocus = true"
			@blur="isFocus = false"
		/>
		<label
			class="checkbox-label"
			v-if="label"
			@click="$emit('change', !checked)"
			>{{ label }}</label
		>
	</div>
</template>

<script>
export default {
	model: { prop: "checked", event: "change" },
	name: "CheckBox",
	props: {
		checked: {
			type: Boolean
		},
		label: {
			type: String,
			default() {
				return null;
			}
		}
	},
	data: function() {
		return {
			isFocus: false
		};
	},
	methods: {},
	watch: {}
};
</script>

<style scoped>
.checkbox-custom {
	position: relative;
	display: flex;
	justify-content: center;
	cursor: pointer;
}

.blue-outline:before {
	content: "";
	display: block;
	position: absolute;
	top: -4px;
	height: 26px;
	width: 26px;
	box-sizing: border-box;
	border: 1px solid rgb(222, 222, 245);
	border-radius: 2px;
	padding: 2px;
}

.checkbox-custom input {
	position: absolute;
	height: 18px;
	width: 18px;
	top: 0px;
	left: 0px;
	margin: 0;
	border: none;
	cursor: pointer;
	opacity: 0;
	/* display: none; */
}

.checkbox-label {
	font-size: 13px;
	position: absolute;
	left: 28px;
	white-space: nowrap;
	cursor: pointer;
}

.checked {
	/* transition: all 0.3s ease; */
	transform: rotate(90deg);
	border-color: #2ca01c !important;
	box-sizing: border-box;
}

.checkbox {
	transition: all 0.3s ease;
	height: 18px;
	width: 18px;
	box-sizing: border-box;
	background-color: #fff;
	border: 1px solid #AFAFAF;
	border-radius: 2px;
	display: block;
}

.checked .mi-checkmark {

	transform: rotate(-90deg);
}
</style>