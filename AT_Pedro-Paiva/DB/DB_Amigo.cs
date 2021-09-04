using AT_Pedro_Paiva.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AT_Pedro_Paiva.DB
{
	public class DB_Amigo
	{
		DB_Conexao db;
		public string Error { get; set; }
        DateTime hoje = new DateTime();

        public bool conectar()
		{
			db = new DB_Conexao();
			db.String_Conexao = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\PEDRO\Visual Studio Projects\ASP.NET\AT_Pedro-Paiva\banco.mdf;Integrated Security=True;Connect Timeout=30";
			return db.Conectar();
		}

		public List<AmigoModel> getAmigos(string search)
		{
			conectar();

			string comando = @"select * from amigos where nome = {0}" + search;

			db.Executar(comando, true);
			List<AmigoModel> retorno = new List<AmigoModel>();
			AmigoModel aux;

			while (db.Sql_Reader.Read())
			{
				aux = new AmigoModel();
				aux.id = int.Parse(db.Sql_Reader["id"].ToString());
				aux.nome = db.Sql_Reader["nome"].ToString();
				aux.sobrenome = db.Sql_Reader["sobrenome"].ToString();
				aux.aniversario = (DateTime)db.Sql_Reader["aniversario"];

				retorno.Add(aux);
			}
			db.Desconectar();

			return retorno;
		}

		public DataTable get_Amigo(string search)
		{
			conectar();

			string comando = "select * from amigos";

			db.Executar(comando, true);
			DataTable dt = new DataTable();
			dt.Load(db.Sql_Reader);
			db.Desconectar();

			return dt;
		}

			public DataTable lista_Amigos_Ordenados()
			{
				conectar();
                hoje = DateTime.Now;

                string comando = @"select * from amigos order by case when month(aniversario) = "+hoje.Month+" then 0"
                                 + "when month(aniversario) = " + (hoje.Month + 1) + " then 1"
                                 + "when month(aniversario) = " + (hoje.Month + 2) + " then 2"
                                 + "when month(aniversario) = " + (hoje.Month + 3) + " then 3"
                                 + "when month(aniversario) = " + (hoje.Month + 4) + " then 4"
                                 + "when month(aniversario) = " + (hoje.Month + 5) + " then 5"
                                 + "when month(aniversario) = " + (hoje.Month + 6) + " then 6"
                                 + "when month(aniversario) = " + (hoje.Month + 7) + " then 7"
                                 + "when month(aniversario) = " + (hoje.Month + 8) + " then 8"
                                 + "when month(aniversario) = " + (hoje.Month + 9) + " then 9"
                                 + "when month(aniversario) = " + (hoje.Month + 10) + " then 10"
                                 + "when month(aniversario) = "+ (hoje.Month+11) +" then 11 end";

				db.Executar(comando, true);
				DataTable dt = new DataTable();
				dt.Load(db.Sql_Reader);
				db.Desconectar();

				return dt;
			}

			public DataTable lista_Aniversariantes_do_Dia()
			{
				conectar();
				hoje = DateTime.Now;
            
                string comando = @"select * from amigos where DATEPART(DAY, aniversario) = '"+ hoje.Day + "' AND DATEPART(MONTH, aniversario) = '"+ hoje.Month + "';" ;
                db.Executar(comando, true);
                DataTable dt = new DataTable();
                if (!db.Sql_Reader.Equals(null))
                {
                    dt.Load(db.Sql_Reader);
                    db.Desconectar();
                } else { db.Desconectar(); }

				return dt;
			}

			public bool InsertAmigo(AmigoModel _amigo)
		{
			bool retorno = false;

			try
			{
				conectar();

				string comando = @"insert into amigos
								   (nome,sobrenome,aniversario) 
								   values ('{0}','{1}','{2}')";

				comando = string.Format(comando, _amigo.nome,
												 _amigo.sobrenome,
												 _amigo.aniversario.ToString("yyyy-MM-dd"));

				retorno = db.Executar(comando, false);

			}
			catch (Exception ex)
			{
				Error = ex.Message;
			}
			finally
			{
				db.Desconectar();
			}

			return retorno;
		}

		public bool Update_Amigo(AmigoModel _amigo)
		{
			bool retorno = false;
			try
			{
				conectar();

				string comando = @"update amigos set
								   nome = '{1}', sobrenome = '{2}',
								   aniversario = '{3}'
								   where id = '{0}'";

				comando = string.Format(comando, _amigo.id,
												 _amigo.nome,
												 _amigo.sobrenome,
												 _amigo.aniversario.ToString("yyyy-MM-dd"));

				retorno = db.Executar(comando, false);
			}
			catch (Exception ex)
			{
				Error = ex.Message;
			}
			finally
			{
				db.Desconectar();
			}
			return retorno;
		}

		public bool Delete_Amigo(int id)
		{
			bool retorno = false;
			try
			{
				conectar();
				db.Criar_Transacao();

				string comando = "delete amigos where id = '{0}'";

				comando = string.Format(comando, id);
				retorno = db.Executar(comando, false);
				db.Close_transaction(retorno);
			}
			catch (Exception ex)
			{
				Error = ex.Message;
			}
			finally
			{
				db.Desconectar();
			}

			return retorno;
		}
}
}