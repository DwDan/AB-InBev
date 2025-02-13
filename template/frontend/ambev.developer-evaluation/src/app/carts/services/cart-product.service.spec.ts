import { TestBed } from '@angular/core/testing';
import { CartProductService } from './cart-product.service';
import { ApiService } from '../../core/services/api.service';
import { of } from 'rxjs';
import { CartProduct } from '../models/cart-product.model';

describe('CartProductService', () => {
  let service: CartProductService;
  let apiServiceSpy: jasmine.SpyObj<ApiService>;

  beforeEach(() => {
    apiServiceSpy = jasmine.createSpyObj('ApiService', [
      'get',
      'post',
      'put',
      'delete',
      'getById',
    ]);

    TestBed.configureTestingModule({
      providers: [
        CartProductService,
        { provide: ApiService, useValue: apiServiceSpy },
      ],
    });

    service = TestBed.inject(CartProductService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should add a new cart product', (done) => {
    const newCartProduct: CartProduct = <CartProduct>{ id: 1, productId: 100, quantity: 2 };
    apiServiceSpy.post.and.returnValue(of(newCartProduct));

    service.addCartProduct(newCartProduct).subscribe((cartProduct) => {
      expect(cartProduct).toEqual(newCartProduct);
      done();
    });
  });

  it('should update an existing cart product', (done) => {
    const updatedCartProduct: CartProduct = <CartProduct>{
      id: 1,
      productId: 100,
      quantity: 3,
    };
    apiServiceSpy.put.and.returnValue(of(updatedCartProduct));

    service.updateCartProduct(updatedCartProduct).subscribe((cartProduct) => {
      expect(cartProduct.quantity).toBe(3);
      done();
    });
  });

  it('should delete a cart product', (done) => {
    apiServiceSpy.delete.and.returnValue(of(true));

    service.deleteCartProduct(1).subscribe((result) => {
      expect(result).toBe(true);
      done();
    });
  });

  it('should get cart product by ID', (done) => {
    const mockCartProduct: CartProduct = <CartProduct>{ id: 1, productId: 100, quantity: 2 };
    apiServiceSpy.getById.and.returnValue(of(mockCartProduct));

    service.getCartProductById(1).subscribe((cartProduct) => {
      expect(cartProduct).toEqual(mockCartProduct);
      done();
    });
  });
});
