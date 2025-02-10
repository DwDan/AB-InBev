import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  ReactiveFormsModule,
  FormsModule,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../../../core/shared/modules/material.module';
import { User } from '../../models/user.model';
import { UserService } from '../../services/user.service';
import { GeolocationService } from '../../services/geolocation.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [CommonModule, MaterialModule, FormsModule, ReactiveFormsModule],
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.scss'],
})
export class UserFormComponent implements OnInit {
  userForm!: FormGroup;
  isEditMode = false;
  userId!: number;
  user?: User;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private geolocationService: GeolocationService,
    private route: ActivatedRoute,
    private location: Location
  ) {}

  ngOnInit() {
    this.userForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      name: this.fb.group({
        firstname: ['', Validators.required],
        lastname: ['', Validators.required],
      }),
      phone: ['', Validators.required],
      status: [0, Validators.required],
      role: [0, Validators.required],
      address: this.fb.group({
        city: ['', Validators.required],
        street: ['', Validators.required],
        number: [null, Validators.required],
        zipcode: ['', Validators.required],
        geolocation: this.fb.group({
          latitude: ['', Validators.required],
          longitude: ['', Validators.required],
        }),
      }),
    });

    this.route.params.subscribe((params) => {
      if (params['id']) {
        this.isEditMode = true;
        this.userId = +params['id'];
        this.userService.getUserById(this.userId).subscribe((user) => {
          if (user) {
            this.user = user;
            this.userForm.patchValue({
              ...user,
            });
          }
        });
      } else {
        this.userForm.patchValue({
          role: 4,
          status: 1,
        });

        this.getLocation();
      }
    });
  }

  onSubmit() {
    if (this.userForm.valid) {
      const formValue = this.userForm.value;

      if (this.isEditMode && this.user) {
        this.userService
          .updateUser({
            id: this.user.id,
            ...formValue,
          })
          .subscribe(() => {
            this.location.back();
          });
      } else {
        this.userService.addUser(formValue).subscribe(() => {
          this.location.back();
        });
      }
    }
  }

  onCancel() {
    this.location.back();
  }

  getLocation(): void {
    this.geolocationService.getCurrentLocation().subscribe({
      next: (location) => {
        this.userForm.patchValue({
          address: {
            geolocation: {
              latitude: location.latitude.toString(),
              longitude: location.longitude.toString(),
            },
          },
        });
      },
      error: () => {
        alert('Não foi possível obter a localização.');
      },
    });
  }
}
