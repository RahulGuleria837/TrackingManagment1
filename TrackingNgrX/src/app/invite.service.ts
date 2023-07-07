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
    console.log(email)
    alert(email);
      return this.http.post<any>(`http://localhost:5046/minimalAPI/CreateInvitation?senderId=${email}`,"")
  }

  showInvitedUsers(){
    return this.http.get<any>('http://localhost:5046/minimalAPI/Getall')
  }

  changeAction(receiverid:any,action:any){
    debugger
    return this.http.post<any>(`http://localhost:5046/minimalAPI/update?reciverId=${receiverid}&action=${action}`,"")
  }
}
