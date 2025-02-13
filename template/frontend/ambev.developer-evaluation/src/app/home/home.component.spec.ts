import {
  ComponentFixture,
  TestBed,
  fakeAsync,
  tick,
} from '@angular/core/testing';
import { HomeComponent } from './home.component';
import { ProductService } from '../products/services/product.service';
import { CartService } from '../carts/services/cart.service';
import { of } from 'rxjs';
import { Product } from '../products/models/product.model';
import { Cart, instanceCart } from '../carts/models/cart.model';
import { CartProduct } from '../carts/models/cart-product.model';
import { ActivatedRoute } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;
  let productServiceSpy: jasmine.SpyObj<ProductService>;
  let cartServiceSpy: jasmine.SpyObj<CartService>;

  beforeEach(async () => {
    productServiceSpy = jasmine.createSpyObj('ProductService', ['getProducts']);

    productServiceSpy.getProducts.and.returnValue(
      of([
        <Product>{
          id: 1,
          title: 'Product A',
          price: 100,
          description: 'Desc A',
          image: '',
          rating: { rate: 4, count: 10 },
        },
        <Product>{
          id: 2,
          title: 'Product B',
          price: 200,
          description: 'Desc B',
          image: '',
          rating: { rate: 4.5, count: 20 },
        },
      ])
    );

    cartServiceSpy = jasmine.createSpyObj('CartService', [
      'getActiveCartBy',
      'updateCart',
    ]);

    cartServiceSpy.getActiveCartBy.and.returnValue(
      of({
        ...instanceCart(),
        id: 1,
        products: [{ productId: 1, quantity: 2 } as CartProduct],
      })
    );

    await TestBed.configureTestingModule({
      imports: [HomeComponent, BrowserAnimationsModule],
      providers: [
        { provide: ProductService, useValue: productServiceSpy },
        { provide: CartService, useValue: cartServiceSpy },
        {
          provide: ActivatedRoute,
          useValue: { params: of({ id: 1 }) },
        },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should add product to cart when confirmed', (done) => {
    spyOn(window, 'confirm').and.returnValue(true);

    const mockCart: Cart = {
      ...instanceCart(),
      id: 1,
      products: [
        { productId: 1, quantity: 2 } as CartProduct,
        { productId: 2, quantity: 2 } as CartProduct,
      ],
    };

    component.cart = mockCart;
    cartServiceSpy.updateCart.and.returnValue(of(mockCart));

    component.openCartDialog(2);

    cartServiceSpy.updateCart(mockCart).subscribe(() => {
      expect(component.cart.products.length).toBe(2);
      expect(cartServiceSpy.updateCart).toHaveBeenCalled();
      done();
    });
  });

  it('should not add product to cart when canceled', fakeAsync(() => {
    spyOn(window, 'confirm').and.returnValue(false);

    component.openCartDialog(2);
    tick();
    fixture.detectChanges();

    expect(cartServiceSpy.updateCart).not.toHaveBeenCalled();
  }));
});
