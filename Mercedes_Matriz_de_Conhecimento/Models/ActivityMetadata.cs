﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mercedes_Matriz_de_Conhecimento.Models
{
    public class ActivityMetadata
    {
        public int idAtividade { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Sigla { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public string UsuarioCriacao { get; set; }

        public System.DateTime DataCriacao { get; set; }

        [Display(Name = "Grupo de Atividades")]
        public bool IndicaGrupoDeAtividades { get; set; }

        [Required]
        [Display(Name = "Perfil de Atividades")]
        public Nullable<int> idPerfilAtividade { get; set; }

        [Display(Name = "Tipo Equipamento GSA")]
        public Nullable<int> idTipoEquipamentoGSA { get; set; }
    }
}