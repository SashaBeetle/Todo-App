import { createAction,props } from "@ngrx/store";

export const getBoard = createAction('[Board] Get Board');

export const AddBoard = createAction(
    '[Board] Add board', 
    props<{board: any}>());
