import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Realstate } from './store/realstate';
import { Observable } from 'rxjs';
import { LoginService } from '../login.service';

@Injectable({
  providedIn: 'root'
})
export class RealstateService {

  constructor(private http:HttpClient,private loginService:LoginService) { }

  get():Observable<any>{
    return this.http.get<any[]>('http://localhost:5046/getspecificdata')
  }
  Create(payload:Realstate)
  {
    return this.http.post<Realstate>('http://localhost:5046/createState',payload);
  }

  update(payload:Realstate){
  
    return this.http.put<Realstate>('http://localhost:5046/updateState',payload)

  }
  delete(id:number){

    return this.http.delete(`http://localhost:5046/delete/${id}`);
  }
  login(data:any):Observable<any>{
   
      return this.http.post<any>('http://localhost:5046/minimalAPI/login',data);
    }

  showSpecificTrackingData(id:number,applicationUserId:string){
    debugger
         return this.http.get<any>(`http://localhost:5046/minimalAPI/trackingDetails?userId=${applicationUserId}&id=${id}`)
  }

}
