import { createSlice } from "@reduxjs/toolkit";
import { persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage";

const initialState = {
  customerName: "",
  phoneNumber: "",
  address: "",
  emailAddress: "",
  userName: null,
  totalAmount: 0,
  orderItems: [],
};

export const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    addDish: (state, action) => {
      const { dishId, price } = action.payload;
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
    deleteDish: (state, action) => {
      const { dishId, price } = action.payload;
      const existingItemIndex = state.orderItems.findIndex(
        (item) => item.dishId === dishId,
      );
      if (existingItemIndex !== -1) {
        const existingItem = state.orderItems[existingItemIndex];
        if (existingItem.quantity > 1) {
          existingItem.quantity -= 1;
        } else {
          state.orderItems.splice(existingItemIndex, 1);
        }
        state.totalAmount -= price;
      }
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
    },
  },
});
export const { addDish, deleteDish, setInputValue, resetCart } =
  cartSlice.actions;

const cartReducer = persistReducer(
  {
    key: "cart",
    storage: storage,
    whitelist: ["totalAmount", "orderItems", "userName"], // Список ключів, які ви хочете зберегти
  },
  cartSlice.reducer,
);

export default cartReducer;
