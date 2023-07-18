import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { LoginService } from '../login.service';
import { compileNgModule } from '@angular/compiler';
import { Appstate } from '../shared/store/appstate';
import { Store, select } from '@ngrx/store';
import { invokeSaveNewLoginAPI } from '../realstate/store/login.action';
import { selectAppState } from '../shared/store/app.selector';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

 

  currentUser:any;
  loginForm!:FormGroup;
  apiStatus$: Observable<any>;
 
  
    constructor(private builder: FormBuilder,
      private loginservice:LoginService,
       private route:Router ,
       private appstore: Store<Appstate>,
       private stores: Store) { this.apiStatus$=new Observable}
  //private store: Store, private router: Router, private appstore: Store<Appstate>, private route: ActivatedRoute)
    ngOnInit(): void {
      this.loginForm = this.builder.group({
        UserName: ['',Validators.required],
        Password:['',Validators.required]
      })
    }
  
    loginClick() {
      debugger
      this.stores.dispatch(invokeSaveNewLoginAPI({newLogin:this.loginForm.value}));
    this.apiStatus$ = this.appstore.pipe(select(selectAppState));
    this.apiStatus$.subscribe((apState) => {
      if (apState.apiStatus == 'success') {
        alert(apState.apiResponseMessage)
        this.route.navigate(['realstate']);
      }
      this.route.navigate(['realstate']);
    });
    }


  }
