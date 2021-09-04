using AT_Pedro_Paiva.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AT_Pedro_Paiva.Models
{
    public class Amigo
    {
        public List<AmigoModel> listaAmigos = new List<AmigoModel>();
        public List<AmigoModel> buscaAmigo = new List<AmigoModel>();
        public List<AmigoModel> listaAmigosOrdenados = new List<AmigoModel>();
        public List<AmigoModel> listaAmigosDoDia = new List<AmigoModel>();

        DateTime hoje = new DateTime();
        

        DB_Amigo db_user = new DB_Amigo();

        public void ListarAmigos()
        {
            DataTable dt_aux = new DataTable();

            dt_aux = db_user.get_Amigo("");

            listaAmigos = dt_aux.AsEnumerable()
                .Select(p => new AmigoModel
                {
                    id = int.Parse(p["id"].ToString()),
                    nome = p["nome"].ToString(),
                    sobrenome = p["sobrenome"].ToString(),
                    aniversario = (DateTime)p["aniversario"]
                }).ToList();

        }

        public void AniversariantesDoDia()
        {
            DataTable dt_aux = new DataTable();

            dt_aux = db_user.lista_Aniversariantes_do_Dia();

            listaAmigosDoDia = dt_aux.AsEnumerable()
                .Select(p => new AmigoModel
                {
                    id = int.Parse(p["id"].ToString()),
                    nome = p["nome"].ToString(),
                    sobrenome = p["sobrenome"].ToString(),
                    aniversario = (DateTime)p["aniversario"]
                }).ToList();

        }

        public void AniversariantesOrdenados()
        {
            hoje = DateTime.Now;
            DataTable dt_aux = new DataTable();

            dt_aux = db_user.lista_Amigos_Ordenados();

            listaAmigosOrdenados = dt_aux.AsEnumerable()
                .Select(p => new AmigoModel
                {
                    id = int.Parse(p["id"].ToString()),
                    nome = p["nome"].ToString(),
                    sobrenome = p["sobrenome"].ToString(),
                    aniversario = (DateTime)p["aniversario"]
                }).ToList();
        }

        public void CriarAmigo(AmigoModel amigoModel)
        {
            db_user.InsertAmigo(amigoModel);
        }

        public void DeletarAmigo(int id)
        {
            db_user.Delete_Amigo(id);
        }

        public void EditarAmigo(AmigoModel amigoModel)
        {
            db_user.Update_Amigo(amigoModel);
        }

        public void BuscarAmigo(string nome)
        {
            DataTable dt_aux = new DataTable();

            db_user.getAmigos(nome);

            buscaAmigo = dt_aux.AsEnumerable()
                .Select(p => new AmigoModel
                {
                    id = int.Parse(p["id"].ToString()),
                    nome = p["nome"].ToString(),
                    sobrenome = p["sobrenome"].ToString(),
                    aniversario = (DateTime)p["aniversario"]
                }).ToList();
        }
    }
}