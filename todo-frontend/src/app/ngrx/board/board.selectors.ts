import { createFeatureSelector, createSelector } from "@ngrx/store";
import { BoardState } from "./board.reducer";

export const selecFeature = createFeatureSelector<BoardState>('board');

export const selectBoard = createSelector(
  selecFeature, 
    (state: BoardState) => state.board
);

