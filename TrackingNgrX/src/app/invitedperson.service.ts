import { HttpBackend, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InvitedpersonService {

  constructor(private http:HttpClient) { }
 
  Status(reciverid:any,status:any){
    debugger
      return this.http.post(`http://localhost:5046/minimalAPI/ChangeStatus/${reciverid}/${status}`,"");
  }

    invitedPersonData(){
      return this.http.get<any>('http://localhost:5046/minimalAPI/invitationComesFromUser')
    }

    specificUserData(id:any){
      debugger
      return this.http.get(`http://localhost:5046/getuserdatas/${id}`)
    } 
  
    }

