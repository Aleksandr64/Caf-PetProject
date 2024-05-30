import { configureStore, combineReducers } from "@reduxjs/toolkit";
import {
  persistStore,
  FLUSH,
  REHYDRATE,
  PAUSE,
  PERSIST,
  PURGE,
  REGISTER,
} from "redux-persist";
import { apiSlice } from "./API/apiSlice";
import cartReducer from "./slices/cartSlice";
import dishReducer from "./slices/dishSlice";
import authReducer from "./slices/authSlice";
import userSlice from "./slices/userSlice";


const rootReducer = combineReducers({
  user: userSlice,
  cart: cartReducer,
  dish: dishReducer,
  auth: authReducer,
  [apiSlice.reducerPath]: apiSlice.reducer,
});

export const store = configureStore({
  reducer: rootReducer,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: {
        ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
      },
    }).concat(apiSlice.middleware),
  devTools: true,
});
export const persistor = persistStore(store);
