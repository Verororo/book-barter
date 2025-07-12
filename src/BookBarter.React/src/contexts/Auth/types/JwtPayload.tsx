export type JwtPayload = {
  userName: string;
  identifier: number;
  role: string;
  exp: number;
}