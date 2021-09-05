import Vue from "vue";
// import EmployeePage from "./page/EmployeePage.vue";
import App from './App.vue';
import axios from "axios";
import VueAxios from "vue-axios";
import EventBus from "./event-bus";
import router from './router'

// Vue.prototype.$eventBus = EventBus;
Vue.use(VueAxios, axios);
Vue.config.productionTip = false;

Vue.mixin({

    filters: {
        dateFormat: function(date) {
            date = new Date(date);

            if (date.getTime() == 0) {
                return "";
            } else {
                var day = date.getDate();
                var month = date.getMonth() + 1;
                var year = date.getFullYear();
                day = day < 10 ? "0" + day : day;
                month = month < 10 ? "0" + month : month;
                return day + "/" + month + "/" + year;
            }
        },
        salaryFormat: function(salary) {
            if (salary != null) {
                var num = salary.toFixed(0).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
                return num;
            }
            return "";
        },
        genderFormat: function(gender) {
            if (
                gender == "Nam" ||
                gender == "Nữ" ||
                gender == "nam" ||
                gender == "nữ"
            ) {
                return gender;
            } else if (gender == "Không xác định") {
                return "Khác";
            } else {
                return " ";
            }
        },
    }
});

new Vue({
    router,
    render: (h) => h(App),
}).$mount("#app");