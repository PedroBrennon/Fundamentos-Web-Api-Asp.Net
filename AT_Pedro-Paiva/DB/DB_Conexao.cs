using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;   

namespace AT_Pedro_Paiva.DB
{
    public class DB_Conexao
    {

        #region Declaracoes
        private SqlConnection Sql_Conexao = null; 
        private SqlCommand Sql_Comando = null;
        private String String_Conex = "";
        public SqlDataReader Sql_Reader = null;
        private SqlDataAdapter Sql_Adapter = null;
        private SqlTransaction Sql_Transacao = null;
        String Error = "";
        #endregion

        #region Propriedades
        public String String_Conexao
        {
            get { return String_Conex; }
            set { String_Conex = value; }
        }
        #endregion

        #region Funcoes
        private void Criar_Conexao()
        {
            if (Sql_Conexao == null)
                Sql_Conexao = new SqlConnection();
        }

        private void criar_Comando(string sql)
        {
            Sql_Comando = new SqlCommand();
            Sql_Comando.CommandType = CommandType.Text;
            Sql_Comando.CommandText = sql;
            Sql_Comando.Connection = Sql_Conexao;
            Sql_Comando.Transaction = Sql_Transacao;

        }

        public bool Conectar()
        {
            Criar_Conexao();
            if (Sql_Conexao.State == ConnectionState.Broken || Sql_Conexao.State == ConnectionState.Closed)
            {
                Sql_Conexao.ConnectionString = String_Conex;
                Sql_Conexao.Open();
            }
            if (Sql_Conexao.State != ConnectionState.Broken && Sql_Conexao.State != ConnectionState.Closed)
                return true;
            return false;
        }

        public bool Executar(string comando, bool selecionar)
        {
            try
            {
                criar_Comando(comando);
                if (selecionar)
                {
                    Sql_Reader = Sql_Comando.ExecuteReader();
                    return true;
                }
                else
                {
                    return (Sql_Comando.ExecuteNonQuery() > 0);
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                Console.WriteLine(Error);
                return false;
            }
        }

        public bool Get_Data(DataSet ds, string tablename)
        {
            criar_Adaptador();
            Sql_Adapter.Fill(ds);
            ds.Tables[ds.Tables.Count - 1].TableName = tablename;
            return true;
        }

        private bool criar_Adaptador()
        {
            Sql_Adapter = new SqlDataAdapter();
            return true;
        }

        public bool Desconectar()
        {
            if (Sql_Reader != null) Sql_Reader.Close();
            Sql_Conexao.Close();

            if (Sql_Comando != null) Sql_Comando.Dispose();
            Sql_Conexao.Dispose();

            Sql_Reader = null;
            Sql_Comando = null;
            Sql_Conexao = null;


            return true;
        }

        public bool Criar_Transacao()
        {
            Sql_Transacao = Sql_Conexao.BeginTransaction();
            return Sql_Transacao != null;
        }

        public void Close_transaction(bool commit)
        {
            if (commit)
            {
                Sql_Transacao.Commit();
            }
            else
            {
                Sql_Transacao.Rollback();
            }
        }
        #endregion
    }
}