import { createAction,props } from "@ngrx/store";

export const getState = createAction('[Board] Get State');

export const getBoardsApi = createAction('[Board API] Get Boards');

export const addBoardApi = createAction(
    '[Board API] Add Board',
    props<{ board: any}>());

export const deleteBoardApi = createAction(
    '[Board API] Delete Board',
    props<{ boardId: number}>());

export const changeBoardApi = createAction(
    '[Board API] Change Board',
    props<{ boardId: number, boardTitle: string }>());

export const AddCurrentBoard = createAction(
    '[Board] Add CurrentBoard', 
    props<{currentBoard: any}>());

export const AddBoards = createAction(
    '[Board] Add Boards',
    props<{boards: any}>());
