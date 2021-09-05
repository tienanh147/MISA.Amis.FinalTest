<template>
	<div class="right-container">
		<Header />
		<router-view></router-view>
	</div>
</template>
<script>
import Header from "./TheHeader.vue";
export default {
	name: "RightContent",
	components: { Header }
};
</script>

<style scoped>
.right-container {
	display:flex;
	flex-direction: column;
	position: relative;
	flex: 1;
	height: 100vh;
	overflow: hidden;
}
</style>