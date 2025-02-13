import { Product } from '../../products/models/product.model';

export interface CartProduct {
  id: number;
  cartId: number;
  productId: number;
  quantity: number;
  unityPrice: number;
  discount: number;
  totalPrice: number;
  product?: Product;
}