import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public transactions?: Transaction[];

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.http.get<Transaction[]>('/api/transaction').subscribe(result => {
      this.transactions = result;
    }, error => console.error(error));
  }

  public currency = new Intl.NumberFormat('en-PH', {
    style: 'currency',
    currency: 'PHP',
  });
}

interface Transaction {
  id: number
  name: string;
  date: string;
  cost: number;
}
