export type JwtPayload = {
  userName: string;
  id: number;
  role: string;
  exp: number;
}