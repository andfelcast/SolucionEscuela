import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { GeneralResponse, GeneralRequest } from '../interfaces/general';
import { LoginReg } from '../interfaces/login';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private http = inject(HttpClient);
  private apiUrl = environment.API_URL;
  
  constructor(){}
  
  Login(request: LoginReg): Observable<GeneralResponse>{
    return this.http.post<GeneralResponse>(this.apiUrl + 'Auth/Login',request);
  }
  Register(request: GeneralRequest): Observable<GeneralResponse>{
    debugger;
    return this.http.post<GeneralResponse>(this.apiUrl + 'Auth/Register',request);
  }
  ValidateToken(token:string): Observable<GeneralResponse>{
    return this.http.get<GeneralResponse>(this.apiUrl + 'Auth/ValidateToken/' + token);
  }  
}
