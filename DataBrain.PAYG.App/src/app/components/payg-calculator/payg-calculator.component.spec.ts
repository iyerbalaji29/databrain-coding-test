import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaygCalculatorComponent } from './payg-calculator.component';

describe('PaygCalculatorComponent', () => {
  let component: PaygCalculatorComponent;
  let fixture: ComponentFixture<PaygCalculatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaygCalculatorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PaygCalculatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
