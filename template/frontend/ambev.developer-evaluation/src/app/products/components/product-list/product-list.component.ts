import { Component, inject, OnInit } from '@angular/core';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product.service';
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';
import { ConfirmationDialogComponent } from '../../../core/shared/components/confirmation-dialog/confirmation-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MaterialModule } from '../../../core/shared/modules/material.module';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, RouterOutlet, MaterialModule, MatIconModule],
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

  deleteProduct(id: number) {
    this.service.deleteProduct(id).subscribe(() => {
      this.loadProducts(); 
    });
  }

  editProduct(id: number) {
    this.router.navigate(['products/edit', id]);
  }

  openDeleteDialog(id: number) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      data: { message: 'Deseja realmente excluir este produto?' },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.deleteProduct(id);
      }
    });
  }
}
