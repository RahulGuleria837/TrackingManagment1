import { Component, OnDestroy, OnInit } from '@angular/core';
import { NavigationEnd, NavigationStart, Event as NavigationEvent , Router, ActivatedRoute } from '@angular/router';
import { InvitedpersonService } from '../invitedperson.service';
import { InviteService } from '../invite.service';
import { invokeRealStateAPI } from '../realstate/store/realstate.action';

@Component({
  selector: 'app-invitedperson',
  templateUrl: './invitedperson.component.html',
  styleUrls: ['./invitedperson.component.scss']
})
export class InvitedpersonComponent implements OnInit,OnDestroy {
  name = 'Get Current Url Route Demo';
  showTable:boolean=false;
  invitationsId:string="";
  currentRoute: string | undefined;
  id:string="";
  specificUser:any
  invitedPerson:any
    Receiverid:string | undefined;
    Status:string | undefined;
  
    
    buttonLabel = 'Show Table';
  store: any;
  

  constructor (private route:Router,private router:ActivatedRoute,private invitedperservice:InvitedpersonService){
    console.log(route.url);


  }
 

  ngOnInit(): void {
  
    this.searchData()
    this.router.paramMap.subscribe(params=>{
      const Senderid = params.get('id');
      const Receiverid= params.get('id1')
      const status=params.get('id2')
      console.log('SenderId=',Senderid);
      console.log('Receiverid=',Receiverid);
      console.log('Status=',status);
      this.statuschanging();

    })
  }
  ngOnDestroy(): void {
    this.store.dispatch(( invokeRealStateAPI()));
  }
  //getting the id from the routes from the help of parmMap 
statuschanging(){
  debugger
  this.router.paramMap.subscribe(params=>{
    this.Receiverid = params.get('id1')??"";
    // this.displaystatus.SenderId= params.get('id1')??"";
    this.Status=params.get('id2')??"";
   
    this.invitedperservice.Status(this.Receiverid,this.Status).subscribe({
      next: (data)=>{
        console.log(data);
      },
      error:(err)=>{
        console.log(err);
      }
    })

  } )
}

searchData(){
  // this.router.paramMap.subscribe(params=>{
  //   this.Receiverid = params.get('id')??"";

  this.invitedperservice.invitedPersonData().subscribe({
    next:(data)=>{
      console.log(data,"data")
      this.invitedPerson = data;
    }
  })
  }

  toggleTable(invitationSenderUserId:any) {
    console.log(invitationSenderUserId);
    debugger
    this.showTable = !this.showTable;
    this.buttonLabel = this.showTable ? 'Hide Table' : 'Show Table';
    this.specificUser = invitationSenderUserId[0].invitationSenderUserId;
    console.log(this.specificUser);

  }

  // SpcecificPersondata(id:any){
  //   debugger
  //   this.invitedPerson.invitationSenderUserId = this.id;
  //   this.invitedperservice.specificUserData(this.id).subscribe({
  //     next:(ok)=>{
  //               this.specificUser = ok;
                
  //     }
  //   })
  // }


}
