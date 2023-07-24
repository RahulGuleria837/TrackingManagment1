import { Component, ElementRef, OnDestroy, OnInit } from '@angular/core';
import { NavigationEnd, NavigationStart, Event as NavigationEvent , Router, ActivatedRoute } from '@angular/router';
import { InvitedpersonService } from '../invitedperson.service';
import { InviteService } from '../invite.service';
import { getInvitationrealstate, invokeRealStateAPI } from '../realstate/store/realstate.action';

@Component({
  selector: 'app-invitedperson',
  templateUrl: './invitedperson.component.html',
  styleUrls: ['./invitedperson.component.scss']
})
export class InvitedpersonComponent implements OnInit,OnDestroy {
  name = 'Get Current Url Route Demo';
  showTable:boolean=false;
  showTableTracking:boolean=false;
  invitationsId:string="";
  currentRoute: string | undefined;
  id:string="";
  specificUser:any
  invitedPerson:any
  reciverid:string | undefined;
  status:string | undefined;
  
    
    buttonLabel = 'Show Table';
    buttonLabeltracking='Show Table';
  store: any;
  

  constructor (private route:Router,private router:ActivatedRoute,private invitedperservice:InvitedpersonService,private elementRef: ElementRef){
    // console.log(route.url);
   }
 

  ngOnInit(): void {
    this.searchData()
  }

  ngOnDestroy(): void {
    this.elementRef.nativeElement.remove();
 }

searchData(){
  this.invitedperservice.invitedPersonData().subscribe({
    next:(data)=>{
      this.invitedPerson = data;
    }})
  }

  toggleTable(invitationSenderUserId:any) {
    console.log(invitationSenderUserId)
    this.showTable = !this.showTable;
    this.buttonLabel = this.showTable ? 'Hide Table' : 'Show Table';
    this.specificUser = invitationSenderUserId;
    // console.log(this.specificUser);
  }
  toggletablefortracking() {
    this.showTable = !this.showTableTracking;
    this.buttonLabeltracking = this.showTable ? 'Hide Table' : 'Show Table';
  }

  spcecificPersondata(id:any){
    this.invitedPerson.invitationSenderUserId = this.id;
    this.invitedperservice.specificUserData(this.id).subscribe({
      next:(ok)=>{ this.specificUser = ok;
                // console.log(ok,"ok")
               }})}
}