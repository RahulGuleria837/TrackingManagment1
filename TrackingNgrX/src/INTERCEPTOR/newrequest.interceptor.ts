import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class NewrequestInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    var currentUser = {token:""};
    var getcurrentUser = localStorage.getItem('currentUser');
   // console.log(getcurrentUser);
    if(getcurrentUser !=null){
      currentUser.token = JSON.parse(getcurrentUser).token;
      
    }
    request = request.clone({
      setHeaders:{

        Authorization:"Bearer "+currentUser.token
      }
    
    });
    console.log(request);
    
    return next.handle(request);
  }
}
