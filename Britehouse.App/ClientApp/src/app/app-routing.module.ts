import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { IdsComponent } from './ids/ids.component';
import { BulkUploadComponent } from './bulk-upload/bulk-upload.component';

const routes: Routes = [
  {
    path: 'ids',
    component:IdsComponent
  },
  {
    path: 'bulkupload',
    component:BulkUploadComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
