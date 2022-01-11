import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { SynchronizeComponent } from './synchronize/synchronize.component';
import { ConsultComponent } from './consult/consult.component';
import { NavigateComponent } from './navigate/navigate.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SynchronizeComponent,
    ConsultComponent,
    NavigateComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
