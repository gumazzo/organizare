﻿using OpinemBem.DataAccess;
using OpinemBem.Models;
using System;
using System.Web.Mvc;

namespace OpinemBem.WebUI.Controllers
{
    [Authorize]
    public class LeisController : Controller
    {
        public ActionResult AceitarLeisAdm()
        {
            var lst = new ProjetoDeLeiDAO().BuscarNaoPublicados();
            return View(lst);
        }

        public ActionResult ConcordaLeisU(int id)
        {
            var lei = new ProjetoDeLeiDAO().BuscarPorId(id);
            lei.Voto = new VotoDAO().BuscarVoto(id, ((Usuario)User).Id);
            lei.Comentarios = new ComentarioDAO().BuscarPorProjeto(id);
            return View(lei);
        }

        public ActionResult GerenciarLeisAdm(int id)
        {
            var lei = new ProjetoDeLeiDAO().BuscarPorId(id);
            lei.Voto = new VotoDAO().BuscarVoto(id, ((Usuario)User).Id);
            lei.Comentarios = new ComentarioDAO().BuscarPorProjeto(id);
            return View(lei);
        }

        public ActionResult SalvarLeisAdm(ProjetoDeLei obj)
        {
            new ProjetoDeLeiDAO().Inserir(obj);
            return RedirectToAction("AceitarLeisAdm", "Leis");
        }

        public ActionResult EnviarPost(Comentario obj)
        {
            obj.DataHora = DateTime.Now;
            obj.Usuario = new Usuario() { Id = ((Usuario)User).Id };
            new ComentarioDAO().Inserir(obj);

            return RedirectToAction("ConcordaLeisU", "Leis", new { @id = obj.ProjetoDeLei.Id });
        }
    }
}