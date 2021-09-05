<template>
	<div
		class="radio-custom"
		@click="$emit('click', activatorValue)"
		:class="{ 'blue-outline': isFocus }"
	>
		<div class="radio-button" :class="{ checked: checked }">
			<div class="circle"></div>
		</div>
		<input
			type="radio"
			:checked="checked"
			@focus="isFocus = true"
			@blur="isFocus = false"
		/>
		<label v-html="label"></label>
	</div>
</template>

<script>
export default {
	name: "RadioButton",
	model: { prop: "value", event: "click" },
	props: {
		value: {
			type: [String, Number]
		},
		activatorValue: {
			type: [String, Number]
		},
		label: {
			type: String
		}
	},
	data() {
		return { isFocus: false };
	},
	computed: {
		checked() {
			return this.value == this.activatorValue;
		}
	}
};
</script>

<style scoped>
.radio-custom {
	display: flex;
	position: relative;
	cursor: pointer;
	justify-content: center;
}

.blue-outline:before {
	content: "";
	display: block;
	position: absolute;
	top: -4px;
  left: -4px;
	height: 26px;
	width: 26px;
	box-sizing: border-box;
	border: 1px solid rgb(222, 222, 245);
	border-radius: 50%;
	padding: 2px;
}

.radio-custom label {
  margin-left: 10px;
	font-family: NotoSans-Regular;
	font-size: 13px;
	cursor: pointer;
}

.radio-custom input {
	position: absolute;
	height: 18px;
	width: 18px;
	top: 0px;
	left: 0px;
	margin: 0;
	border: none;
	cursor: pointer;
	opacity: 0;
}

.circle {
	border-radius: 50%;
	height: 10px;
	width: 10px;
	background-color: transparent;
}

.checked {
	/* transition: all 0.3s ease; */
	/* transform: rotate(90deg); */
	/* background-color: #2ca01c; */
	/* padding: 3px; */
	border-color: #2ca01c !important;
	box-sizing: border-box;
}

.checked .circle {
	background-color: #2ca01c;
}

.radio-button {
	padding: 3px;
	transition: all 0.3s ease;
	height: 18px;
	width: 18px;
	box-sizing: border-box;
	border: 1px solid #afafaf;
	border-radius: 50%;
	display: block;
}
</style>