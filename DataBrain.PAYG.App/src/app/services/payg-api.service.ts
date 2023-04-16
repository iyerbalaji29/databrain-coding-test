import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { Frequency } from '../models/model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PaygApiService {

  constructor(private http: HttpClient) { }
  
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*'
    })
  };

  calculateTax(income: number, frequency: Frequency): Observable<any> {
    const queryParams = {
      earnings: income,
      frequency: frequency.toString()
    };
    return this.http.get<any>(environment.apiUrl, { params: queryParams, headers: this.httpOptions.headers }).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse) {
      console.error(
        `Error while connecting to api service ${error.status} `);
    
    // Return an observable with a user-facing error message.
    return throwError(() => new Error('Issue while connecting to service, please try again'));
  }
}
