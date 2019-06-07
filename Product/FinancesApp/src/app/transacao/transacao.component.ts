import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ArgumentOutOfRangeError } from 'rxjs';

@Component({
  selector: 'app-transacao',
  templateUrl: './transacao.component.html',
  styleUrls: ['./transacao.component.css']
})
export class TransacaoComponent {
  public transacoes: Transacao[];
  public vigencia: Vigencia
  public dataAnterior: string;

  constructor() {
    this.vigencia = new Vigencia(6, 2019);

    this.dataAnterior = '';

    this.transacoes = [
      { tipo: 1, valor: 10, descricao: 'Salario', categoria: { nome: 'Categoria 1', icone: 'home', background: 'blue', cor: 'white' }, data: 'Segunda, 1' },
      { tipo: 2, valor: 10, descricao: 'Descrição 2', categoria: { nome: 'Categoria 2', icone: 'pets', background: 'red', cor: 'white' }, data: '' },
      { tipo: 2, valor: 10, descricao: 'Descrição 3', categoria: { nome: 'Categoria 3', icone: 'home', background: 'yellow', cor: 'black' }, data: '' },
      { tipo: 2, valor: 10, descricao: 'Descrição descricao descricao descricao descricao descricao descricao descricao descricao descricao descricao descricao descricao ', categoria: { nome: 'Categoria 3', icone: 'home', background: 'yellow', cor: 'black' }, data: 'Terça, 2' },
      { tipo: 2, valor: 10, descricao: 'Descrição 4', categoria: { nome: 'Categoria 4', icone: 'home', background: 'green', cor: 'white' }, data: 'Quarta, 3' },
      { tipo: 2, valor: 10, descricao: 'Descrição 5', categoria: { nome: 'Categoria 4', icone: 'home', background: 'grenn', cor: 'white' }, data: '' },
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

  proximaVigencia() {
    this.vigencia = this.vigencia.proximo();
  }

  anteriorVigencia() {
    this.vigencia = this.vigencia.anterior();
  }
}

class Transacao {
  data: string;
  tipo: number;
  valor: number;
  descricao: string;
  categoria: Categoria
}

class Categoria {
  nome: string
  icone: string
  cor: string
  background: string
}

class Vigencia {
  mes: number;
  ano: number;
  refencia: number;
  descricao: string;

  constructor(mes: number, ano: number) {
    this.mes = mes;
    this.ano = ano;
    this.refencia = ano * 100 + mes;

    let meses = [
      'Janeiro',
      'Feveiro',
      'Março',
      'Abril',
      'Maio',
      'Junho',
      'Julho',
      'Agosto',
      'Setembro',
      'Outubro',
      'Novembro',
      'Dezembro'
    ]

    this.descricao = meses[mes - 1];
    let agora = new Date();

    if (this.ano != agora.getFullYear())
      this.descricao += '/' + agora.getFullYear();
  }

  proximo() : Vigencia {
    let mes: number, ano: number;
    if (this.mes == 12) {
      mes = 1;
      ano = this.ano + 1;
    }
    else {
      mes = this.mes + 1;
      ano = this.ano;
    }

    return new Vigencia(mes, ano);
  }

  anterior() : Vigencia {
    let mes: number, ano: number;
    if (this.mes == 1) {
      mes = 12;
      ano = this.ano - 1;
    }
    else {
      mes = this.mes - 1;
      ano = this.ano;
    }

    return new Vigencia(mes, ano);
  }
}