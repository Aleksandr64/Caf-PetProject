import { configureStore, combineReducers } from "@reduxjs/toolkit";
import { persistStore } from "redux-persist";
import cartReducer from "./slices/cartSlice";
import dishReducer from "./slices/dishSlice";
import authReducer from "./slices/authSlice";
import userSlice from "./slices/userSlice";

const rootReducer = combineReducers({
  user: userSlice,
  cart: cartReducer,
  dish: dishReducer,
  auth: authReducer,
});

export const store = configureStore({
  reducer: rootReducer,
  devTools: true,
});
export const persistor = persistStore(store);
