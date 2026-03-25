export interface PersonReport {
  person: string;
  totalIncome: number;
  totalExpense: number;
  balance: number;
}

export interface SummaryReport {
  totalIncome: number;
  totalExpense: number;
  balance: number;
}

export interface ReportResponse {
  persons: PersonReport[];
  summary: SummaryReport;
}