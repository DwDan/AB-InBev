import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MaterialModule } from '../core/shared/modules/material.module';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule, MaterialModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent {}
