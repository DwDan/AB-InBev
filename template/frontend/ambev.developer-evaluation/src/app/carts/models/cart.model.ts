import { CartProduct } from "./cart-product.model";

export interface Cart {
  id: number;
  date: Date;
  userId: number;
  products: CartProduct[];
}