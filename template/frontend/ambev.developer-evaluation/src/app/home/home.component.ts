import { Component, inject, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MaterialModule } from '../core/shared/modules/material.module';
import { Product } from '../products/models/product.model';
import { ProductService } from '../products/services/product.service';
import { CartService } from '../carts/services/cart.service';
import { Cart, instanceCart } from '../carts/models/cart.model';
import { CartProduct } from '../carts/models/cart-product.model';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule, MaterialModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit {
  products: Product[] = [];
  cart: Cart = instanceCart();

  displayedColumns: string[] = [
    'id',
    'title',
    'price',
    'description',
    'actions',
  ];

  private service = inject(ProductService);
  private cartService = inject(CartService);

  ngOnInit() {
    this.loadProducts();
    this.getActiveCart();
  }

  loadProducts() {
    this.service.getProducts().subscribe((data) => {
      this.products = data;
    });
  }

  getActiveCart() {
    this.cartService.getActiveCartBy().subscribe((data) => {
      this.cart = data;
    });
  }

  openCartDialog(id: number) {
    if (confirm('Deseja realmente incluir este produto no carrinho?')) {
      let existingProduct = this.cart.products.find((p) => p.productId === id);

      if (existingProduct) {
        existingProduct.quantity += 1;
      } else {
        this.cart.products.push(<CartProduct>{
          productId: id,
          quantity: 1,
        });
      }

      this.cartService.updateCart(this.cart).subscribe((response) => {
        if (response) {
          this.cart = response;
        }
      });
    }
  }
}
