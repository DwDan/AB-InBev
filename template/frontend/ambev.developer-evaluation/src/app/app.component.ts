import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-root',
  template: '<router-outlet></router-outlet>',
  standalone: true,
  imports: [RouterModule],
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ambev.developer-evaluation';
}
