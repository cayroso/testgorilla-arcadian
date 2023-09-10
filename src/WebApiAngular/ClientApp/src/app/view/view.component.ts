import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { ITransaction } from '../models/Transaction';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html'
})
export class ViewComponent {
  public transaction?: ITransaction;
  public id?: string;
  public errorMessage?: string;

  constructor(private http: HttpClient, private activatedRoute: ActivatedRoute, private _router: Router) { }

  ngOnInit() {
    this.activatedRoute.paramMap.subscribe(params => {
      const id = params.get("id");

      this.http.get<ITransaction>(`/api/transaction/getbyid/${id}`).subscribe(result => {
        this.transaction = result;
        this.transaction.date = new Date(this.transaction.date);
      }, error => {
        this.errorMessage = error.error;
        console.error(error);
      });

    })
  }

  public currencyFormatter = new Intl.NumberFormat('en-PH', {
    style: 'currency',
    currency: 'PHP',
  });

  public dateFormatter = new Intl.DateTimeFormat('en-PH');

  deleteTransaction() {
    if (this.transaction == null)
      return;

    const ok = confirm(`Are you sure you want to delete transaction [${this.transaction.name}]`);

    if (!ok)
      return;

    this.http.delete<ITransaction[]>(`/api/transaction/delete/${this.transaction.id}`).subscribe(result => {

      if (this.transaction != null) {
        alert(`Transaction [${this.transaction.name}] record deleted.`);
        this._router.navigateByUrl('/');
      }
    }, error => console.error(error));
  }
}

