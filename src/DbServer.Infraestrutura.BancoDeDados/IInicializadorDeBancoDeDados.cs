using System;
using System.Collections.Generic;
using System.Text;

namespace DbServer.Infraestrutura.BancoDeDados
{
    public interface IInicializadorDeBancoDeDados
    {
        string StringConexao { get; }
        void Inicializar();

    }
}
