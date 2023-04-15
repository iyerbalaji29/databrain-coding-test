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
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // Return an observable with a user-facing error message.
    return throwError(() => new Error('Error occured since external API was unreachable'));
  }
}
