import { createFeatureSelector, createSelector } from "@ngrx/store";
import { BoardState } from "./board.reducer";

export const selectFeature = createFeatureSelector<BoardState>('boards');

export const selectBoard = createSelector(
  selectFeature, 
    (state: BoardState) => state.currentBoard
);
export const selectBoards = createSelector(
  selectFeature,
    (state: BoardState) => state.boards
);

