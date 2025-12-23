import { Component, inject, Inject, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { Router } from '@angular/router';
import  { FormBuilder, FormGroup, Validators, ReactiveFormsModule} from '@angular/forms';
import { LoginReg } from '../../interfaces/login';
import { GeneralRequest } from '../../interfaces/general';
import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';

@Component({
  selector: 'app-login',
  imports: [MatCardModule,MatFormFieldModule,MatInputModule,MatButtonModule,ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {  
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

    const request:GeneralRequest = {
      id:"",
      type:"",
      body:objLogin
    };

    this.authService.Login(request).subscribe({
      next:(data) =>{
        if(data.isValid){
          localStorage.setItem("token",data.resultData.token);
          this.router.navigateByUrl("/home")
        }
        else{
          alert('Credenciales incorrectas')
        }
      },
      error:(error) => {
        console.log(error.message);
      }
    });
  }
  RegisterNew(){
    debugger;
    this.router.navigateByUrl("/register");
  }

}
