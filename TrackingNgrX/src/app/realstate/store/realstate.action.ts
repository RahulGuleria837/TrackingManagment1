import { createAction, props } from "@ngrx/store";
import { Realstate } from "./realstate";

 
export const invokeRealStateAPI = createAction(
    '[RealState API] Invoke RealState Fetch API'
  );
   
  export const RealStateFetchAPISuccess = createAction(
    '[RealState API] Fetch API Success',
    props<{ allStates: any[] }>()
  );

  export const invokeSaveRealStateAPI = createAction(
    '[RealState API] Invoke save new State api',
    props<{newStates: any }>()
  );

  export const saveNewRealStateAPISucess = createAction(
    '[RealState API] Invoke new State api success',
    props<{newStates: any}>()
  );

  export const invokeupdateRealStateAPI = createAction(
    '[RealState API] update new state api success',
    props<{updateState : Realstate}>()
  );
   
  export const updateRealStateAPISucess = createAction(
    '[Books API] update state api success',
    props<{updateState: Realstate}>()
  );