import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationComponent, PaginationModule } from 'ngx-bootstrap/pagination';
import { Pagnation } from './Component/pagnation/pagnation';

@NgModule({
  declarations: [Pagnation],
  imports: [CommonModule, PaginationModule],
  exports: [PaginationModule ,Pagnation],
})
export class SharedModule {}
