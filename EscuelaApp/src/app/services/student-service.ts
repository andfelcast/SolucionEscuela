import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Student } from '../interfaces/student';
import { GeneralRequest, GeneralResponse } from '../interfaces/general';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class StudentService {
  private http = inject(HttpClient);
  private apiUrl = environment.API_URL;
  
  constructor(){}

  List(): Observable<GeneralResponse>{
    return this.http.get<GeneralResponse>(this.apiUrl + 'Student/GetAll');
  }

  GetById(request: GeneralRequest): Observable<GeneralResponse>{
    return this.http.post<GeneralResponse>(this.apiUrl + 'Student/GetById',request);
  }
  
  Update(request: GeneralRequest): Observable<GeneralResponse>{
    return this.http.post<GeneralResponse>(this.apiUrl + 'Student/Update',request);
  }

  Delete(request: GeneralRequest): Observable<GeneralResponse>{
    return this.http.post<GeneralResponse>(this.apiUrl + 'Student/Delete',request);
  }
}
