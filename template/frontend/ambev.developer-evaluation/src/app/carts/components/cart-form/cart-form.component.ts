import { Component, inject, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CartService } from '../../services/cart.service';
import { CommonModule, Location } from '@angular/common';
import { MaterialModule } from '../../../core/shared/modules/material.module';
import { Cart, instanceCart } from '../../models/cart.model';
import { CartProduct } from '../../models/cart-product.model';
import { CartProductService } from '../../services/cart-product.service';
import { BranchService } from '../../../branchs/services/branch.service';
import { Branch } from '../../../branchs/models/branch.model';

@Component({
  selector: 'app-cart-form',
  standalone: true,
  imports: [CommonModule, MaterialModule, FormsModule, ReactiveFormsModule],
  templateUrl: './cart-form.component.html',
  styleUrls: ['./cart-form.component.scss'],
})
export class CartFormComponent implements OnInit {
  cart: Cart = instanceCart();
  branches: Branch[] = [];
  displayedColumns: string[] = [
    'id',
    'title',
    'quantity',
    'price',
    'discount',
    'finalPrice',
    'actions',
  ];

  private cartService = inject(CartService);
  private cartProductService = inject(CartProductService);
  private branchService = inject(BranchService);
  private route = inject(ActivatedRoute);
  private location = inject(Location);

  ngOnInit() {
    this.getBranches();

    this.route.params.subscribe((params) => {
      if (params['id']) {
        let cartId = +params['id'];
        this.cartService.getCartById(cartId).subscribe((cart) => {
          this.cart = cart!;
        });
      }
    });
  }

  onSubmit() {
    if (confirm('Deseja finalizar venda?')) {
      this.cart.isFinished = true;

      this.cartService.updateCart(this.cart).subscribe((response) => {
        alert('Venda finalizada com sucesso!');
        this.location.back();
      });
    }
  }

  onCancel() {
    if (confirm('Deseja cancelar venda?')) {
      this.cart.isCancelled = true;

      this.cartService.updateCart(this.cart).subscribe((response) => {
        alert('Venda cancelada com sucesso!');
        this.location.back();
      });
    }
  }

  updateCart() {
    this.cartService.updateCart(this.cart).subscribe((response) => {
      this.cart = response!;
    });
  }

  deleteCartProduct(product: CartProduct) {
    this.cartProductService.deleteCartProduct(product.id).subscribe(() => {
      this.cart.products = this.cart.products.filter(
        (p) => p.id !== product.id
      );

      this.updateCart();
    });
  }

  onReturn() {
    this.location.back();
  }

  increaseQuantity(product: CartProduct) {
    product.quantity++;
    this.updateCart();
  }

  decreaseQuantity(product: CartProduct) {
    product.quantity--;

    if (product.quantity == 0) {
      this.deleteCartProduct(product);
    } else {
      this.updateCart();
    }
  }

  deleteCart() {
    if (confirm('Tem certeza que deseja excluir este carrinho?')) {
      this.cartService.deleteCart(this.cart.id).subscribe(() => {
        this.location.back();
      });
    }
  }

  getBranches() {
    this.branchService.getBranches().subscribe((retorno) => {
      this.branches = retorno;
    });
  }
}
