import { createApp } from 'vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import App from './App.vue'
import * as ElementPlusIconsVue from '@element-plus/icons-vue'
import router from './router/index.js'
import axios from 'axios'
import VueAxios from 'vue-axios'
import horizontalScroll from 'el-table-horizontal-scroll'



const app = createApp(App)
app.use(ElementPlus)
app.use(router)-
app.use(VueAxios, axios)
app.use(horizontalScroll)
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component)
}


app.mount('#app')

