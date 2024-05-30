import { createSlice } from "@reduxjs/toolkit";
import { persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage";

const initialState = {
  customerName: "",
  phoneNumber: "",
  address: "",
  emailAddress: "", // Виправлення помилки в імені властивості
  totalAmount: 0,
  orderItems: [],
};

export const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    addDish: (state, action) => {
      const { dishId, price } = action.payload;
      console.log(dishId, price);
      const existingItem = state.orderItems.find(
        (item) => item.dishId === dishId,
      );
      if (existingItem) {
        existingItem.quantity += 1;
      } else {
        state.orderItems.push({ dishId: dishId, quantity: 1 });
      }
      state.totalAmount += price;
    },
    setInputValue: (state, action) => {
      const { propertyName, value } = action.payload;
      state[propertyName] = value;
    },
    resetCart: (state) => {
      state.customerName = "";
      state.phoneNumber = "";
      state.address = "";
      state.emailAddress = "";
      state.totalAmount = 0;
      state.orderItems = [];
    }
  },
});
export const {
  addDish,
  setInputValue,
  resetCart
} = cartSlice.actions;

const cartReducer = persistReducer(
  {
    key: "cart",
    storage: storage,
    whitelist: ["totalAmount", "orderItems"], // Список ключів, які ви хочете зберегти
  },
  cartSlice.reducer,
);

export default cartReducer;

