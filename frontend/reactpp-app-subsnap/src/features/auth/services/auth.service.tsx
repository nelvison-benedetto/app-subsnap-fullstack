import type { Credentials, SignUpPayload, AuthResponse, User } from "../types";  //si 'type'!

const VITE_API_URL = import.meta.env.VITE_API_URL;

export async function login({ email, password }: Credentials): Promise<AuthResponse> {
  const res = await fetch(`${VITE_API_URL}/auth/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ email, password }),
    credentials: "include", // se usi cookie HttpOnly
  });

  if (!res.ok) throw new Error("Login failed");
  return res.json();
}

export async function signUp(payload: SignUpPayload): Promise<User> {
  const res = await fetch(`${VITE_API_URL}/auth/register`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(payload),
  });

  if (!res.ok) throw new Error("SignUp failed");
  return res.json();
}

export async function logout(): Promise<void> {
  await fetch(`${VITE_API_URL}/auth/logout`, {
    method: "POST",
    credentials: "include",
  });
}

export async function fetchCurrentUser(): Promise<User | null> {
  const res = await fetch(`${VITE_API_URL}/auth/me`, {
    method: "GET",
    credentials: "include",
  });
  if (!res.ok) return null;
  return res.json();
}
