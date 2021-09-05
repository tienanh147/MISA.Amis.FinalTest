import Vue from "vue";
import VueRouter from 'vue-router';
import Home from '../components/HelloWorld.vue';
import Employee from '../components/layout/TheEmployeeContent.vue';
Vue.use(VueRouter);

const routers = [
    { path: '/', name: 'Home', component: Employee },
    { path: '/dict/employee', name: 'Employee', component: Employee },
    { path: '/dict/customer', name: 'Customer', component: Home },
]

const router = new VueRouter({
    mode: 'history',
    routes: routers
})

export default router;