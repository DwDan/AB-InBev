import {
  ComponentFixture,
  TestBed,
  fakeAsync,
  tick,
} from '@angular/core/testing';
import { ProductListComponent } from './product-list.component';
import { ProductService } from '../../services/product.service';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { Product } from '../../models/product.model';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('ProductListComponent', () => {
  let component: ProductListComponent;
  let fixture: ComponentFixture<ProductListComponent>;
  let productServiceSpy: jasmine.SpyObj<ProductService>;
  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    productServiceSpy = jasmine.createSpyObj('ProductService', [
      'getProducts',
      'deleteProduct',
    ]);

    productServiceSpy.getProducts.and.returnValue(of([
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
    ]));

    routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    await TestBed.configureTestingModule({
      imports: [ProductListComponent, BrowserAnimationsModule],
      providers: [
        { provide: ProductService, useValue: productServiceSpy },
        { provide: Router, useValue: routerSpy },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ProductListComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load products on init', fakeAsync(() => {
    const mockProducts: Product[] = [
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
    ];

    productServiceSpy.getProducts.and.returnValue(of(mockProducts));

    component.ngOnInit();
    tick();
    fixture.detectChanges();

    expect(component.products.length).toBe(2);
    expect(component.products).toEqual(mockProducts);
  }));

  it('should navigate to add product page', () => {
    component.addProduct();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['products/add']);
  });

  it('should navigate to edit product page', () => {
    component.editProduct(1);
    expect(routerSpy.navigate).toHaveBeenCalledWith(['products/edit', 1]);
  });

  it('should delete product after confirmation', fakeAsync(() => {
    spyOn(window, 'confirm').and.returnValue(true);
    productServiceSpy.deleteProduct.and.returnValue(of(true));

    component.deleteProduct(1);
    tick();

    expect(productServiceSpy.deleteProduct).toHaveBeenCalledWith(1);
  }));

  it('should not delete product if user cancels confirmation', fakeAsync(() => {
    spyOn(window, 'confirm').and.returnValue(false);

    component.deleteProduct(1);
    tick();

    expect(productServiceSpy.deleteProduct).not.toHaveBeenCalled();
  }));

  it('should navigate back to home', () => {
    component.goToHome();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/home']);
  });
});
