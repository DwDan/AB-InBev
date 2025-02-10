import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { ProductListComponent } from './products/components/product-list/product-list.component';
import { ProductFormComponent } from './products/components/product-form/product-form.component';
import { UserListComponent } from './users/component/user-list/user-list.component';
import { UserFormComponent } from './users/component/user-form/user-form.component';
import { BranchListComponent } from './branchs/components/branch-list/branch-list.component';
import { BranchFormComponent } from './branchs/components/branch-form/branch-form.component';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'products',
    component: ProductListComponent,
  },
  { path: 'products/edit/:id', component: ProductFormComponent },
  { path: 'products/add', component: ProductFormComponent },
  {
    path: 'users',
    component: UserListComponent,
  },
  { path: 'users/edit/:id', component: UserFormComponent },
  { path: 'users/add', component: UserFormComponent },
  {
    path: 'branches',
    component: BranchListComponent,
  },
  { path: 'branches/edit/:id', component: BranchFormComponent },
  { path: 'branches/add', component: BranchFormComponent },
];
