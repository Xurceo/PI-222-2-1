import type { IBid } from "./Bid";
import type { ILot } from "./Lot";

export interface IUser {
  id: string;
  username: string;
  role: string;
  lots: ILot[];
  bids: IBid[];
}
