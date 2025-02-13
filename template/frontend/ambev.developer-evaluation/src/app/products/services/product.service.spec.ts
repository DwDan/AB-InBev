import { TestBed } from '@angular/core/testing';
import { ProductService } from './product.service';
import { ApiService } from '../../core/services/api.service';
import { of } from 'rxjs';
import { Product } from '../models/product.model';

describe('ProductService', () => {
  let service: ProductService;
  let apiServiceSpy: jasmine.SpyObj<ApiService>;

  beforeEach(() => {
    const spy = jasmine.createSpyObj('ApiService', [
      'get',
      'post',
      'put',
      'delete',
      'getById',
    ]);

    TestBed.configureTestingModule({
      providers: [ProductService, { provide: ApiService, useValue: spy }],
    });

    service = TestBed.inject(ProductService);
    apiServiceSpy = TestBed.inject(ApiService) as jasmine.SpyObj<ApiService>;
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should retrieve a list of products', (done) => {
    const mockProducts: Product[] = [
      {
        id: 1,
        title: 'Product A',
        description: 'Description A',
        price: 10,
        category: 'Category A',
        image: 'image1.jpg',
        rating: { rate: 4.5, count: 10 },
      },
      {
        id: 2,
        title: 'Product B',
        description: 'Description B',
        price: 20,
        category: 'Category B',
        image: 'image2.jpg',
        rating: { rate: 4.0, count: 5 },
      },
    ];

    apiServiceSpy.get.and.returnValue(of({ data: mockProducts }));

    service.getProducts().subscribe((products) => {
      expect(products.length).toBe(2);
      expect(products).toEqual(mockProducts);
      done();
    });

    expect(apiServiceSpy.get).toHaveBeenCalledOnceWith('products');
  });

  it('should add a product', (done) => {
    const newProduct: Product = {
      id: 3,
      title: 'New Product',
      description: 'New Description',
      price: 30,
      category: 'New Category',
      image: 'new_image.jpg',
      rating: { rate: 5.0, count: 15 },
    };

    apiServiceSpy.post.and.returnValue(of(newProduct));

    service.addProduct(newProduct).subscribe((product) => {
      expect(product).toEqual(newProduct);
      done();
    });

    expect(apiServiceSpy.post).toHaveBeenCalledOnceWith('products', newProduct);
  });

  it('should update a product', (done) => {
    const updatedProduct: Product = {
      id: 1,
      title: 'Updated Product',
      description: 'Updated Description',
      price: 40,
      category: 'Updated Category',
      image: 'updated_image.jpg',
      rating: { rate: 4.8, count: 20 },
    };

    apiServiceSpy.put.and.returnValue(of(updatedProduct));

    service.updateProduct(updatedProduct).subscribe((product) => {
      expect(product).toEqual(updatedProduct);
      done();
    });

    expect(apiServiceSpy.put).toHaveBeenCalledOnceWith(
      'products',
      updatedProduct.id,
      updatedProduct
    );
  });

  it('should delete a product', (done) => {
    const productId = 1;
    apiServiceSpy.delete.and.returnValue(of(true));

    service.deleteProduct(productId).subscribe((response) => {
      expect(response).toBeTrue();
      done();
    });

    expect(apiServiceSpy.delete).toHaveBeenCalledOnceWith(
      'products',
      productId
    );
  });

  it('should retrieve a product by ID', (done) => {
    const mockProduct: Product = {
      id: 1,
      title: 'Product A',
      description: 'Description A',
      price: 10,
      category: 'Category A',
      image: 'image1.jpg',
      rating: { rate: 4.5, count: 10 },
    };

    apiServiceSpy.getById.and.returnValue(of(mockProduct));

    service.getProductById(mockProduct.id).subscribe((product) => {
      expect(product).toEqual(mockProduct);
      done();
    });

    expect(apiServiceSpy.getById).toHaveBeenCalledOnceWith(
      'products',
      mockProduct.id
    );
  });
});
