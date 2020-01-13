using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DbServer.Infraestrutura.BancoDeDados.Repositorios
{
    public class Repositorio
    {
        private readonly IDbConnection _connection;
      
        protected IDbConnection Connection { get { return this._connection; } }
      

        public Repositorio(IDbConnection connection)
        {
            this._connection = connection;
            this.Connection.Open();
        }

        protected virtual void EnsureConnectionIsOpen()
        {
            if (ShouldOpenConnection(this.Connection.State))
            {
                lock (this.Connection)
                {
                    if (ShouldOpenConnection(this.Connection.State))
                    {
                        this.Connection.Open();
                    }
                }
            }
        }

        private static bool ShouldOpenConnection(ConnectionState connectionState)
        {
            return connectionState == ConnectionState.Closed || connectionState == ConnectionState.Broken;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Connection != null)
                {
                    this.Connection.Dispose();
                }
            }
        }

    }
}
