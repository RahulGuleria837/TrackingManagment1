import { createFeatureSelector } from "@ngrx/store";
import { Appstate } from "./appstate";


export class AppSelector {
}

export const selectAppState = createFeatureSelector<Appstate>('appState');