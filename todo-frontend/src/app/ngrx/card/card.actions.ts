import { createAction, props } from "@ngrx/store";

export const deleteCardApi = createAction(
    '[Card API] Delete Card',
    props<{ cardId: number, boardId: number}>());

export const postCardApi = createAction(
    '[Card API] Post Card',
    props<{card: any, boardId: number,}>());

export const patchCardApi = createAction(
    '[Card API] Patch Card',
    props<{card: any, boardId: number}>());