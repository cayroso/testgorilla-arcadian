export class Theme {
  constructor(public name: string, public cssMin: string) { }
}
export class Bootswatch {
  constructor(public themes: Theme[]) { }
}
