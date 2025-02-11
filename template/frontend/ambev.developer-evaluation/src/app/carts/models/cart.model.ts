import { CartProduct } from "./cart-product.model";

export interface Cart {
  id: number;
  date: Date;
  userId: number;
  price : number;
  totalPrice : number;
  products: CartProduct[];
  isFinished: boolean;
  IsCancelled: boolean;
  branchId?: number;
}

export function instanceCart() {
  return <Cart>{
    products: [],
    id: 0,
    date: new Date(),
    userId: 0,
    price: 0,
    unityPrice: 0,
    discount: 0,
    totalPrice: 0,
    isFinished: false,
    IsCancelled: false
  };
}