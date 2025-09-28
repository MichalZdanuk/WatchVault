import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../../../core/services/auth.service';
import { Router, RouterLink } from '@angular/router';
import { Login } from '../../../../shared/models/login';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthStateService } from '../../../../core/auth/AuthStateService';

@Component({
  selector: 'app-login.component',
  imports: [ReactiveFormsModule, CommonModule, RouterLink, MatIconModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup = new FormGroup({});
  hidePassword: boolean = true;

  constructor(
    private authService: AuthService,
    private authState: AuthStateService,
    private formBuilder: FormBuilder,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.loginForm.invalid) return;

    let login: Login = this.loginForm.value;

    this.authService.login(login).subscribe({
      next: (res) => {
        this.authState.login(res.accessToken);
        this.router.navigate(['/movies/trending']);
      },
      error: () => {
        this.snackBar.open('Invalid login credentials', '', {
          panelClass: ['error-snackbar'],
          duration: 3000,
          horizontalPosition: 'center',
          verticalPosition: 'bottom',
        });
      },
    });
  }

  togglePassword(): void {
    this.hidePassword = !this.hidePassword;
  }
}
