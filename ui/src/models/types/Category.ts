export interface ICategory {
  id: string | null;
  name: string;
  parentId: string | null;
  subcategoryIds: string[] | null;
  lotIds: string[] | null;
}
