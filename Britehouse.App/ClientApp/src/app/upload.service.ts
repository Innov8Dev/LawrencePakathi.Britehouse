import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UploadService {
  uri = '/api/ids/postfiles';

  constructor(private http: HttpClient) { }

  upload(formdata) {
    let headers = new Headers();
    headers.append('Content-Type', 'text/csv');
    this.http.post(this.uri, formdata).subscribe(res => console.log(res));
  }
}
