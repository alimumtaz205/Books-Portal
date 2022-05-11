import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl:any;

  constructor(private http: HttpClient) {

    this.baseUrl = environment.apiURL;
   }

login(model:any){
  debugger
  return this.http.post<any>(this.baseUrl + "/Account/LoginUser", model)
}

}
