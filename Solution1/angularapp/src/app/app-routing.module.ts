import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

//import { AppComponent } from './app.component';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { CreateComponent } from '../create/create.component';
import { ViewComponent } from '../view/view.component';
import { PageNotFoundComponent } from '../page-not-found/page-not-found.component';


const appRoutes: Routes = [
  {
    path: '',
    component: DashboardComponent,
  },
  {
    path: 'create',
    component: CreateComponent,
  },
  {
    path: 'view',
    component: ViewComponent,
  },
  //{
  //  path: 'admin',
  //  loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule),
  //  canMatch: [authGuard]
  //},
  //{
  //  path: 'crisis-center',
  //  loadChildren: () => import('./crisis-center/crisis-center.module').then(m => m.CrisisCenterModule),
  //  data: { preload: true }
  //},
  { path: '', redirectTo: '/', pathMatch: 'full' },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(
      appRoutes,
      {
        enableTracing: false, // <-- debugging purposes only
        //preloadingStrategy: SelectivePreloadingStrategyService,
      }
    )
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
