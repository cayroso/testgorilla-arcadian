import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html'
})
export class CreateComponent {
  public transaction: Transaction = new Transaction();

  constructor(private http: HttpClient, private _router: Router) {

  }
  submitted = false;

  onSubmit() { this.submitted = true; }

  submit() {

    debugger;
    this.http.post<Transaction>('/api/transaction', this.transaction).subscribe(result => {
      this._router.navigateByUrl(`/view/${result}`);
    }, error => console.error(error));
  }
}

export class Transaction {
  constructor(public name = '', public date?: Date, public cost: number = 1) {
  }
}
