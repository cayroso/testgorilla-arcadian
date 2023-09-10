import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html'
})
export class CreateComponent {
  public transaction: Transaction = new Transaction();

  constructor(private http: HttpClient, private _router: Router) { }

  submit() {

    debugger;
    this.http.post<Transaction>('/api/transaction/create', this.transaction).subscribe(result => {
      //  NOTE: leaving this in case the requirement changed to view the record after creatd
      //this._router.navigateByUrl(`/view/${result}`);

      this._router.navigateByUrl('/');

    }, error => console.error(error));
  }
}

export class Transaction {
  constructor(public name: string = '', public date: Date = new Date(), public cost: number = 1) { }
}
