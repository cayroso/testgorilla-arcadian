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
    this.getTransactions();
  }

  public currencyFormatter = new Intl.NumberFormat('en-PH', {
    style: 'currency',
    currency: 'PHP',
  });

  getTransactions() {
    this.http.get<ITransaction[]>('/api/transaction/get').subscribe(result => {
      this.transactions = result;
    }, error => console.error(error));
  }

  deleteTransaction(transaction: ITransaction) {
    const ok = confirm(`Are you sure you want to delete transaction [${transaction.name}]`);

    if (!ok)
      return;

    this.http.delete<ITransaction[]>(`/api/transaction/delete/${transaction.id}`).subscribe(result => {


      alert(`Transaction [${transaction.name}] record deleted.`);

      this.getTransactions();
    }, error => console.error(error));
  }
}


