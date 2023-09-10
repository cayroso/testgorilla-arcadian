import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html'
})
export class ViewComponent {
  public transaction?: Transaction;
  public id?: String;

  constructor(private http: HttpClient, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.activatedRoute.paramMap.subscribe(params => {
      const id = params.get("id");

      this.http.get<Transaction>(`/api/transaction/${id}`).subscribe(result => {
        this.transaction = result;
        this.transaction.date = new Date(this.transaction.date);
      }, error => console.error(error));

    })
  }

  public currency = new Intl.NumberFormat('en-PH', {
    style: 'currency',
    currency: 'PHP',
  });

  public dateFormatter = new Intl.DateTimeFormat('en-PH');

  //constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  //  http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').subscribe(result => {
  //    this.forecasts = result;
  //  }, error => console.error(error));
  //}
}

interface Transaction {
  id: number
  name: string;
  date: Date;
  cost: number;
}
