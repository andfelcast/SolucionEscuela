import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import { StudentRegister } from '../../interfaces/student';
import { GeneralRequest } from '../../interfaces/general';
import { AuthService } from '../../services/auth-service';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-register',
  imports: [MatCardModule,MatFormFieldModule,MatInputModule,MatButtonModule,ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})

export class Register {
  private service = inject(AuthService);
     private router = inject(Router);
     public formBuild = inject(FormBuilder);

     public formRegistro: FormGroup = this.formBuild.group({
          nombres: ['',Validators.required],
          apellidos: ['',Validators.required],
          numDocumento:['', Validators.required],
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
               documentNumber: this.formRegistro.value.numDocumento.toString(),
               address: this.formRegistro.value.direccion,
               city: this.formRegistro.value.ciudad,
               phone: this.formRegistro.value.telefono.toString(),
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
                      Swal.fire({
                        title: "Registro completo",
                        text: "Su nombre de usuario asignado es: " + data.resultData.toString() + ".",
                        icon: "success",
                        timer:3000
                      });
                         this.router.navigate([''])
                    }else{
                         Swal.fire({
                              title: "Registro fallido",
                              text: "No se pudo realizar el registro del estudiante. Favor intente más tarde",
                              icon: "error",
                              timer:3000
                         });
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
