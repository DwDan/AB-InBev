import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  ReactiveFormsModule,
  FormsModule,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../../core/shared/modules/material.module';
import { Product } from '../../models/product.model';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [CommonModule, MaterialModule, FormsModule, ReactiveFormsModule],
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss'],
})
export class ProductFormComponent implements OnInit {
  productForm!: FormGroup;
  isEditMode = false;
  productId!: number;
  product?: Product;
  stars = Array(5).fill(0);
  imagePreview: string = "https://upload.wikimedia.org/wikipedia/commons/a/ac/No_image_available.svg"; 
  defaultImage: string = "https://upload.wikimedia.org/wikipedia/commons/a/ac/No_image_available.svg"; 
  baseImageUrl: string = "https://your-image-server.com/images/"; 

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.productForm = this.fb.group({
      title: ['', Validators.required],
      category: ['', Validators.required],
      price: [0, [Validators.required, Validators.min(1)]],
      description: ['', Validators.required],
      image: ['', Validators.required],
      rating: this.fb.group({
        rate: [0, [Validators.min(0), Validators.max(5)]],
        count: [0, Validators.min(0)],
      }),
    });

    this.route.params.subscribe((params) => {
      if (params['id']) {
        this.isEditMode = true;
        this.productId = +params['id'];
        this.productService.getProductById(this.productId).subscribe((product) => {
          if (product) {
            this.product = product;
            this.productForm.patchValue({
              ...product,
            });
          }
        });
      }
    });
  }

  onSubmit() {
    if (this.productForm.valid) {
      const formValue = this.productForm.value;

      if (this.isEditMode && this.product) {
        this.productService.updateProduct({
          id: this.product.id,
          ...formValue,
        }).subscribe(() => {
          this.router.navigate(['/products']);
        });
      } else {
        this.productService.addProduct(formValue).subscribe(() => {
          this.router.navigate(['/products']);
        });
      }
    }
  }

  onCancel() {
    this.router.navigate(['/products']);
  }

  setRating(value: number) {
    this.productForm.get("rating.rate")?.setValue(value);
  }


  updateImagePreview() {
    let imageUrl = this.productForm.get("image")?.value.trim();

    if (!imageUrl) {
      this.imagePreview = this.defaultImage;
      return;
    }

    if (!imageUrl.startsWith("http")) {
      imageUrl = `${this.baseImageUrl}${imageUrl}`;
    }

    this.imagePreview = imageUrl;
  }

  handleImageError() {
    this.imagePreview = this.defaultImage;
    this.productForm.get("image")?.setValue("");
  }
}

