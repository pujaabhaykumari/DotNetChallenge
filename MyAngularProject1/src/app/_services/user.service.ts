import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { User, Vitual } from '@app/_models';

@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<User[]>(`${environment.apiUrl}/users`);
    }

  

    AddData(vitualData:Vitual) {
        return this.http.post<any>(`${environment.apiUrl}/addData`,  vitualData );
            
          
    }

}