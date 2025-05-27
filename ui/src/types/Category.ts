import type { ILot } from "./Lot.ts";

export interface ICategory {
    id: string
    name: string
    parentId?: string | null;
    subCategoryIds?: string[];
    subcategories: ICategory[];
    lotIds?: string[];
    lots: ILot[];
}