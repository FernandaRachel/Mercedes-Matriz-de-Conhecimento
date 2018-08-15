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
using PagedList;
using System.Configuration;
using DCX.ITLC.AutSis.Services.Integracao;

namespace Mercedes_Matriz_de_Conhecimento.Controllers
{
    public class WorkzoneController : Controller
    {


        private WorkzoneService _workzone;
        private EmployeeService _employee;
        private WorzoneXEmployeeService _workzoneXemployee;

        public WorkzoneController()
        {
            _workzone = new WorkzoneService();
            _employee = new EmployeeService();
            _workzoneXemployee = new WorzoneXEmployeeService();

        }

        // GET: workzone
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            var pages_quantity = Convert.ToInt32(ConfigurationManager.AppSettings["pages_quantity"]);

            IPagedList<tblWorkzone> workzone;
            workzone = _workzone.GetWorkzonesWithPagination(page, pages_quantity);

            return View(workzone);

        }

        public ActionResult Create()
        {
            IEnumerable<tblWorkzone> workzone;
            workzone = _workzone.GetWorkzones();
            ViewData["Workzone"] = workzone;


            var innerX = new List<SelectListItem>();
            SelectListItem innerXItem = new SelectListItem { Selected = false, Text = "BU_TESTE", Value = "1" };
            SelectListItem innerXItem2 = new SelectListItem { Selected = false, Text = "BU_TESTE2", Value = "2" };
            innerX.Insert(0, innerXItem);
            innerX.Insert(0, innerXItem2);
            SelectList BU = new SelectList(innerX, "Value", "Text");

            var listaCC = new List<SelectListItem>();
            SelectListItem itemCC = new SelectListItem { Selected = false, Text = "CC_TESTE", Value = "1" };
            SelectListItem itemCC2 = new SelectListItem { Selected = false, Text = "CC_TESTE2", Value = "2" };
            listaCC.Insert(0, itemCC);
            listaCC.Insert(0, itemCC2);
            SelectList CC = new SelectList(listaCC, "Value", "Text");

            var listaLinha = new List<SelectListItem>();
            SelectListItem itemLinha = new SelectListItem { Selected = false, Text = "LINHA_TESTE", Value = "1" };
            listaLinha.Insert(0, itemLinha);
            SelectList Linha = new SelectList(listaLinha, "Value", "Text");

            ViewData["BU"] = BU;
            ViewData["CC"] = CC;
            ViewData["LINHA"] = Linha;


            return View("Create");
        }

        [OutputCache(Duration = 1)]
        public ActionResult SearchUser(int idWorkzone, string nome = "")
        {
            ModelState.Clear();
            tblWorkzone workzone;
            workzone = _workzone.GetWorkzoneById(idWorkzone);

            /* SELECIONA FUNCIONÁRIOS ADICIONADOS NESSA WZ*/
            IEnumerable<tblFuncionarios> employeeAdded;
            employeeAdded = _workzone.setUpEmployees(idWorkzone);
            IEnumerable<tblFuncionarios> funcFiltrados;
            funcFiltrados = _employee.GetEmployeeByName(nome);

            ViewBag.Name = nome;
            ViewData["Funcionarios"] = funcFiltrados;
            ViewData["FuncionariosAdicionados"] = employeeAdded;

            FuncListModel Func = new FuncListModel();

            Func.idWorkzone = idWorkzone;
            Func.funcionariosAdded = employeeAdded;
            Func.funcionarios = funcFiltrados;
            UpdateModel(Func);

            //return RedirectToAction("Details", new { id = workzone.IdWorkzone, nome = nome });

            return PartialView("_Lista", Func);
        }

