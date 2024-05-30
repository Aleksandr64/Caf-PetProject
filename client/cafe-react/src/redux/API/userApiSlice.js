import { apiSlice } from "./apiSlice";

export const userApiSlice = apiSlice.injectEndpoints({
  endpoints: (builder) => ({
    getUserData: builder.mutation({
      query: () => "User/GetUserData",
    }),
  }),
});

export const { useGetUserDataMutation } = userApiSlice;