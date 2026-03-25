import axios from "axios";
import { useState } from "react";
import type { Transaction } from "../types/Transaction";
import type { CreateTransactionDTO } from "../types/CreateTransactionDTO";
import type { Person } from "../types/Person";
import type { Category } from "../types/Category";
import { getTransactions, createTransaction } from "../services/transactionService";
import { getPersons } from "../services/personService";
import { getCategories } from "../services/categoryService";

export function TransactionsPage() {
  const [transactions, setTransactions] = useState<Transaction[]>([]);
const [persons, setPersons] = useState<Person[]>([]);
const [categories, setCategories] = useState<Category[]>([]);

const [form, setForm] = useState<CreateTransactionDTO>({
  description: "",
  amount: 0,
  type: 0,
  personId: "",
  categoryId: ""
});

  const [error, setError] = useState("");

  const load = async () => {
    const [t, p, c] = await Promise.all([
      getTransactions(),
      getPersons(),
      getCategories()
    ]);

    setTransactions(t);
    setPersons(p);
    setCategories(c);
  };

//   useEffect(() => {
//     load();
//   }, []);

  const handleCreate = async () => {
  try {
    setError("");
    await createTransaction(form);

    setForm({
      description: "",
      amount: 0,
      type: 0,
      personId: "",
      categoryId: ""
    });

    load();
  } catch (err: unknown) {
    if (axios.isAxiosError(err)) {
      setError(err.response?.data || "Erro na requisição");
    } else {
      setError("Erro inesperado");
    }
  }
};

  return (
    <div>
      <h2>Transações</h2>

      {error && <p style={{ color: "red" }}>{error}</p>}

      <input
        placeholder="Descrição"
        value={form.description}
        onChange={e => setForm({ ...form, description: e.target.value })}
      />

      <input
        type="number"
        placeholder="Valor"
        value={form.amount}
        onChange={e => setForm({ ...form, amount: Number(e.target.value) })}
      />

      <select
        value={form.type}
        onChange={e => setForm({ ...form, type: Number(e.target.value) })}
      >
        <option value={0}>Despesa</option>
        <option value={1}>Receita</option>
      </select>

      <select
        value={form.personId}
        onChange={e => setForm({ ...form, personId: e.target.value })}
      >
        <option value="">Selecione Pessoa</option>
        {persons.map(p => (
          <option key={p.id} value={p.id}>
            {p.name}
          </option>
        ))}
      </select>

      <select
        value={form.categoryId}
        onChange={e => setForm({ ...form, categoryId: e.target.value })}
      >
        <option value="">Selecione Categoria</option>
        {categories.map(c => (
          <option key={c.id} value={c.id}>
            {c.description}
          </option>
        ))}
      </select>

      <button onClick={handleCreate}>Criar</button>

      <hr />

      <ul>
        {transactions.map(t => (
          <li key={t.id}>
            {t.description} - {t.amount} - {t.type === 0 ? "Despesa" : "Receita"}
          </li>
        ))}
      </ul>
    </div>
  );
}