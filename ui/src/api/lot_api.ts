import axios from 'axios'
import type {ILot} from "../types/Lot.ts";

const api = import.meta.env.VITE_API_URL

export async function addLot(category: ILot): Promise<void> {
    try{
        await axios.post(`${api}/lots`, category);
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.error('Axios error:', error.response?.data || error.message);
        } else {
            console.error('Unexpected error:', error);
        }
    }
}

export async function fetchLots(): Promise<ILot[]> {
    try{
        const response = await axios.get(`${api}/lots`)
        return response.data as ILot[]
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.error('Axios error:', error.response?.data || error.message);
        } else {
            console.error('Unexpected error:', error);
        }
        throw error
    }
}

export async function fetchLotById(id: number): Promise<ILot> {
    try{
        const response = await axios.get(`${api}/lots/${id}`)
        return response.data as ILot
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.error('Axios error:', error.response?.data || error.message);
        } else {
            console.error('Unexpected error:', error);
        }
        throw error
    }
}