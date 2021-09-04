using AT_Pedro_Paiva.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AT_Pedro_Paiva.Controllers
{
    public class AmigoController : Controller
    {
        private Amigo _amigos = new Amigo();

        //Index (lista aniversariantes e lista ordenada crescente aniversariantes)
        public ActionResult Index()
        {
            _amigos.AniversariantesDoDia();
            _amigos.AniversariantesOrdenados();
            _amigos.ListarAmigos();
            var listaGeral = new AmigoModel
            {
                amigosDoDia = _amigos.listaAmigosDoDia,
                amigosOrdenados = _amigos.listaAmigosOrdenados
            };

            return View(listaGeral);
        }

        //Lista Amigo
        public ActionResult ListaAmigo(string busca)
        {
            _amigos.ListarAmigos();
            var listaAmigos = _amigos.listaAmigos;

            if (!String.IsNullOrEmpty(busca))
            {

                listaAmigos = listaAmigos.Where(a => a.nome.ToUpper().Contains(busca.ToUpper())).ToList();
            }
            return View(listaAmigos);
        }


        //Adicionar Amigo
        public ActionResult AdicionarAmigo()
        {
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult AdicionarAmigo(AmigoModel _amigo)
        {
            _amigos.CriarAmigo(_amigo);
            _amigos.ListarAmigos();
            return RedirectToAction("Index");
        }
        
        //Deletar Amigo
        public ActionResult DeletarAmigo(int id)
        {
            _amigos.ListarAmigos();
            return View(_amigos.listaAmigos.Where(p => p.id == id).First());
        }

        [HttpPost]
        public RedirectToRouteResult DeletarAmigo(int id, FormCollection collection)
        {
            _amigos.DeletarAmigo(id);
            _amigos.ListarAmigos();
            return RedirectToAction("Index");
        }

        //Editar Amigo
        public ActionResult EditarAmigo(int id)
        {
            _amigos.ListarAmigos();
            return View(_amigos.listaAmigos.Where(p => p.id == id).First());
        }

        [HttpPost]
        public RedirectToRouteResult EditarAmigo(AmigoModel _amigo)
        {
            _amigos.EditarAmigo(_amigo);
            _amigos.ListarAmigos();
            return RedirectToAction("Index");
        }

        //Detalhar Amigo
        public ActionResult DetalharAmigo(int id)
        {
            _amigos.ListarAmigos();
            return View(_amigos.listaAmigos.Where(p => p.id == id).First());
        }
    }
}