export interface CreateTransactionDTO {
  description: string;
  amount: number;
  type: number;
  personId: string;
  categoryId: string;
}