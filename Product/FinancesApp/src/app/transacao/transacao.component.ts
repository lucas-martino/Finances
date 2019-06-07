import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-transacao',
  templateUrl: './transacao.component.html',
  styleUrls: ['./transacao.component.css']
})
export class TransacaoComponent {
  public transacoes: Transacao[];

  constructor() {
    let t = new Transacao();
    t.tipo = 1;
    t.valor = 10;
    t.descricao = 'descricao descricao descricao descricao descricao descricao descricao descricao descricao descricao descricao descricao descricao descricao descricao descricao';
    t.categoria = 'categoria';
    t.icone = 'pets';
    this.transacoes = [
      t,
      { tipo: 1, valor: 10, descricao: 'desc', categoria: 'cat', icone: 'home', cor: 'blue', data: '' },
    ];
  }


  color(tipo: number) {
    switch (tipo) {
      case 1:
        return "Green";

      case 2:
        return "Red";

      default:
        return "Black";
    }
  }
}

class Transacao {
  data: string;
  tipo: number;
  valor: number;
  descricao: string;
  categoria: string;
  icone: string;
  cor: string;
}
