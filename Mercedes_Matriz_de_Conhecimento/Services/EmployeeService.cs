﻿using Mercedes_Matriz_de_Conhecimento.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using PagedList;

namespace Mercedes_Matriz_de_Conhecimento.Services
{
    public class EmployeeService : IEmployeeService
    {

        public DbConnection _db = new DbConnection();


        public IEnumerable<tblFuncionarios> GetEmployees()
        {

            IEnumerable<tblFuncionarios> employee;



            var query = from f in _db.tblFuncionarios
                        orderby f.Nome ascending
                        select f;

            employee = query.AsEnumerable();
            return employee;
        }

        public tblFuncionarios GetEmployeeById(int id)
        {
            tblFuncionarios employee;

            var query = from f in _db.tblFuncionarios
                        where f.idfuncionario == id
                        orderby f.Nome ascending
                        select f;

            employee = query.FirstOrDefault();

            return employee;
        }

        public IEnumerable<tblFuncionarios> GetEmployeeByName(string Nome, int id)
        {
            List<tblFuncionarios> employee = new List<tblFuncionarios>();
            var wzXfunc = _db.tblWorkzoneXFuncionario.Where(w => w.idWorkzone == id);
            var query = _db.tblFuncionarios;

            if (Nome.Count() == 0)
            {
                foreach (var q in query)
                {
                    var returned = wzXfunc.Where(wXf => wXf.idFuncionario == q.idfuncionario);
                    if (returned.Count() == 0)
                        employee.Add(q);
                }

                return employee;
            }
            else
            {
                var queryCNome = _db.tblFuncionarios
                .Where(f => f.Nome.Contains(Nome));

                foreach (var q in queryCNome)
                {
                    var returned = wzXfunc.Where(wXf => wXf.idFuncionario == q.idfuncionario);
                    if (returned.Count() == 0)
                        employee.Add(q);
                }

                return employee;
            }
        }

        public tblFuncionarios CreateEmployee(tblFuncionarios Employee)
        {
            _db.tblFuncionarios.Add(Employee);

            _db.SaveChanges();


            return Employee;

        }

        public tblFuncionarios DeleteEmployee(int id)
        {
            tblFuncionarios Employee;



            var query = from f in _db.tblFuncionarios
                        where f.idfuncionario == id
                        orderby f.Nome ascending
                        select f;

            Employee = query.FirstOrDefault();

            _db.tblFuncionarios.Remove(Employee);
            _db.SaveChanges();

            return Employee;

        }

        public tblFuncionarios UpdateEmployee(tblFuncionarios Employee)
        {
            var eup = _db.tblFuncionarios.Find(Employee.idfuncionario);
            eup.Nome = Employee.Nome;
            eup.RE = Employee.RE;
            eup.idBu_Origem = Employee.idBu_Origem;
            eup.idAmericas = Employee.idAmericas;
            eup.IdentificadorAuxiliar = Employee.IdentificadorAuxiliar;
            eup.Ativo = Employee.Ativo;
            eup.JustificativaNaoAtivo = Employee.JustificativaNaoAtivo;

            _db.Entry(eup).State = EntityState.Modified;
            _db.SaveChanges();


            return Employee;
        }

        public bool checkIfUserAlreadyExits(tblFuncionarios Employee)
        {
            var query = from f in _db.tblFuncionarios
                        where f.Nome == Employee.Nome
                        orderby f.Nome ascending
                        select f;

            if (query.Count() == 1 && query.FirstOrDefault().idfuncionario != Employee.idfuncionario)
                return true;

            return false;
        }

        public IEnumerable<tblFuncionarios> GetEmployeesWithPagination(int pageNumber, int quantity)
        {
            IEnumerable<tblFuncionarios> employee;



            var query = from f in _db.tblFuncionarios
                        orderby f.Nome ascending
                        select f;

            employee = query.ToPagedList(pageNumber, quantity);
            return employee;
        }
    }
}