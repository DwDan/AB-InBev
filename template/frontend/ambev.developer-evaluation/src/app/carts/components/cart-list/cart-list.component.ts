import { Component, inject, OnInit } from '@angular/core';
import { Cart } from '../../models/cart.model';
import { CartService } from '../../services/cart.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MaterialModule } from '../../../core/shared/modules/material.module';

@Component({
  selector: 'app-cart-list',
  standalone: true,
  imports: [CommonModule, MaterialModule, MatIconModule],
  templateUrl: './cart-list.component.html',
  styleUrl: './cart-list.component.scss',
})
export class CartListComponent implements OnInit {
  carts: Cart[] = [];

  displayedColumns: string[] = [
    'id',
    'date',
    'status',
    'actions',
  ];

  private service = inject(CartService);
  private router = inject(Router);

  ngOnInit() {
    this.loadCarts();
  }

  loadCarts() {
    this.service.getCarts().subscribe((data) => {
      this.carts = data;
    });
  }

  viewCart(id: number) {
    this.router.navigate(['carts', id]);
  }

  goToHome() {
    this.router.navigate(['/home']);
  }
}