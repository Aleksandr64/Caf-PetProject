import { createSlice } from "@reduxjs/toolkit";
import {persistReducer} from "redux-persist";
import storage from "redux-persist/lib/storage";

const initialState = {
  dishes: [],
};

export const dishSlice = createSlice({
  name: "dishes",
  initialState,
  reducers: {
    setAllDish: (state, action) => {
      state.dishes = action.payload;
    },
  },
});

export const { setAllDish } = dishSlice.actions;

const dishReducer = persistReducer(
  {
    key: "dishes",
    storage: storage,
    whitelist: ["dishes"], // Список ключів, які ви хочете зберегти
  },
  dishSlice.reducer,
);

export default dishReducer;
