import { api } from "../api/api";
import type { Transaction } from "../types/Transaction";
import type { CreateTransactionDTO } from "../types/CreateTransactionDTO";

export const getTransactions = async (): Promise<Transaction[]> => {
  const response = await api.get("/transactions");
  return response.data;
};

export const createTransaction = async (data: CreateTransactionDTO) => {
  return api.post("/transactions", data);
};