import { api } from "../api/api";
import type { Person } from "../types/Person";

export const getPersons = async (): Promise<Person[]> => {
  const response = await api.get("/persons");
  return response.data;
}

export const createPerson = async (data: {
  name: string;
  age: number;
}) => {
  return api.post("/persons", data);
};

export const deletePerson = async (id: string) => {
  return api.delete(`/persons/${id}`);
};