import { createSlice } from "@reduxjs/toolkit";  //no 'type'!
import type { PayloadAction } from "@reduxjs/toolkit";  //si 'type'!
import type { User } from "./types";

interface AuthState {
  user: User | null;
  loading: boolean;
  token: string | null;
}

const initialState: AuthState = {
  user: null,
  loading: false,
  token: null,
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    setUser: (state, action: PayloadAction<User | null>) => {
      state.user = action.payload;
    },
    setToken: (state, action: PayloadAction<string | null>) => {
      state.token = action.payload;
    },
    setLoading: (state, action: PayloadAction<boolean>) => {
      state.loading = action.payload;
    },
    logout: (state) => {
      state.user = null;
      state.token = null;
    },
  },
});

export const { setUser, setToken, setLoading, logout } = authSlice.actions;
export default authSlice.reducer;
