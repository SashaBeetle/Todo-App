import { ApplicationConfig, isDevMode } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { provideStore } from '@ngrx/store';
import { provideStoreDevtools } from '@ngrx/store-devtools';
import { provideEffects } from '@ngrx/effects';
import { provideRouterStore } from '@ngrx/router-store';
import { boardReducers } from './ngrx/board/board.reducer';
import * as boardEffects from './ngrx/board/board.effects'

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes), 
    provideClientHydration(), 
    provideHttpClient(), 
    provideEffects(
      boardEffects,
    ), 
    provideStore({
      [boardReducers.name]: boardReducers.reducer,
    }), 
    provideStoreDevtools({ maxAge: 25, logOnly: !isDevMode(), autoPause: true, trace: false,traceLimit:75 }), 
    provideRouterStore()]
};
