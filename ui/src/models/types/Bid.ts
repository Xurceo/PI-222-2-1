import type { ILot } from "./Lot";
import type { IUser } from "./User";

export interface IBid {
  id: string;
  amount: number;
  time: string;
  lot: ILot;
  user: IUser;
}
