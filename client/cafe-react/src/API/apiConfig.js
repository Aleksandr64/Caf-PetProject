import axios from "axios";
import { store } from "../redux/store";
import { selectCurrentAccessToken } from "../redux/slices/authSlice";
import { refreshAccessToken } from "./authUtils";

const api = axios.create({
  baseURL: "http://localhost:5179/api",
  withCredentials: true,
});

let isRefreshing = false;
let refreshTokenPromise = null;

const getNewAccessToken = () => {
  if (isRefreshing) {
    return refreshTokenPromise;
  }
  isRefreshing = true;

  refreshTokenPromise = refreshAccessToken()
    .then((newAccessToken) => {
      isRefreshing = false;
      refreshTokenPromise = null;
      return newAccessToken;
    })
    .catch((error) => {
      console.log("Failed to refresh access token", error);
      isRefreshing = false;
      refreshTokenPromise = null;
      throw error;
    });

  return refreshTokenPromise;
};

api.interceptors.request.use(
  (config) => {
    const token = selectCurrentAccessToken(store.getState());
    if (token && config.headers) {
      config.headers["Authorization"] = `Bearer ${token}`;
    }
    config.withCredentials = true;
    return config;
  },
  (error) => {
    return Promise.reject(error);
  },
);

api.interceptors.response.use(
  (response) => {
    return response;
  },
  async (error) => {
    const originalRequest = error.config;
    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;
      try {
        const newToken = await getNewAccessToken();
        axios.defaults.headers.common["Authorization"] = `Bearer ${newToken}`;
        if (originalRequest.headers) {
          originalRequest.headers["Authorization"] = `Bearer ${newToken}`;
        }
        return api(originalRequest);
      } catch (err) {
        return Promise.reject(err);
      }
    }
    return Promise.reject(error);
  },
);

export default api;
