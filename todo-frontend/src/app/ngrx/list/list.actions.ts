import { createAction, props } from "@ngrx/store";

export const deleteListApi = createAction(
    '[List API] Delete List',
    props<{ listId: number, boardId: number}>());

export const postListApi = createAction(
    '[List API] Post List',
    props<{boardId: number, list: any}>());

export const patchListApi = createAction(
    '[List API] Patch List',
    props<{list: any, boardId: number, newListTitle: string}>());