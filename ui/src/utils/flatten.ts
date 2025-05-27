import type { ICategory } from "../types/Category.ts"; // if your interface is defined in a separate file

export function flattenCategories(categories: ICategory[]): ICategory[] {
    return categories.map(category => ({
        ...category,
        subcategories: flattenCategories(category.subcategories)
    }));
}