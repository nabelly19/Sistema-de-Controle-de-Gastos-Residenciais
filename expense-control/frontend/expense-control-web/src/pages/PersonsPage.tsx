import { useState } from "react";
import type { Person } from "../types/Person";
import { getPersons, createPerson, deletePerson } from "../services/personService";

export function PersonsPage() {
  const [persons, setPersons] = useState<Person[]>([]);
  const [name, setName] = useState("");
  const [age, setAge] = useState(0);

  const load = async () => {
    const data = await getPersons();
    setPersons(data);
  };

//   useEffect(() => {
//     load();
//   }, []);

  const handleCreate = async () => {
    await createPerson({ name, age });
    setName("");
    setAge(0);
    load();
  };

  const handleDelete = async (id: string) => {
    await deletePerson(id);
    load();
  };

  return (
    <>
    <div>
      <h2>Pessoas</h2>

      <input placeholder="Nome" value={name} onChange={e => setName(e.target.value)} />
      <input type="number" placeholder="Idade" value={age} onChange={e => setAge(Number(e.target.value))} />
      <button onClick={handleCreate}>Criar</button>

      <ul>
        {persons.map(p => (
          <li key={p.id}>
            {p.name} - {p.age}
            <button onClick={() => handleDelete(p.id)}>Excluir</button>
          </li>
        ))}
      </ul>
    </div>
    </>
  );
}