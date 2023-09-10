import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { ITransaction } from '../models/Transaction';
import { Transaction } from '../create/create.component';

@Component({
  selector: 'app-view',
  templateUrl: './edit.component.html'
})
export class EditComponent {
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

  submit() {
    this.http.put<Transaction>('/api/transaction/update', this.transaction).subscribe(result => {
      //  NOTE: leaving this in case the requirement changed to view the record after creatd
      //this._router.navigateByUrl(`/view/${result}`);

      this._router.navigateByUrl('/');
    }, error => console.error(error));
  }
}

