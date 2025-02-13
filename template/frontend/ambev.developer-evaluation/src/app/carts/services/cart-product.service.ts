import { Injectable } from '@angular/core';
import { CartProduct } from '../models/cart-product.model';
import { Observable, map } from 'rxjs';
import { ApiService } from '../../core/services/api.service';

@Injectable({
  providedIn: 'root'
})
export class CartProductService {
  constructor(private apiService: ApiService) {}

  addCartProduct(cart: CartProduct): Observable<CartProduct> {
    return this.apiService.post<CartProduct>('cartproducts', cart);
  }

  updateCartProduct(updatedCartProduct: CartProduct): Observable<CartProduct> {
    return this.apiService.put<CartProduct>('cartproducts', updatedCartProduct.id, updatedCartProduct);
  }

  deleteCartProduct(id: number): Observable<boolean> {
    return this.apiService.delete('cartproducts', id);
  }

  getCartProductById(id: number): Observable<CartProduct | undefined> {
    return this.apiService
      .getById<CartProduct>('cartproducts', id)
      .pipe(map((response) => response || []));
  }
}
