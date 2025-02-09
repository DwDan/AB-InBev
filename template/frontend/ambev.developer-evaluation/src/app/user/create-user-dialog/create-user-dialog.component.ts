import { Component } from '@angular/core';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { UserService } from '../service/user.service';
import { GeolocationService } from '../service/geolocation.service';
import { User } from '../models/user.model';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-create-user-dialog',
  templateUrl: './create-user-dialog.component.html',
  styleUrls: ['./create-user-dialog.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatButtonModule
  ],
})
export class CreateUserDialogComponent {
  userForm: FormGroup;
  isLoading = false;

  constructor(
    private dialogRef: MatDialogRef<CreateUserDialogComponent>,
    private userService: UserService,
    private geolocationService: GeolocationService,
    private fb: FormBuilder
  ) {
    this.userForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      phone: ['', Validators.required],
      city: ['', Validators.required],
      street: ['', Validators.required],
      number: [null, Validators.required],
      zipcode: ['', Validators.required],
      latitude: ['', Validators.required],
      longitude: ['', Validators.required],
      status: [0, Validators.required],
      role: [0, Validators.required],
    });
  }

  ngOnInit(): void {
    this.getLocation();
  }

  getLocation(): void {
    this.geolocationService.getCurrentLocation().subscribe({
      next: (location) => {
        this.userForm.patchValue({
          latitude: location.latitude.toString(),
          longitude: location.longitude.toString(),
        });
      },
      error: () => {
        alert('Não foi possível obter a localização.');
      },
    });
  }

  save(): void {
    if (this.userForm.valid) {
      const formValues = this.userForm.value;
      const newUser = {
        id: 0,
        username: formValues.username,
        password: formValues.password,
        email: formValues.email,
        name: {
          firstname: formValues.firstname,
          lastname: formValues.lastname,
        },
        address: {
          city: formValues.city,
          street: formValues.street,
          number: formValues.number,
          zipcode: formValues.zipcode,
          geolocation: {
            latitude: formValues.latitude,
            longitude: formValues.longitude,
          },
        },
        phone: formValues.phone,
        status: 1,
        role: 4,
      };

      this.createUser(newUser);
    }
  }

  createUser(user: any) {
    this.isLoading = true;

    this.userService.createUser(user).subscribe({
      next: () => {
        alert('Usuário criado com sucesso!');
        this.dialogRef.close();
        this.isLoading = false;
      },
      error: (error) => {
        alert('Erro ao criar usuário: ' + error.message);
        this.isLoading = false;
      },
    });
  }

  close(): void {
    this.dialogRef.close();
  }
}
