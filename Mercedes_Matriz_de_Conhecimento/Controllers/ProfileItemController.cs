﻿using Mercedes_Matriz_de_Conhecimento.Models;
using Mercedes_Matriz_de_Conhecimento.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Http;
using System.Data.Entity;
using System.Net;
using System.Configuration;
using Mercedes_Matriz_de_Conhecimento.Helpers;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class ProfileItemController : Controller
    {


        private ProfileItemService _ProfileItem;


        public ProfileItemController()
        {
            //Pega o nome do usuário para exibir na barra de navegação
            SistemaApi username = new SistemaApi();

            try
            {

                username = AuthorizationHelper.GetSystem();
                ViewBag.User = username.Usuario.ChaveAmericas;
                if (username != null)
                {
                    var imgUser = AuthorizationHelper.GetUserImage(username.Usuario.ChaveAmericas);
                    ViewBag.UserPhoto = imgUser;
                }
            }
            catch
            {
                var imgUser = AuthorizationHelper.GetUserImage("");

                ViewBag.User = "";
                ViewBag.UserPhoto = imgUser;
            }

            _ProfileItem = new ProfileItemService();

        }

        // GET: profileItem
        //[AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.ItemdeTreinamento, Feature = FeaturesHelper.Consultar)]
        public ActionResult Index(int page = 1)
        {
            var pages_quantity = Convert.ToInt32(ConfigurationManager.AppSettings["pages_quantity"]);

            IEnumerable<tblPerfilItens> profileItem;
            profileItem = _ProfileItem.GetProfileItensWithPagination(page, pages_quantity);

            return View(profileItem);

        }

        //[AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.ItemdeTreinamento, Feature = FeaturesHelper.Editar)]
        public ActionResult Create()
        {

            return View("Create");
        }

        //GET: Activity/Details/5
        //[AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.ItemdeTreinamento, Feature = FeaturesHelper.Editar)]
        public ActionResult Details(int id)
        {

            tblPerfilItens profileItem;
            profileItem = _ProfileItem.GetProfileItemById(id);

            if (profileItem == null)
                return View("Index");

            return View("Edit", profileItem);
        }


        [HttpPost]
        public ActionResult Create(tblPerfilItens profileItem)
        {
            var exits = _ProfileItem.checkIfProfileItemAlreadyExits(profileItem);
            profileItem.Tipo = "T";

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _ProfileItem.CreateProfileItem(profileItem);

                    return RedirectToAction("Index");
                }

            }

            if (exits)
                ModelState.AddModelError("Nome", "Perfil de Treinamento já existe");

            return View("Create", profileItem);
        }


        // GET: Activity/Edit/5
        [HttpPost]
        public ActionResult Edit(tblPerfilItens profileItem, int id)
        {
            profileItem.IdPerfilItem = id;
            var exits = _ProfileItem.checkIfProfileItemAlreadyExits(profileItem);


            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _ProfileItem.UpdateProfileItem(profileItem);

                    return RedirectToAction("Index");
                }

            }
            if (exits)
                ModelState.AddModelError("Nome", "Perfil de Treinamento já existe");

            return View(profileItem);
        }


        // GET: profileItem/Delete/5
        //[AccessHelper(Menu = MenuHelper.VisualizacaoCadastro, Screen = ScreensHelper.ItemdeTreinamento, Feature = FeaturesHelper.Excluir)]
        public ActionResult Delete(int id)
        {

            try
            {
                _ProfileItem.DeleteProfileItem(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("conflicted with the REFERENCE constraint "))
                    ViewBag.Erro = "Você não pode executar essa ação, pois essa informação está em uso";
                return View("Error");
            }
        }


    }
}
