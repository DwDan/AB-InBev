import { TestBed } from '@angular/core/testing';
import { CartService } from './cart.service';
import { ApiService } from '../../core/services/api.service';
import { of } from 'rxjs';
import { Cart } from '../models/cart.model';
import { PaginatedResponse } from '../../core/shared/models/paginated-response.model';

describe('CartService', () => {
  let service: CartService;
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
        CartService,
        { provide: ApiService, useValue: apiServiceSpy },
      ],
    });

    service = TestBed.inject(CartService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch all carts', (done) => {
    const mockCarts: Cart[] = [
      <Cart>{ id: 1, userId: 100, products: [{ productId: 200, quantity: 1 }] },
      <Cart>{ id: 2, userId: 100, products: [{ productId: 200, quantity: 1 }] },
    ];
    const mockResponse: PaginatedResponse<Cart> = {
      data: mockCarts,
      totalItems: 2,
      currentPage: 1,
      totalPages: 1,
    };

    apiServiceSpy.get.and.returnValue(of(mockResponse));

    service.getCarts().subscribe((carts) => {
      expect(carts.length).toBe(2);
      expect(carts).toEqual(mockCarts);
      done();
    });
  });

  it('should add a new cart', (done) => {
    const newCart: Cart = <Cart>{
      id: 1,
      userId: 100,
      products: [{ productId: 200, quantity: 1 }],
    };
    apiServiceSpy.post.and.returnValue(of(newCart));

    service.addCart(newCart).subscribe((cart) => {
      expect(cart).toEqual(newCart);
      done();
    });
  });

  it('should update an existing cart', (done) => {
    const updatedCart: Cart = <Cart>{
      id: 1,
      userId: 100,
      products: [{ productId: 200, quantity: 1 }],
    };
    apiServiceSpy.put.and.returnValue(of(updatedCart));

    service.updateCart(updatedCart).subscribe((cart) => {
      expect(cart.products.length).toBe(1);
      done();
    });
  });

  it('should delete a cart', (done) => {
    apiServiceSpy.delete.and.returnValue(of(true));

    service.deleteCart(1).subscribe((result) => {
      expect(result).toBe(true);
      done();
    });
  });

  it('should get cart by ID', (done) => {
    const mockCart: Cart = <Cart>{
      id: 1,
      userId: 100,
      products: [{ productId: 200, quantity: 1 }],
    };
    apiServiceSpy.getById.and.returnValue(of(mockCart));

    service.getCartById(1).subscribe((cart) => {
      expect(cart).toEqual(mockCart);
      done();
    });
  });

  it('should get active cart', (done) => {
    const activeCart: Cart = <Cart>{
      id: 1,
      userId: 100,
      products: [{ productId: 200, quantity: 1 }],
    };
    apiServiceSpy.get.and.returnValue(of(activeCart));

    service.getActiveCartBy().subscribe((cart) => {
      expect(cart).toEqual(activeCart);
      done();
    });
  });
});
