export type User = {
  userName: string;
  email: string;
  city: string;
  role: 'User' | 'Moderator' | 'Admin';
}