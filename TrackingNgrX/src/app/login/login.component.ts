import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { LoginService } from '../login.service';
import { compileNgModule } from '@angular/compiler';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  currentUser:any;
  loginForm!:FormGroup;
  
    constructor(private builder: FormBuilder, private loginservice: LoginService,private route:Router) { }
  
    ngOnInit(): void {
      this.loginForm = this.builder.group({
        UserName: ['',Validators.required],
        Password:['',Validators.required]
      })
    }

  
    loginClick() {
      debugger
      console.log(this.loginForm.value)
      this.loginservice.login(this.loginForm.value).subscribe({
          next:(rr)=>{
            this.currentUser=rr.UserName;
            localStorage["currentUser"]=JSON.stringify(rr);
           this.route.navigate(['realstate']);
           this.route.events.subscribe(event => {
           this.loginForm.reset();
          });
          
            // console.log(rr)
          },
          error:(err)=>{
             console.log(err);
          }
          
      })
    }
}