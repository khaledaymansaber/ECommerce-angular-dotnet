import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core'; // إضافة CUSTOM_ELEMENTS_SCHEMA
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; // ضروري جداً للسبينر
import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { CoreModule } from './core/core-module';
import { SharedModule } from './shared/shared-module';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { Home } from './home/home';
import { HomeModule } from './home/home-module';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastrModule } from 'ngx-toastr';
import { loaderInterceptor } from './core/Interceptor/loader-interceptor';

@NgModule({
  declarations: [App],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HomeModule,
    NgxSpinnerModule,
    AppRoutingModule,
    CoreModule,
    ToastrModule.forRoot({
      closeButton:true,
      positionClass:'toast-top-right',
      countDuplicates:true,
      timeOut:1500,
      progressBar:true,
    })
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA], // مهم عشان الأنجولار يفهم تاج <ngx-spinner>
  providers: [
    provideClientHydration(),
    // تم التعديل هنا: تسجيل الانترسيبتور بالطريقة الحديثة
    provideHttpClient(withInterceptors([loaderInterceptor]))
  ],
  bootstrap: [App],
})
export class AppModule {}
