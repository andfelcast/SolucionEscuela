import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { StudentService } from '../../services/student-service';
import { Router } from 'express';
import { StudentRegister } from '../../interfaces/student';
import { GeneralRequest } from '../../interfaces/general';


@Component({
  selector: 'app-register',
  imports: [MatCardModule,MatFormFieldModule,MatInputModule,MatButtonModule,ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})

export class Register {
  private service = inject(StudentService);
     private router = inject(Router);
     public formBuild = inject(FormBuilder);

     public formRegistro: FormGroup = this.formBuild.group({
          nombres: ['',Validators.required],
          apellidos: ['',Validators.required],
          fecNacimiento:['', Validators.required],
          direccion:['', Validators.required],
          ciudad:['', Validators.required],
          telefono:['', Validators.required],
          correo: ['',Validators.required],
          clave: ['',Validators.required]
     })

     Register(){
          if(this.formRegistro.invalid) return;

          const objRegister:StudentRegister = {
               firstName: this.formRegistro.value.nombres,
               lastName: this.formRegistro.value.apellidos,
               docNumber: this.formRegistro.value.numDocumento,
               address: this.formRegistro.value.direccion,
               city: this.formRegistro.value.ciudad,
               phone: this.formRegistro.value.telefono,
               email: this.formRegistro.value.correo,
               password: this.formRegistro.value.clave,                             
          }
          const request: GeneralRequest = {
            id:"",
            type:"",
            body:objRegister
          }
          this.service.Register(request).subscribe({
               next: (data) =>{
                    if(data.isValid){
                         this.router.navigate([''])
                    }else{
                         alert("No se pudo registrar")
                    }
               }, error:(error) =>{
                    console.log(error.message);
               }
          })

     }

     Back(){
          this.router.navigate([''])
     }
}
