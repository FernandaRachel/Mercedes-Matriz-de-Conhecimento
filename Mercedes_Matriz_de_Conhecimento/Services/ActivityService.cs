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
    public class ActivityService : IActivityService
    {

        public DbConnection _db = new DbConnection();
        public ActivityGroupService _activityGroup = new ActivityGroupService();



        public tblAtividades GetActivityById(int id)
        {
            tblAtividades activity;

            var query = from f in _db.tblAtividades
                        where f.idAtividade == id
                        orderby f.Nome
                        select f;

            activity = query.FirstOrDefault();

            return activity;
        }

        public IEnumerable<tblAtividades> GetActivityByName(string Nome, int idWz)
        {
            List<tblAtividades> activity = new List<tblAtividades>();

            //Se o nome veio vazio traz todas Atividades
            if (Nome.Count() == 0)
                return GetActivitiesNotAdded(idWz);

            if (idWz != 0)
            {

                var query = from f in _db.tblAtividades
                            where f.Nome.Contains(Nome)
                            orderby f.Nome ascending
                            select f;


                foreach (var q in query)
                {
                    var returned = _db.tblWorkzoneXAtividades
                        .Where(wXa => wXa.idAtividade == q.idAtividade && wXa.idWorkzone == idWz);
                    if (returned.Count() == 0)
                        activity.Add(q);
                }
            }

            return activity;
        }

        public IEnumerable<tblAtividades> GetActivities()
        {
            IEnumerable<tblAtividades> activity;

            var query = from f in _db.tblAtividades
                        orderby f.Nome ascending
                        select f;

            activity = query.AsEnumerable();

            return activity.AsEnumerable();
        }

        public IEnumerable<tblAtividades> GetActivitiesNotAdded(int idWz)
        {
            List<tblAtividades> activity = new List<tblAtividades>();

            if (idWz != 0)
            {

                var query = from f in _db.tblAtividades
                            orderby f.Nome ascending
                            select f;


                foreach (var q in query)
                {
                    var returned = _db.tblWorkzoneXAtividades
                        .Where(wXa => wXa.idAtividade == q.idAtividade && wXa.idWorkzone == idWz);
                    if (returned.Count() == 0)
                        activity.Add(q);
                }
            }
            else
            {
                activity = _db.tblAtividades.ToList();
            }

            return activity.AsEnumerable();
        }

        public IEnumerable<tblAtividades> GetActivitiesDifferentFromFatherAndNotAGroup(int id)
        {
            List<tblAtividades> atv = new List<tblAtividades>();

            var query = from f in _db.tblAtividades
                        where f.IndicaGrupoDeAtividades == false
                        && f.idAtividade != id
                        orderby f.Nome ascending
                        select f;

            foreach (var q in query)
            {
                var query2 = _db.tblGrupoAtividades
                    .Where(t => t.idAtividadePai == id && t.idAtividadeFilho == q.idAtividade);

                if (query2.Count() == 0)
                    atv.Add(q);
            }


            return atv;
        }


        public tblAtividades CreateActivity(tblAtividades Activity)
        {
            _db.tblAtividades.Add(Activity);
            _db.SaveChanges();

            var actv = _db.tblAtividades.OrderByDescending(a => a.DataCriacao).FirstOrDefault();

            return actv;
        }

        public tblAtividades DeleteActivity(int id)
        {
            tblAtividades Activity;

            var query = from f in _db.tblAtividades
                        where f.idAtividade == id
                        orderby f.Nome ascending
                        select f;

            Activity = query.FirstOrDefault();

            _db.tblAtividades.Remove(Activity);
            _db.SaveChanges();

            return Activity;
        }


        public tblAtividades UpdateActivity(tblAtividades Activity)
        {
            var trainingToUpdate = _db.tblAtividades.Find(Activity.idAtividade);
            trainingToUpdate.Nome = Activity.Nome;
            trainingToUpdate.Sigla = Activity.Sigla;
            trainingToUpdate.Descricao = Activity.Descricao;
            trainingToUpdate.idPerfilAtividade = Activity.idPerfilAtividade;
            trainingToUpdate.idTipoEquipamentoGSA = Activity.idTipoEquipamentoGSA;
            trainingToUpdate.IndicaGrupoDeAtividades = Activity.IndicaGrupoDeAtividades;

            if (!Activity.IndicaGrupoDeAtividades)
            {
                _activityGroup.DeleteActivityGroupByDaddy(Activity.idAtividade);
            }
            _db.Entry(trainingToUpdate).State = EntityState.Modified;
            _db.SaveChanges();


            return trainingToUpdate;
        }


        public bool checkIfActivityAlreadyExits(tblAtividades Activity)
        {
            var query = from f in _db.tblAtividades
                        where f.Nome == Activity.Nome
                        orderby f.Nome ascending
                        select f;

            if (query.Count() == 1 && query.FirstOrDefault().idAtividade != Activity.idAtividade)
                return true;

            return false;
        }

        public IEnumerable<tblAtividades> GetActivitiesWithPagination(int pageNumber, int quantity)
        {
            IEnumerable<tblAtividades> activity;



            var query = from f in _db.tblAtividades
                        orderby f.Nome ascending
                        select f;

            activity = query.ToPagedList(pageNumber, quantity);

            return activity.AsEnumerable();
        }
    }
}