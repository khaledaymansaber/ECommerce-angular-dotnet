import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBar } from './nav-bar/nav-bar';
import { AppRoutingModule } from "../app-routing-module";

@NgModule({
  declarations: [NavBar],
  imports: [CommonModule, AppRoutingModule],
  exports:[NavBar]
})
export class CoreModule {}
