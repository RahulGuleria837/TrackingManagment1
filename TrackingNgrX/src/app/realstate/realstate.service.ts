import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Realstate } from './store/realstate';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RealstateService {

  constructor(private http:HttpClient) { }

  get():Observable<any>{
    return this.http.get<any[]>('http://localhost:5046/GetAll')
  }
  Create(payload:Realstate)
  {
    return this.http.post<Realstate>('http://localhost:5046/createState',payload);
  }

  update(payload:Realstate){
    return this.http.put<Realstate>('http://localhost:5046/udateState/${payload.id}',payload)

  }
}
