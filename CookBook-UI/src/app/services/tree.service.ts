import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { TestClass } from '../components/tree/tree.component';

@Injectable({
  providedIn: 'root'
})
export class TreeService {

  constructor(public http: HttpClient) {

	}

	GetNodes(parentId: TestClass) {
    let body = JSON.stringify(parentId);
    const header = new HttpHeaders()
     .set('Content-type', 'application/json');
    return this.http.post("http://localhost:63441/api/test/post", body, { headers: header});
	}

  GetRoot() {
    return this.http.get("http://localhost:63441/api/test/root");
	}

}
