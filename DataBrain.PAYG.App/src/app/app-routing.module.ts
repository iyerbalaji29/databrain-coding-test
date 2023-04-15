import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PaygCalculatorComponent } from './components/payg-calculator/payg-calculator.component';

const routes: Routes = [
  { path: '', redirectTo: 'payg-calculator', pathMatch: 'full' },
  { path: 'payg-calculator', component: PaygCalculatorComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
