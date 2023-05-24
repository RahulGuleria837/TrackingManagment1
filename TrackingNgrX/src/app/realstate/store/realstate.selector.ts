import { createFeatureSelector, createSelector } from "@ngrx/store";
import { Realstate } from "./realstate";


//CREATE

export const selectRealstate = createFeatureSelector<any[]>('mystate');


//Edit
export const selectRealstatebyID = (id:number)=>
createSelector(selectRealstate,(realState:Realstate[])=>{
var realstatebyId = realState.filter((_)=> _.id == id );

if(realstatebyId.length == 0){
    return null;
}
return realstatebyId[0];
});
