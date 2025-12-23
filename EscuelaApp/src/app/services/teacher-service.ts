import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { GeneralResponse, GeneralRequest } from '../interfaces/general';

@Injectable({
  providedIn: 'root',
})
export class TeacherService {
  private http = inject(HttpClient);
  private apiUrl = environment.API_URL;
  
  constructor(){}

  List(): Observable<GeneralResponse>{
    return this.http.get<GeneralResponse>(this.apiUrl + 'Teacher/GetAll');
  }

  GetById(request: GeneralRequest): Observable<GeneralResponse>{
    return this.http.post<GeneralResponse>(this.apiUrl + 'Teacher/GetById',request);
  }

  Insert(request: GeneralRequest): Observable<GeneralResponse>{
    return this.http.post<GeneralResponse>(this.apiUrl + 'Teacher/Insert',request);
  }

  Update(request: GeneralRequest): Observable<GeneralResponse>{
    return this.http.post<GeneralResponse>(this.apiUrl + 'Teacher/Update',request);
  }

  Delete(request: GeneralRequest): Observable<GeneralResponse>{
    return this.http.post<GeneralResponse>(this.apiUrl + 'Teacher/Delete',request);
  }
}
