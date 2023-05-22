import { Component, OnInit } from '@angular/core';
import { LoginService } from './login.service';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'TrackingNgrX';
  logoutUser:boolean=true;
Logout:boolean=false;

  currentUser:any;


constructor(public loginservice:LoginService,private route:Router){

}


ngOnInit(): void {
  var currentUser= JSON.parse(localStorage.getItem("currentUser")?? "")
  if(currentUser!=""){
    this.logoutUser=false;
  }

}

logOutClick(){
  debugger
  this.Logout=true;
  this.currentUser='';
  localStorage.clear();
  this.route.navigate(['login'])
  this.route.events.subscribe(event => {
    if (event instanceof NavigationEnd) {
      window.location.reload();
  }
})}

}


