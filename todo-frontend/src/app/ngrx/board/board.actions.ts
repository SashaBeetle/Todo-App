import { createAction,props } from "@ngrx/store";

export const getBoards = createAction('[Board] Get Board');

export const getBoardsTest = createAction('[Board] Get Boards');


export const AddCurrentBoard = createAction(
    '[Board] Add currentBoard', 
    props<{currentBoard: any}>());

export const AddBoards = createAction(
    '[Board] Add boards',
    props<{boards: any}>());
