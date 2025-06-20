import Home from "../components/Home.vue";
import Categories from "../components/Categories/Categories.vue";
import Category from "../components/Categories/Category.vue";
import Lots from "../components/Lots/Lots.vue";
import Users from "../components/Users/Users.vue";
import AddCategory from "../components/Categories/AddCategory.vue";
import AddLot from "../components/Lots/AddLot.vue";
import Login from "../components/Shared/Login.vue";
import Profile from "../components/Users/Profile.vue";
import Lot from "../components/Lots/Lot.vue";
import PlaceBid from "../components/Bids/PlaceBid.vue";
import Register from "../components/Shared/Register.vue";

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
    path: "/category/:categoryId",
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
    path: "/lots",
    name: "Lots",
    component: Lots,
  },
  {
    path: "/lots/:lotId",
    name: "Lot",
    component: Lot,
    props: true,
  },
  {
    path: "/lots/:lotId/place-bid",
    name: "PlaceBid",
    component: PlaceBid,
    props: true,
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
    path: "/register",
    name: "Register",
    component: Register,
  },
  {
    path: "/profile/:userId",
    name: "Profile",
    component: Profile,
    props: true,
  },
  {
    path: "/profile/:userId/create-lot",
    name: "AddLot",
    component: AddLot,
    props: true,
  },
];
