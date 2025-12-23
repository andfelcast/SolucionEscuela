import { ChangeDetectorRef , Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { Student } from '../../../interfaces/student';
import { StudentService } from '../../../services/student-service';
import { GeneralRequest } from '../../../interfaces/general';
import { firstValueFrom } from 'rxjs';
import { AnonymousSubject } from 'rxjs/internal/Subject';

@Component({
  selector: 'app-student-info',
  imports: [MatCardModule,MatInputModule,MatButtonModule],
  templateUrl: './student-info.html',
  styleUrl: './student-info.css',  
})
export class StudentInfo {
  user:Student = {
    active : false,
    address:'',
    lastName:'',
    documentNumber:'',
    email:'',
    password:'',
    id:0,
    firstName:'',
    city:'',
    phone:'',
    subjects:[],
    userName:'',
    credits:0,
    creationDate:new Date(),
  };
  private service = inject(StudentService);
  constructor(private cdr: ChangeDetectorRef) {
    let info:any;
    const request:GeneralRequest = {
      id:localStorage.getItem("userId")!,
      type:'',
      body:null
    };
    this.service.GetById(request).subscribe({
      next:(data) =>{
        if(data.isValid){          
          this.user = data.resultData;
          this.cdr.detectChanges();
        }
      }
    });        
  }
  
  async LoadInfo(){
    let data = null;
    const request:GeneralRequest = {
      id:localStorage.getItem("userId")!,
      type:'',
      body:null
    };
    // this.service.GetById(request).subscribe({
    //   next:(data) =>{
    //     if(data.isValid){
    //       this.user = data.resultData
    //     }
    //   }
    // });
    data = await firstValueFrom(this.service.GetById(request));
    this.user = data.resultData;
  }
  
}
