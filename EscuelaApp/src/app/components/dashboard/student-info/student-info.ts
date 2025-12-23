import { ChangeDetectorRef , Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import {MatIconModule} from '@angular/material/icon';
import { Student } from '../../../interfaces/student';
import { StudentService } from '../../../services/student-service';
import { GeneralRequest } from '../../../interfaces/general';
import {MatListModule} from '@angular/material/list';
import { Subject } from '../../../interfaces/subject';
import { SubjectService } from '../../../services/subject-service';
import { Teacher } from '../../../interfaces/teacher';
import { TeacherService } from '../../../services/teacher-service';
import Swal from 'sweetalert2';
import { trustedHTMLFromString } from '@angular/cdk/private';
import { FormsModule, ɵInternalFormsSharedModule } from "@angular/forms";

@Component({
  selector: 'app-student-info',
  imports: [MatCardModule, MatInputModule, MatButtonModule, MatIconModule, MatListModule, ɵInternalFormsSharedModule,FormsModule],
  templateUrl: './student-info.html',
  styleUrl: './student-info.css',  
})
export class StudentInfo {
  currentCredits:number = 0;
  totalCredits:number = 9;
  showSubjects:boolean = false;
  selectedSubjects:number[]=[];
  currentSubjects:number[]=[];
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
  lstSubjects:Subject[] = [];
  lstStudentSubjects:Subject[] = [];
  lstPartners:Student[] = [];
  lstTeachers:Teacher[] = [];
  private service = inject(StudentService);
  private subjectService = inject(SubjectService);
  private teacherService = inject(TeacherService);
  constructor(private cdr: ChangeDetectorRef) {    
    this.LoadTeachers();
    this.LoadSubjects();            
    this.LoadInfo();
  }
  
  LoadInfo(){
  let info:any;
    const request:GeneralRequest = {
      id:localStorage.getItem("userId")!,
      type:'',
      body:null
    };
    this.service.GetById(request).subscribe({
      next:(data) =>{
        if(data.isValid){                 
          this.currentCredits = 0;
          this.user = data.resultData;
          this.lstStudentSubjects = this.user.subjects;
          this.selectedSubjects = [];
          this.currentSubjects = [];

          this.lstStudentSubjects.forEach(element => {
            this.selectedSubjects.push(element.id);
            this.currentSubjects.push(element.id);
            element.teacher = this.lstTeachers.find(x=> x.id == element.id)!;
            this.currentCredits += element.credits;            
          });
          this.cdr.detectChanges();
        }
      }
    });   
  }
  LoadSubjects(){
    this.subjectService.List().subscribe({
      next:(data) =>{
        if(data.isValid){                    
          this.lstSubjects = data.resultData;               
          this.cdr.detectChanges();
        }
      }
    });  
  }
  LoadTeachers(){
    this.teacherService.List().subscribe({
      next:(data) =>{
        if(data.isValid){                           
          this.lstTeachers = data.resultData
          this.cdr.detectChanges();
        }
      }
    });
  }

  Detail(id:number){
    const request:GeneralRequest = {
      id:id.toString(),
      type:'',
      body:null
    };
    this.subjectService.GetById(request).subscribe({
      next:(data) =>{
        if(data.isValid){                              
          let subject = data.resultData;          
          this.lstPartners = subject.students; 
          this.showTable(this.lstPartners,subject.name);         
          this.cdr.detectChanges();
        }
      }
    });                 
  }

  private showTable(partners:Student[],name:string){
    let htmlTable=`<table class="table">
                  <tbody>                  
                  `;
    for(var item of partners){
      htmlTable +=`<tr><td>` + item.firstName + ' ' + item.lastName + `</td></tr>`;
    }
    htmlTable += `</table>`;
    Swal.fire({
      titleText: 'Listado de Estudiantes ' + name, // Use titleText for simple text title
      html: htmlTable, // Insert the HTML string here
      icon: 'info', // Optional: adds an icon (e.g., 'info', 'success', 'warning', 'error')
      confirmButtonText: 'Close'
    });
  }

  CheckSubjects(){
    this.showSubjects = !this.showSubjects;
    this.selectedSubjects = this.currentSubjects;
  }

  onSelectionChange(event: any) {
    if(this.selectedSubjects.length > 3){
      this.WarnPopup('Se ha sobrepasado el número máximo de créditos(' + this.totalCredits + ')');
      this.selectedSubjects = this.currentSubjects;
      return;
    }
    if(this.selectedSubjects.length == 0){
      this.WarnPopup('No ha inscrito materias aún');
      this.selectedSubjects = this.currentSubjects;
      return;
    }
    let lstCurTeachers:number[] = [];
    this.selectedSubjects.forEach(element => {
      lstCurTeachers.push(this.lstSubjects.find(x => x.id == element)?.teacherId!);
    });
    for(let i = 0; i < lstCurTeachers.length;i++){
      if(lstCurTeachers.filter(item => item === i).length > 1){
        this.WarnPopup('Ha inscrito dos materias a cargo del mismo profesor. Favor ajustar la selección');
        this.selectedSubjects = this.currentSubjects;
        i = lstCurTeachers.length;
        return;
      }
    }
  }

  SaveSubjects(){
    const request: GeneralRequest = {
        id:localStorage.getItem("userId")!,
        type:"",
        body:this.selectedSubjects
    }
    this.service.AddSubjects(request).subscribe({
        next: (data) =>{
            if(data.isValid){
              this.SuccessPopup("Se realizó la inscripción correcta de asignaturas");
              setTimeout(() => {
                window.location.reload();
              }, 5000); // 5000ms delay
            }else{
              this.ErrorPopup("No se pudo realizar la asociación de asignaturas");                  
            }
        }, error:(error) =>{
            console.log(error.message);
        }
    })
  }

  private WarnPopup(message:string){
    Swal.fire({
                title: "Atención",
                text: message,
                icon: "warning",
                timer:3000
            });    
  }
  private SuccessPopup(message:string){
    Swal.fire({
                title: "Proceso exitoso",
                text: message,
                icon: "success",
                timer:3000
            });    
  }
  private ErrorPopup(message:string){
    Swal.fire({
                title: "Error",
                text: message,
                icon: "error",
                timer:3000
            });    
  }  
}
