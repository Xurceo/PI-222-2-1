import axios from 'axios'
import type {ICategory} from "../types/Category.ts";

const api = import.meta.env.VITE_API_URL

export async function AddCategory(category: ICategory): Promise<void> {
    try{
        await axios.post(`${api}/categories`, category);
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.error('Axios error:', error.response?.data || error.message);
        } else {
            console.error('Unexpected error:', error);
        }
    }
}

export async function UpdateCategory(category: ICategory): Promise<void> {
    try{
        await axios.post(`${api}/categories/update`, category);
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.error('Axios error:', error.response?.data || error.message);
        } else {
            console.error('Unexpected error:', error);
        }
    }
}

export async function fetchCategories(): Promise<ICategory[]> {
    try{
        const response = await axios.get(`${api}/categories`)
        return response.data as ICategory[]
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.error('Axios error:', error.response?.data || error.message);
        } else {
            console.error('Unexpected error:', error);
        }
        throw error
    }
}

export async function fetchCategoryById(id: string): Promise<ICategory> {
    try{
        const response = await axios.get(`${api}/categories/${id}`)
        console.log(response.data)
        return response.data as ICategory
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.error('Axios error:', error.response?.data || error.message);
        } else {
            console.error('Unexpected error:', error);
        }
        throw error
    }
}