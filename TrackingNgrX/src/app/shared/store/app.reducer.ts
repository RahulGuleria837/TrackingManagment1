import { createReducer, on } from "@ngrx/store";
import { Appstate } from "./appstate";
import { setApiStatus } from "./app.action";
import { state } from "@angular/animations";


export const initialState:Readonly<Appstate>={
    apiResponseMessage:'',
    apiStatus:'',
};

export class AppReducer {
}

export const appReducer = createReducer(
    initialState,
    on(setApiStatus,(state,{ apiStatus }) =>{
    
    return{
        ...state,
        ...setApiStatus
    };

})
);