        //GET: workzone/Details/5
        public ActionResult Details(int id, string nome = "")
        {
            FuncListModel Func = new FuncListModel();
            Func.idWorkzone = id;
            var x = ViewBag.Name;
            
            ModelState.Clear();
            ViewData.Clear();
            UpdateModel(ViewData);
            /*  MONTANDO SELECT LIST BU, CC E LINHA*/
            var innerX = new List<SelectListItem>();
            SelectListItem innerXItem = new SelectListItem { Selected = false, Text = "BU_TESTE", Value = "1" };
            SelectListItem innerXItem2 = new SelectListItem { Selected = false, Text = "BU_TESTE2", Value = "2" };
            innerX.Insert(0, innerXItem);
            innerX.Insert(0, innerXItem2);
            SelectList BU = new SelectList(innerX, "Value", "Text");

            var listaCC = new List<SelectListItem>();
            SelectListItem itemCC = new SelectListItem { Selected = false, Text = "CC_TESTE", Value = "1" };
            SelectListItem itemCC2 = new SelectListItem { Selected = false, Text = "CC_TESTE2", Value = "2" };
            listaCC.Insert(0, itemCC);
            listaCC.Insert(0, itemCC2);
            SelectList CC = new SelectList(listaCC, "Value", "Text");

            var listaLinha = new List<SelectListItem>();
            SelectListItem itemLinha = new SelectListItem { Selected = false, Text = "LINHA_TESTE", Value = "1" };
            listaLinha.Insert(0, itemLinha);
            SelectList Linha = new SelectList(listaLinha, "Value", "Text");

            ViewData["BU"] = BU;
            ViewData["CC"] = CC;
            ViewData["LINHA"] = Linha;

            /*FINALIZANDO SELECT LISTA BU, CC E LINHA*/

            tblWorkzone workzone;
            workzone = _workzone.GetWorkzoneById(id);
            /*SELECIONA FUNCIONÁRIOS EXISTENTES*/
            IEnumerable<tblFuncionarios> employee;
            if (nome.Length > 0)
                employee = _employee.GetEmployeeByName(nome);
            else
                employee = _employee.GetEmployees();
            Func.funcionarios = employee;
            ViewData["Funcionarios"] = employee;
            
            /* SELECIONA FUNCIONÁRIOS ADICIONADOS NESSA WZ*/
            IEnumerable<tblFuncionarios> employeeAdded;
            employeeAdded = _workzone.setUpEmployees(id);
            Func.funcionariosAdded = employeeAdded;

            ViewData["FuncionariosAdicionados"] = employeeAdded;

            if (workzone == null)
                return View("Index");

            return View("Edit", workzone);
        }

        public ActionResult Push(int id, int idwz)
        {
            tblWorkzoneXFuncionario infoToPush = new tblWorkzoneXFuncionario();

            infoToPush.idFuncionario = id;
            infoToPush.idWorkzone = idwz;

            var exists = _workzoneXemployee.checkIfWorzoneXEmployeeAlreadyExits(infoToPush);

            if (ModelState.IsValid && !exists)
                _workzoneXemployee.CreateWorzoneXEmployee(infoToPush);

            return RedirectToAction("Details", new { id = idwz });
        }

        public ActionResult Pop(int id, int idwz)
        {
            tblWorkzoneXFuncionario wzXemployee = _workzoneXemployee.GetWorzoneXEmployeeById(idwz, id);

            if (ModelState.IsValid)
                _workzoneXemployee.DeleteWorzoneXEmployee(wzXemployee.idWorkzoneFuncionario);

            var workzone = _workzone.GetWorkzoneById(idwz);


            return RedirectToAction("Details", new { id = idwz });
        }


        // GET: workzone/Create
        [HttpPost]
        public ActionResult Create(tblWorkzone workzone)
        {
            var exits = _workzone.checkIfWorkzoneAlreadyExits(workzone);
            workzone.UsuarioCriacao = "Teste Sem Seg";
            workzone.DataCriacao = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    workzone.FlagAtivo = true;

                    var returnedElement = _workzone.CreateWorkzone(workzone);

                    return RedirectToAction("Details", new { id = returnedElement.IdWorkzone });

                }

            }

            if (exits)
                ModelState.AddModelError("Nome", "Workzone já existe");

            return View("Create");
        }


        // GET: workzone/Edit/5
        [HttpPost]
        public ActionResult Edit(tblWorkzone workzone, int id)
        {
            workzone.IdWorkzone = id;
            var exits = _workzone.checkIfWorkzoneAlreadyExits(workzone);

            workzone.UsuarioAlteracao = "Usuário Teste Edit";
            workzone.DataAlteracao = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (!exits)
                {
                    _workzone.UpdateWorkzone(workzone);

                    return RedirectToAction("Index");
                }

            }
            return View(workzone);
        }


        // GET: workzone/Delete/5
        public ActionResult Delete(int id)
        {

            _workzone.DeleteWorkzone(id);

            return RedirectToAction("Index");

        }


    }
}
