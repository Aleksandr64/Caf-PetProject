import { createSlice } from "@reduxjs/toolkit";
import { persistReducer } from "redux-persist";
import storage from "redux-persist/lib/storage";

const initialState = {
  userName: null,
  accessToken: null,
  role: null,
};

const authSlice = createSlice({
  name: "auth",
  initialState: initialState,
  reducers: {
    setCredentials: (state, actions) => {
      const { accessToken, userName, role } = actions.payload;
      state.userName = userName;
      state.accessToken = accessToken;
      state.role = role;
    },
    logOut: (state) => {
      state.userName = null;
      state.accessToken = null;
      state.role = null;
    },
  },
});

export const selectCurrentUserName = (state) => state.auth.userName;
export const selectCurrentAccessToken = (state) => state.auth.accessToken;
export const selectCurrentRole = (state) => state.auth.role;

export const { setCredentials, logOut } = authSlice.actions;

const authReducer = persistReducer(
  {
    key: "auth",
    storage: storage,
    whitelist: ["userName", "accessToken", "role"],
  },
  authSlice.reducer,
);

export default authReducer;
