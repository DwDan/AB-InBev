import { Geolocation } from "./geolocation.model";

export interface Address {
  city: string;
  street: string;
  number: string;
  zipcode: string;
  geolocation: Geolocation;
}
