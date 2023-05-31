import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AnyCatcher } from 'rxjs/internal/AnyCatcher';

@Injectable({
  providedIn: 'root'
})
export class InviteService {

  constructor(private http:HttpClient) { }


  Invite(displayName:any):Observable<any>{
    debugger
  
    return this.http.get<any>(`http://localhost:5046/minimalAPI/invite/${displayName}`)

  }
  SendEmail(email:any){
    debugger
      return this.http.post<any>(`http://localhost:5046/minimalAPI/email`,email)
  }
}