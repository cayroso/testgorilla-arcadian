import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Bootswatch, Theme } from '../models/Bootswatch';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  public themes: Array<Theme> = [];
  public selectedTheme: string = '';

  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  constructor(private http: HttpClient) { }

  ngOnInit() {

    this.http.get<Bootswatch>(`https://bootswatch.com/api/5.json`).subscribe(result => {
      this.themes = result.themes;

      const cachedTheme = localStorage.getItem('theme') || '';

      if (cachedTheme) {
        this.selectedTheme = cachedTheme;
        this.onSelectedTheme();
      }
      else {
        this.selectedTheme = this.themes.find(e => e.name === 'Sketchy')?.cssMin || '';
        this.onSelectedTheme();
      }


    }, error => {
      console.error(error);
    });

  }

  onSelectedTheme() {
    localStorage.setItem('theme', this.selectedTheme);

    let link = document.createElement('link');
    link.rel = 'stylesheet';
    link.setAttribute('href', this.selectedTheme);

    document.head.appendChild(link);
  }
}


