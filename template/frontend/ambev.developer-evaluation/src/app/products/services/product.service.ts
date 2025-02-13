import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { Product } from '../models/product.model';
import { ApiService } from '../../core/services/api.service';
import { PaginatedResponse } from '../../core/shared/models/paginated-response.model';

@Injectable({
  providedIn: 'root',
})
export class ProductService {

  constructor(private apiService: ApiService) {}

  getProducts(): Observable<Product[]> {
    return this.apiService.get<PaginatedResponse<Product>>('products').pipe(
      map(response => response?.data || []) 
    );
  }

  addProduct(product: Product): Observable<Product> {
    return this.apiService.post<Product>('products', product);
  }

  updateProduct(updatedProduct: Product): Observable<Product> {
    return this.apiService.put<Product>('products', updatedProduct.id, updatedProduct);
  }

  deleteProduct(id: number): Observable<boolean> {
    return this.apiService.delete('products', id);
  }

  getProductById(id: number): Observable<Product | undefined> {
    return this.apiService.getById<Product>('products', id).pipe(
      map(response => response || []) 
    );
  }
}
