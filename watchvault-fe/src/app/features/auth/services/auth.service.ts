import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Register } from '../models/register.model';
import { Observable } from 'rxjs';
import { LoginResponse } from '../models/login-response.model';
import { Login } from '../models/login-request.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = environment.apiUrl + '/auth';

  constructor(private http: HttpClient) {}

  register(register: Register): Observable<void> {
    const formData = new FormData();

    formData.append('firstName', register.firstName);
    formData.append('lastName', register.lastName);
    formData.append('username', register.username);
    formData.append('email', register.email);
    formData.append('password', register.password);

    if (register.file) {
      formData.append('file', register.file);
    }

    return this.http.post<void>(this.apiUrl + '/register', formData);
  }

  login(login: Login): Observable<LoginResponse> {
    const params = new HttpParams().set('username', login.username).set('password', login.password);

    return this.http.get<LoginResponse>(this.apiUrl + '/retrieve-token', { params });
  }
}
