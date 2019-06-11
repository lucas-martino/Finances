using System;
using Finances.Domain.Entity.Validator;

namespace Finances.Domain.Entity
{
    public class Vigencia : FinancesEntity
    {
        public Vigencia()
        {
            Validator = new VigenciaValidator();
        }

        public Vigencia(int referencia)
        {
            Referencia = referencia;
            int ano = Ano();
            int mes = Mes();
            _inicio = new DateTime(ano, mes, 1);
            _termino = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));
        }

        private DateTime _inicio, _termino;

        public int Referencia { get; set; }
        public Usuario Usuario { get; set; }

        public int Mes()
        {
            return Referencia % 100;
        }

        public int Ano()
        {
            return Referencia / 100;
        }

        public DateTime Inicio
        {
            get { return _inicio; }
        }

        public DateTime Termino
        {
            get { return _termino; }
        }
    }
}