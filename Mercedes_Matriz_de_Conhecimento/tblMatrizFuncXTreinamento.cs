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
    
    public partial class tblMatrizFuncXTreinamento
    {
        public int idMatrizFuncTrein { get; set; }
        public int idMatrizWorkzone { get; set; }
        public int idFuncionario { get; set; }
        public int idTreinamento { get; set; }
        public int idItemPerfil { get; set; }
    
        public virtual tblFuncionarios tblFuncionarios { get; set; }
        public virtual tblMatrizWorkzone tblMatrizWorkzone { get; set; }
        public virtual tblPerfilItens tblPerfilItens { get; set; }
        public virtual tblTreinamento tblTreinamento { get; set; }
    }
}
