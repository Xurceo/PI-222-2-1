import type { IBid } from "./Bid";
import type { ICategory } from "./Category";
import type { IUser } from "./User";
import type { LotStatus } from "../enums/LotStatus.ts";

export interface ILot {
  id: string;
  title: string;
  description: string;
  startPrice: number;
  winner: IUser;
  owner: IUser;
  category: ICategory;
  bids: IBid[];
  status: LotStatus;
  startTime: Date;
  endTime: Date;
}
