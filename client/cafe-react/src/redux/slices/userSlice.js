import { createSlice } from "@reduxjs/toolkit";
import { persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage"; // або будь-яке інше зберігання, яке ви вибрали

const initialState = {
  userData: {},
  orderData: [],
};

const userSlice = createSlice({
  name: "user",
  initialState: initialState,
  reducers: {
    setUserData: (state, action) => {
      state.userData = action.payload
    },
  },
});

export const selectCurrentUserData = (state) => state.user.userData;
export const selectCurrentOrderData = (state) => state.user.orderData;

export const { setUserData } = userSlice.actions;

const userReducer = persistReducer(
  {
    key: "user",
    storage: storage,
    whitelist: ["userData"],
  },
  userSlice.reducer,
);

export default userReducer;
