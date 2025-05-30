import Home from "../components/Home.vue";
import Categories from "../components/Categories/Categories.vue";
import Category from "../components/Categories/Category.vue";
import Lots from "../components/Lots/Lots.vue";
import Users from "../components/Users/Users.vue";
import AddCategory from "../components/Categories/AddCategory.vue";
import AddLotToCategory from "../components/Lots/AddLotToCategory.vue";
import Login from "../components/Shared/Login.vue";
import Profile from "../components/Users/Profile.vue";

export const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/categories",
    name: "Categories",
    component: Categories,
  },
  {
    path: "/category/:id",
    name: "Category",
    component: Category,
    props: true,
  },
  {
    path: "/categories/add",
    name: "AddCategory",
    component: AddCategory,
  },
  {
    path: "/category/:id/add-lot",
    name: "AddLotToCategory",
    component: AddLotToCategory,
    props: true,
  },
  {
    path: "/lots",
    name: "Lots",
    component: Lots,
  },
  {
    path: "/users",
    name: "Users",
    component: Users,
  },
  {
    path: "/login",
    name: "Login",
    component: Login,
  },
  {
    path: "/profile/:id",
    name: "Profile",
    component: Profile,
    props: true,
  },
];
