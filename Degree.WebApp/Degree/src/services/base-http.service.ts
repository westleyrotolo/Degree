import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import {catchError, map, tap} from 'rxjs/operators';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { HttpModule } from '@angular/http';
import { headersToString } from 'selenium-webdriver/http';
const httpOptions = {
    headers: new HttpHeaders(
      { 'Content-Type': 'application/json' }
    )
  };


@Injectable({
  providedIn: 'root'
})
export class BaseHttpService {

  constructor(private http: HttpClient) {}

  getData(url: string): Observable<any> {
      return this.http
          .get<any>(url)
          .pipe(
            tap(_ => {
            }),
          catchError(this.handleErrorObservable));
  }

  post(model, url: string): Observable<any> {
      const headers = new HttpHeaders({
          'Content-Type': 'application/json; charset=utf-8'
      });
      const options = { headers: headers };
      const body = JSON.stringify(model);
      return this.http
          .post(url, body, options)
          .pipe(
            map(this.extractData),
            catchError(this.handleErrorObservable));
  }

  deleteByBody(model, url: string): Observable<any> {
      const headers = new HttpHeaders({
          'Content-Type': 'application/json; charset=utf-8'
      });
      const options = { headers: headers };

      const body = JSON.parse(model);
      return this.http
          .post(url, body, options)
          .pipe(
              map(this.extractData),
              catchError(this.handleErrorObservable));
  }

  delete(url: string): Observable<any> {
      const headers = new HttpHeaders({
          'Content-Type': 'application/json; charset=utf-8'
      });
      const options = { headers: headers };

      return this.http
          .delete(url, options)
          .pipe(
          map(this.extractData),
          catchError(this.handleErrorObservable));
  }

  edit(model, url: string): Observable<any> {
      const headers = new HttpHeaders({
          'Content-Type': 'application/json; charset=utf-8'
      });
      const options = { headers: headers };
      const body = JSON.stringify(model);
      return this.http
          .put(url, body, options)
          .pipe(
          map(this.extractData),
          catchError(this.handleErrorObservable));
  }

  editByParams(url: string): Observable<any> {
      const headers = new HttpHeaders({
          'Content-Type': 'application/json; charset=utf-8'
      });
      const options = { headers: headers };
      return this.http
          .put(url, '', options)
          .pipe(
          map(this.extractData),
          catchError(this.handleErrorObservable));
  }

  private extractData(res: Response) {
      const body = res;
      return body || {};
  }

  private handleErrorObservable(error: Response | any) {
      console.error(error.message || error);
      return Observable.throw(error.message || error);
  }
}
