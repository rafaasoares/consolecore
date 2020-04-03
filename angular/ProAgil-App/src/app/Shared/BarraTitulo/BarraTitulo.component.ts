import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-BarraTitulo',
  templateUrl: './BarraTitulo.component.html',
  styleUrls: ['./BarraTitulo.component.css']
})
export class BarraTituloComponent implements OnInit {
  @Input() title: string;

  constructor() { }

  ngOnInit() {
  }

}
