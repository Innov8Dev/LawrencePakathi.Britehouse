import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { error } from 'util';

@Injectable({
  providedIn: 'root'
})
export class BusinessService {
  uri = '/api/ids';

  constructor(private http: HttpClient) { }
  errorMessage: string;
  saveId(idNumber) {
    const obj = [{
      idNumber: idNumber
    }];
    console.log(obj);
    this.http.post(this.uri, obj).subscribe(res => console.log(res), error => this.errorMessage=<any>error);

  }
}
