export interface User {
  id: string;
  email: string;
  name?: string;
  roles?: string[];
}

export interface Credentials {
  email: string;
  password: string;
}

export interface AuthResponse {
  user: User;
  token: string; // JWT
}

export interface SignUpPayload {
  email: string;
  password: string;
  name?: string;
}
