import { api } from "../api/api";
import type { Category } from "../types/Category";

export const getCategories = async (): Promise<Category[]> => {
  const response = await api.get("/categories");
  return response.data;
};

export const createCategory = async (data: {
  description: string;
  purpose: number;
}) => {
  return api.post("/categories", data);
};