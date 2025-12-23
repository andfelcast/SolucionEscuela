import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { GeneralResponse, GeneralRequest } from '../interfaces/general';

@Injectable({
  providedIn: 'root',
})
export class SubjectService {
  private http = inject(HttpClient);
  private apiUrl = environment.API_URL;
  
  constructor(){}

  List(): Observable<GeneralResponse>{
    return this.http.get<GeneralResponse>(this.apiUrl + 'Subject/GetAll');
  }

  GetById(request: GeneralRequest): Observable<GeneralResponse>{
    return this.http.post<GeneralResponse>(this.apiUrl + 'Subject/GetById',request);
  }

  Register(request: GeneralRequest): Observable<GeneralResponse>{
    return this.http.post<GeneralResponse>(this.apiUrl + 'Subject/Register',request);
  }

  Update(request: GeneralRequest): Observable<GeneralResponse>{
    return this.http.post<GeneralResponse>(this.apiUrl + 'Subject/Update',request);
  }

  Delete(request: GeneralRequest): Observable<GeneralResponse>{
    return this.http.post<GeneralResponse>(this.apiUrl + 'Subject/Delete',request);
  }
}
