import type { ILot } from "./Lot.ts";

export interface ICategory {
  id: string;
  name: string;
  parent: ICategory | null;
  subcategories: ICategory[];
  lots: ILot[];
}
