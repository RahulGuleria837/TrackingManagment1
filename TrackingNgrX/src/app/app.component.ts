import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { LoginService } from './login.service';
import { NavigationEnd, Router } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { selectLogins } from './realstate/store/login.selector';
import { Logout, saveNewLoginAPISucess } from './realstate/store/login.action';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit,OnChanges {
title: any;
logginUser:any;
constructor(private store: Store, private router: Router) { }
  login$ = this.store.pipe(select(selectLogins));

  ngOnChanges(changes: SimpleChanges): void {
     var currentUser = localStorage.getItem('currentUser');
     if (currentUser) {
       this.logginUser = JSON.parse(currentUser);} 
  }

  ngOnInit(): void {
    this.login$.subscribe((data) => {
      if (data.data == null) {
        let localData = localStorage.getItem('currentUser');
        if (localData != null) {
          this.store.dispatch(
            saveNewLoginAPISucess({
              newLogin: JSON.parse(localData),
              logout: false,
            })
          );
        }
      }
    });

  }

  LogoutClick() {
    // here we will clear the local storage..
    localStorage.clear();
    this.store.dispatch(Logout({ data: { result: null, logout: true } }));
    this.router.navigate(['login']);
  }
user(){
  var currentUser = localStorage.getItem('currentUser');
    if (currentUser) {
      this.logginUser = JSON.parse(currentUser);}
}

}


