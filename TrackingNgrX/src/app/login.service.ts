import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {


  private BaseUrl = "https://localhost:44365/minimalAPI/login";

  constructor(private httpclient:HttpClient,private router:Router) { }
  
  login(login:any):Observable<any>{
  debugger
    return this.httpclient.post<any>('http://localhost:5046/minimalAPI/login',login);
  }
  register(register:any):Observable<any>{
   return this.httpclient.post<any>('http://localhost:5046/minimalAPI/register',register)
  }
}
