//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mercedes_Matriz_de_Conhecimento
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblMatrizFuncTreinHistorico
    {
        public int idMatrizFuncTreinHistorico { get; set; }
        public int idMatrizWorkzoneHistorico { get; set; }
        public int idFuncionario { get; set; }
        public string nomeFuncionario { get; set; }
        public string REFuncionario { get; set; }
        public string BUFuncionario { get; set; }
        public int idTreinamento { get; set; }
        public string nomeTreinamento { get; set; }
        public int idTipoTreinamento { get; set; }
        public string nomeTipoTreinamento { get; set; }
        public string siglaTipoTreinamento { get; set; }
        public int idItemPerfil { get; set; }
        public string siglaItemPerfil { get; set; }
        public string cor { get; set; }
    
        public virtual tblMatrizWorkzoneHistorico tblMatrizWorkzoneHistorico { get; set; }
    }
}
