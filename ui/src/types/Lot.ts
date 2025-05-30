import type { IBid } from "./Bid";
import type { ICategory } from "./Category";
import type { IUser } from "./User";

export interface ILot {
  id: string;
  title: string;
  description: string;
  startingPrice: number;
  winner: IUser;
  owner: IUser;
  category: ICategory;
  bids: IBid[];
  isEnded: boolean;
}
