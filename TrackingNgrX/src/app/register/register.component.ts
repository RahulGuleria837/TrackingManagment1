import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginService } from '../login.service';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
register:any
  loginForm!:FormGroup;

constructor(private builder: FormBuilder,private http:HttpClient,private registerservice:LoginService,private route:Router){}

ngOnInit(): void {
  this.loginForm = this.builder.group({
    UserName: ['',Validators.required],
    Password:['',Validators.required],
    emai:['']
  })
}

registerClick() {
  this.register = this.loginForm.value
  
  this.registerservice.register(this.register).subscribe({
    next:(data)=>{
      console.log(data)
    this.route.navigate(['/realstate']);
  }
  })
 }
}
