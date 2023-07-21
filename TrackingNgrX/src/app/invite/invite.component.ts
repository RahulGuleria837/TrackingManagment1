import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { InviteService } from '../invite.service';
import { FormControl, FormGroup, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { Observable, Subject, debounceTime, map, switchMap } from 'rxjs';

@Component({
  selector: 'app-invite',
  templateUrl: './invite.component.html',
  styleUrls: ['./invite.component.scss']
})
export class InviteComponent implements OnInit {

  invites:any[]=[];
  displayNameIsNot: boolean = true;
  displayName = {
    userName:"",
    userId:"" };
  displayInvitation: any;
  results$: Observable<any>;
  subject = new Subject();
  constructor(private inviteService: InviteService) { this.results$ = new Observable(); 
  }
   
  ngOnInit(): void {

    this.allInvitedUsers();

    this.results$ = this.subject.pipe(
      debounceTime(2000),
      switchMap((searchText) =>
        this.inviteService
          .Invite(searchText)
          .pipe(
            map((response) =>
            

              response.length == 0
                ? [{ name: 'no one in db with this name or id' }]
                : response
               
            )
          
          )
      )
    );
    this.results$.subscribe({
      next: (data) => {
        console.log(data,"ok",);
      
      },
    });
  }


  search(event: any) {
    debugger
    if (!this.displayNameIsNot) {
      this.displayNameIsNot = true;
    }
    const searchText = event.target.value;
    if (searchText.length == 0) {
      this.displayNameIsNot = false;
      return;
    }
    debugger
    // emits the `searchText` into the stream. This will cause the operators in its pipe function (defined in the ngOnInit method) to be run. `debounceTime` runs and then `map`. If the time interval of 1 sec in debounceTime hasn’t elapsed, map will not be called, thereby saving the server from being called.
    this.subject.next(searchText);
    console.log(event.target.value)
  }

  mouseover(event: any, userId: any) {
    debugger
    if (event.target.value == 'no one in db with this name or id') {
      return;
    }
    this.displayName.userId=userId;
    this.displayName.userName = event.target.value;
    this.displayNameIsNot = false;
    console.log(this.displayName)
  }

  showInvitationUser() {
    debugger
    console.log(this.displayInvitation.display,"display")
    if (this.displayInvitation.display == 'none') {
      this.displayInvitation = {
        display: 'block',
      };
    } else {
      debugger
      this.displayInvitation = {
        display: 'none',
      };
    }
  }

  //SENDING INVITATION TO THE 
   sendInvitation() {
    debugger
    console.log("check",this.displayName.userId,this.displayName)
    let invitationUser = this.displayName.userId;
    this.inviteService.SendEmail(invitationUser).subscribe({
      next: (data) => {
        console.log(data);
      },
      error: (err) => {
        console.log(err);
      },
    });
   }

   //SHOWING ALL DATA OF INVITED USERS
   allInvitedUsers(){
    this.inviteService.showInvitedUsers().subscribe({
      next:(data)=>{
        this.invites=data;
        console.log(data)
      }
    })
   }

   //CHANGING ACTION WHILE CLICKING ON THE ACTION 
   actionsChange(receiverid:any,action:any){
    debugger
    this.inviteService.changeAction(receiverid,action).subscribe({
      next:(data)=>{
      console.log(data)
      }
    })
   }


}









