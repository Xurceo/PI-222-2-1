export interface IUser {
  id: string | null;
  username: string;
  role: string;
  lotIds: string[] | null;
  bidIds: string[] | null;
}
