import { BrowserRouter, Routes, Route } from "react-router-dom";
import { PersonsPage } from "../pages/PersonsPage";
import { TransactionsPage } from "../pages/TransactionsPage";

export function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<PersonsPage />} />
        <Route path="/transactions" element={<TransactionsPage />} />
      </Routes>
    </BrowserRouter>
  );
}