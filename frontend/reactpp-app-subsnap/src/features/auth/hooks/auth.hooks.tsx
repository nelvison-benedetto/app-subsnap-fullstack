import { useMutation, useQuery, useQueryClient, UseQueryOptions  } from "@tanstack/react-query";
import { login, logout, fetchCurrentUser, signUp } from "../services/auth.service";
import { useAppDispatch } from "../../../app/hooks.tsx";
import { setUser, setToken, logout as logoutReducer } from "../auth.slice";

export function useSignIn() {
  const dispatch = useAppDispatch();
  const qc = useQueryClient();

  return useMutation({
    mutationFn: login,
    onSuccess: ({ user, token }) => {
      dispatch(setUser(user));
      dispatch(setToken(token));
      qc.invalidateQueries(["currentUser"]);
    },
  });
}

export function useSignUp() {
  const dispatch = useAppDispatch();
  return useMutation({
    mutationFn: signUp,
    onSuccess: (user) => {
      dispatch(setUser(user));
    },
  });
}

export function useSignOut() {
  const dispatch = useAppDispatch();
  const qc = useQueryClient();
  return useMutation({
    mutationFn: logout,
    onSuccess: () => {
      dispatch(logoutReducer());
      qc.clear(); // reset cache
    },
  });
}

export function useCurrentUser() {
  const dispatch = useAppDispatch();

  const queryOptions: UseQueryOptions<User | null, Error, User | null, ["currentUser"]> = {
    queryKey: ["currentUser"],
    queryFn: fetchCurrentUser,
    staleTime: 1000 * 60 * 5,
    onSuccess: (user) => {
      if (user) dispatch(setUser(user));
    },
  };
  return useQuery(queryOptions);
}


