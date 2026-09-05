import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { AuthService } from '../login/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, ButtonModule, CheckboxModule, InputTextModule, PasswordModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
    loginForm: FormGroup;
  errorMessage = '';
  email = '';
  password = '';
  rememberMe = false;
  submitted = false;

  constructor( private fb: FormBuilder,
    private authService: AuthService,
    private router: Router) {
        this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      rememberMe: [false]
    });
    }

     onSubmit(): void {
    if (this.loginForm.invalid) return;

    const { username, password } = this.loginForm.value;
    this.authService.login(username, password).subscribe({
      next: () => this.router.navigate(['/orders']),
      error: () => this.errorMessage = 'Invalid username or password.'
    });
  }
//   onSubmit(): void {
//     this.submitted = true;

//     if (this.email.trim() && this.password.trim()) {
//       this.router.navigate(['/orders']);
//     }
//   }
}