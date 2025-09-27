import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Register } from '../../shared/models/register';
import { Observable } from 'rxjs';
import { LoginResponse } from '../../shared/models/loginResponse';
import { Login } from '../../shared/models/login';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = environment.apiUrl + '/auth';

  constructor(private http: HttpClient) {}

  register(register: Register): Observable<void> {
    return this.http.post<void>(this.apiUrl + '/register', register);
  }

  login(login: Login): Observable<LoginResponse> {
    const params = new HttpParams().set('username', login.username).set('password', login.password);

    return this.http.get<LoginResponse>(this.apiUrl + '/retrieve-token', { params });
  }
}
