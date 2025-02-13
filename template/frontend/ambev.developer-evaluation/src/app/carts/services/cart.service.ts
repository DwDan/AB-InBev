import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Cart } from '../models/cart.model';
import { ApiService } from '../../core/services/api.service';
import { PaginatedResponse } from '../../core/shared/models/paginated-response.model';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  constructor(private apiService: ApiService) {}

  getCarts(): Observable<Cart[]> {
    return this.apiService
      .get<PaginatedResponse<Cart>>('carts')
      .pipe(map((response) => response?.data || []));
  }

  addCart(cart: Cart): Observable<Cart> {
    return this.apiService.post<Cart>('carts', cart);
  }

  updateCart(updatedCart: Cart): Observable<Cart> {
    return this.apiService.put<Cart>('carts', updatedCart.id, updatedCart);
  }

  deleteCart(id: number): Observable<boolean> {
    return this.apiService.delete('carts', id);
  }

  getCartById(id: number): Observable<Cart | undefined> {
    return this.apiService
      .getById<Cart>('carts', id)
      .pipe(map((response) => response || []));
  }

  getActiveCartBy(): Observable<Cart> {
    return this.apiService
      .get<Cart>('carts/active')
      .pipe(map((response) => response || []));
  }
}
