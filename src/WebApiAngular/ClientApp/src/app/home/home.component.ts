import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

import { ITransaction } from '../models/Transaction';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public transactions?: ITransaction[];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get<ITransaction[]>('/api/transaction/get').subscribe(result => {
      this.transactions = result;
    }, error => console.error(error));
  }

  public currencyFormatter = new Intl.NumberFormat('en-PH', {
    style: 'currency',
    currency: 'PHP',
  });
}


