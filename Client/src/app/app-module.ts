import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { CoreModule } from './core/core-module';
import { SharedModule } from './shared/shared-module';
import { provideHttpClient } from '@angular/common/http';
import { ShopModule } from './shop/shop-module';

@NgModule({
  declarations: [
    App
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    SharedModule,
    ShopModule
  ],
  providers: [
   // provideBrowserGlobalErrorListeners(),
    provideClientHydration(),
    provideHttpClient()

  ],
  bootstrap: [App]
})
export class AppModule { }
