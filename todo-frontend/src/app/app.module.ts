import { NgModule } from '@angular/core';
import { HttpClientModule, provideHttpClient, withFetch } from '@angular/common/http';

@NgModule({
  imports: [
    HttpClientModule
  ],
  providers: [
    provideHttpClient(withFetch()) // Enable fetch
  ]
})
export class AppModule { }