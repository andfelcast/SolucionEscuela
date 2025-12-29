import { Component, inject, Inject, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { Router } from '@angular/router';
import  { FormBuilder, FormGroup, Validators, ReactiveFormsModule} from '@angular/forms';
import { LoginReg } from '../../interfaces/login';
import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  imports: [MatCardModule,MatFormFieldModule,MatInputModule,MatButtonModule,ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login implements OnInit{
  ngOnInit(): void {
    if (localStorage.getItem('token')) {
      localStorage.clear();  
    }
  }  
  private authService = inject(AuthService);
  private router = inject(Router);
  public formBuild = inject(FormBuilder);

  public formLogin: FormGroup = this.formBuild.group({
    userName:["", Validators.required],
    pass:["", Validators.required]
  });

  
  StartSession(){
    if(this.formLogin.invalid) return;

    const objLogin:LoginReg = {
      userName: this.formLogin.value.userName,
      password: this.formLogin.value.pass
    }    

    this.authService.Login(objLogin).subscribe({
      next:(data) =>{
        if(data.isValid){
          Swal.fire({
                title: "Bienvenido",                
                icon: "success",
                timer:3000
          });
          localStorage.setItem("token",data.resultData.token);          
          localStorage.setItem("userId",data.resultData.userId);
          localStorage.setItem("userName",data.resultData.userName);
          this.router.navigate(["home"])
        }
        else{
            Swal.fire({
                title: "Error",
                text: "Credenciales incorrectas. Favor intente de nuevo",
                icon: "error",
                timer:3000
            });          
            this.Reset();
        }
      },
      error:(error) => {
        Swal.fire({
                title: "Error",
                text: "Credenciales incorrectas. Favor intente de nuevo",
                icon: "error",
                timer:3000
            });
        this.Reset();
      }
    });
  }
  RegisterNew(){
    debugger;
    this.router.navigate(["register"]);
  }
  private Reset(){    
    this.formLogin.patchValue({userName:''})
    this.formLogin.patchValue({pass:''})
  }

}
