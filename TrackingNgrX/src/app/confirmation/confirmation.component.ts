import { Component, OnInit } from '@angular/core';
import { InviteService } from '../invite.service';
import { ActivatedRoute } from '@angular/router';
import { InvitedpersonService } from '../invitedperson.service';

@Component({
  selector: 'app-confirmation',
  templateUrl: './confirmation.component.html',
  styleUrls: ['./confirmation.component.scss']
})
export class ConfirmationComponent implements OnInit {
  status: any;
  constructor(
    private invite: InvitedpersonService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    let reciverId = this.route.snapshot.paramMap.get('reciverId');
    console.log(reciverId);
    let getStatus = this.route.snapshot.paramMap.get('status') ?? '';
    let status = Number.parseInt(getStatus.toString().replace(':', ''));
    reciverId = reciverId?.toString().replace(':', '') ?? '';

    this.status = status == 1 ? 'Approved' : 'Rejected';

    // now we will call the api and we will change the invitation as appropiately ............
    this.invite.Status(reciverId, getStatus).subscribe({
      next: (data) => {
        console.log(data);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
