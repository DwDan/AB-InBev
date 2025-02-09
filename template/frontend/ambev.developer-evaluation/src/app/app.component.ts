import { Component } from '@angular/core';
import { LoginComponent } from './login/login.component';

@Component({
  selector: 'app-root',
  template: '<app-login></app-login>',
  standalone: true,
  imports: [LoginComponent],
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ambev.developer-evaluation';
}
