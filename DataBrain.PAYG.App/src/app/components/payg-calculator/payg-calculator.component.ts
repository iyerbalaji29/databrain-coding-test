import { Component, Input } from '@angular/core';
import { Frequency } from 'src/app/models/model';
import { PaygApiService } from 'src/app/services/payg-api.service';

@Component({
  selector: 'app-payg-calculator',
  templateUrl: './payg-calculator.component.html',
  styleUrls: ['./payg-calculator.component.css']
})

export class PaygCalculatorComponent {
  @Input() income: number = 0;
  @Input() frequency: Frequency = Frequency.Weekly;
  tax: number = 0;
  frequencyOptions = Object.values(Frequency);

  constructor(private apiService: PaygApiService) {}

  calculateTax(): void {
    this.apiService.calculateTax(this.income, this.frequency)
      .subscribe(tax => this.tax = tax);
  }

}
