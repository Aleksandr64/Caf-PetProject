import axios from "axios";
import { jwtDecode } from "jwt-decode";
import { store } from "../redux/store";
import {
  logOut,
  selectCurrentAccessToken,
  setCredentials,
} from "../redux/slices/authSlice";

export const refreshAccessToken = async () => {
  const oldAccessToken = selectCurrentAccessToken(store.getState());
  try {
    const response = await axios.post(
      "http://localhost:5179/api/Auth/GetNewAccessToken",
      {},
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${oldAccessToken}`,
        },
        withCredentials: true,
      },
    );
    const newAccessToken = response.data;
    const tokenPrincipal = jwtDecode(newAccessToken);
    console.log(tokenPrincipal);
    store.dispatch(
      setCredentials({
        accessToken: newAccessToken,
        userName: tokenPrincipal["userName"],
        role: tokenPrincipal["role"],
      }),
    );
    return newAccessToken;
  } catch (error) {
    console.error("Failed to refresh access token", error);
    await logoutUser();
    throw error;
  }
};

export const logoutUser = async () => {
  try {
    await axios.post(
      "http://localhost:5179/api/Auth/Logout",
      {},
      { withCredentials: true },
    );
    store.dispatch(logOut());
  } catch (err) {
    console.error("Failed to logout", err);
  }
};
