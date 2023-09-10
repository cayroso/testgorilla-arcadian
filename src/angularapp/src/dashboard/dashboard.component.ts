import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  public transactions?: Transaction[];

  constructor(http: HttpClient) {
    
    http.get('/api/transactions').subscribe(result => {
      debugger;
      //this.transactions = result;
    }, error => console.error(error));
  }
}

interface Transaction {
  id: number
  name: string;
  date: string; 
  cost: number;
}
