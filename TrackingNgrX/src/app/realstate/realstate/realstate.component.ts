import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { selectRealstate } from '../store/realstate.selector';
import { invokeRealStateAPI } from '../store/realstate.action';

@Component({
  selector: 'app-realstate',
  templateUrl: './realstate.component.html',
  styleUrls: ['./realstate.component.scss']
})
export class RealstateComponent implements OnInit {

constructor(private store:Store){}

books$ = this.store.pipe(select(selectRealstate));

ngOnInit(): void {
  this.store.dispatch(invokeRealStateAPI());
}
}
