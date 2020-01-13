using System;
using System.Collections.Generic;
using System.Text;

namespace DbServer.Dominio.Modelo
{
    public class ContaCorrente : Entidade
    {
        public string Agencia { get; set; }
        public string Numero { get; set; }
        public string Digito { get; set; }
        public decimal Saldo { get; set; }

        public ContaCorrente()
        {
            this.Id = string.Empty;
        }

        public ContaCorrente(string agencia, string numero, string digito)
        {
            this.Agencia = agencia;
            this.Numero = numero;
            this.Digito = digito;
        }

        public void Creditar(decimal valor)
        {
            this.Saldo += valor;
        }

        public void Debitar(decimal valor)
        {
            this.Saldo -= valor;
        }

        public bool ContaValida()
        {
            return this.Id != string.Empty;
        }
    }
}
