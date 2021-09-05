<template>
  <div id="app">

    <LeftMenu/>
		<RightContent/>
    
  </div>
</template>

<script>
// import HelloWorld from './components/HelloWorld.vue';
import Header from "./components/layout/TheHeader.vue";
import LeftMenu from "./components/layout/TheLeftMenu.vue";
import RightContent from "./components/layout/TheRightContent.vue";
import EmployeeContent from "./components/layout/TheEmployeeContent.vue";
export default {
  name: "App",
  components: {
    LeftMenu,
		RightContent
  }
};
</script>

<style>
@import "./css/main.css";

</style>
