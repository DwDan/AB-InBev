import { Address } from "./address.model";
import { Name } from "./name.model";

export interface User {
  id: number;
  username: string;
  password: string;
  email: string;
  name: Name;
  phone: string;
  status: number;
  role: number;
  address: Address;
}
