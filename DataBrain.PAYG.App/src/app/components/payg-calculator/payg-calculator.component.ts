import { Component, OnInit } from '@angular/core';
import { Frequency } from 'src/app/models/model';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { PaygApiService } from 'src/app/services/payg-api.service';

@Component({
  selector: 'app-payg-calculator',
  templateUrl: './payg-calculator.component.html',
  styleUrls: ['./payg-calculator.component.css']
})

export class PaygCalculatorComponent  implements OnInit{
  taxForm!: FormGroup;
  income!: number;
  frequency!: Frequency;
  taxAmount!: number;
  errorMessage!: string;

  frequencyOptions = Object.values(Frequency);

  constructor(private apiService: PaygApiService,private formBuilder: FormBuilder) {
    this.taxForm = this.formBuilder.group({
      income: ['', [Validators.required, Validators.pattern('^[0-9]*(\.[0-9]{0,2})?$')]],
      frequency: ['', Validators.required]
    });
  }

  ngOnInit(): void {

  }

  calculateTax(): void {
    this.errorMessage = '';
    if (this.taxForm.invalid) {
      return;
    }

    this.income = this.taxForm.get('income')?.value;
    this.frequency = this.taxForm.get('frequency')?.value;

    this.apiService.calculateTax(this.income,this.frequency)
      .subscribe((data) => {
        this.taxAmount = data;
      },
      (error) => {
        this.errorMessage = error;
      });
  }
}
