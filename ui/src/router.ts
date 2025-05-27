import { createRouter, createWebHistory } from 'vue-router'

import Home from './components/Home.vue'
import Categories from "./components/Categories/Categories.vue";
import Category from "./components/Categories/Category.vue";
import Lots from "./components/Lots/Lots.vue";
import Users from "./components/Users/Users.vue";
import AddCategory from "./components/Categories/AddCategory.vue";
import AddLotToCategory from "./components/Lots/AddLotToCategory.vue";

const routes = [
    {
        path: '/',
        name: 'Home',
        component: Home,
    },
    {
        path: '/categories',
        name: 'Categories',
        component: Categories,
    },
    {
        path: '/category/:id',
        name: 'Category',
        component: Category,
        props: true,
    },
    {
        path: '/categories/add',
        name: 'AddCategory',
        component: AddCategory,
    },
    {

        path: '/category/:id/add-lot',
        name: 'AddLotToCategory',
        component: AddLotToCategory,
        props: true,
    },
    {
        path: '/lots',
        name: 'Lots',
        component: Lots,
    },
    {
        path: '/users',
        name: 'Users',
        component: Users,
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes,
})

export default router