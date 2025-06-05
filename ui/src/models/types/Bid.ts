import type { ILot } from "./Lot";
import type { IUser } from "./User";

export interface IBid {
  id: string;
  amount: number;
  time: Date;
  lot: ILot | null;
  lotId: string;
  user: IUser | null;
  userId: string;
}
