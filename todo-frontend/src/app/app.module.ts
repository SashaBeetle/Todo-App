import { NgModule } from '@angular/core';
import { HttpClientModule, provideHttpClient, withFetch } from '@angular/common/http';
import { ScrollPanelModule } from 'primeng/scrollpanel';

@NgModule({
  imports: [
    HttpClientModule,
    ScrollPanelModule,
  ],
  providers: [
    provideHttpClient(withFetch()),
  ]
})
export class AppModule { }