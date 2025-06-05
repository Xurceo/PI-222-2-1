import type { LotStatus } from "../enums/LotStatus.ts";

export interface ILot {
  id: string | null;
  title: string;
  description: string;
  startPrice: number;
  winnerId: string | null;
  ownerId: string | null;
  categoryId: string;
  bidIds: string[] | null;
  status: LotStatus | null;
  startTime: Date | null;
  endTime: Date;
}
