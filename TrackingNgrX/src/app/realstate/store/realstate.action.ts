import { createAction, props } from "@ngrx/store";

 
export const invokeRealStateAPI = createAction(
    '[RealState API] Invoke RealState Fetch API'
  );
   
  export const RealStateFetchAPISuccess = createAction(
    '[RealState API] Fetch API Success',
    props<{ allStates: any[] }>()
  );
