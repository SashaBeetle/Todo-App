import { createAction,props } from "@ngrx/store";

export const getState = createAction('[Board] Get State');

export const getBoardsApi = createAction('[Board API] Get Boards');

export const getBoardApi = createAction(
    '[Board API] Get Board',
    props<{boardId: number}>());

export const postBoardApi = createAction(
    '[Board API] Post Board',
    props<{ board: any}>());

export const deleteBoardApi = createAction(
    '[Board API] Delete Board',
    props<{ boardId: number}>());

export const patchBoardApi = createAction(
    '[Board API] Patch Board',
    props<{ boardId: number, boardTitle: string }>());

export const AddCurrentBoard = createAction(
    '[Board] Add CurrentBoard', 
    props<{currentBoard: any}>());

export const AddBoards = createAction(
    '[Board] Add Boards',
    props<{boards: any}>());
