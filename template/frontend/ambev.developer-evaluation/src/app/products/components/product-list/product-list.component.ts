import { Component, inject, OnInit } from '@angular/core';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MaterialModule } from '../../../core/shared/modules/material.module';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, MaterialModule, MatIconModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss',
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];

  displayedColumns: string[] = [
    'id',
    'title',
    'price',
    'description',
    'actions',
  ];

  private dialog = inject(MatDialog);
  private service = inject(ProductService);
  private router = inject(Router);

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    this.service.getProducts().subscribe((data) => {
      this.products = data;
    });
  }

  addProduct() {
    this.router.navigate(['products/add']);
  }

  editProduct(id: number) {
    this.router.navigate(['products/edit', id]);
  }

  deleteProduct(id: number) {
    if (confirm('Deseja realmente excluir este produto?')) {
      this.service.deleteProduct(id).subscribe(() => {
        this.loadProducts(); 
      });
    }
  }

  goToHome() {
    this.router.navigate(['/home']);
  }
}