import { api } from "../api/api";
import type { ReportResponse } from "../types/Report";

export const getReport = async (): Promise<ReportResponse> => {
  const response = await api.get("/reports/persons");
  return response.data;
};