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
    
    public partial class tblMatrizFuncXAtividadesTemp
    {
        public int idMatrizFuncAtivTemp { get; set; }
        public int idMatrizWorkzoneTemp { get; set; }
        public int idFuncionario { get; set; }
        public int idAtividade { get; set; }
        public int idItemPerfil { get; set; }
        public string alocacaoForcada { get; set; }
        public string cor { get; set; }
    
        public virtual tblAtividades tblAtividades { get; set; }
        public virtual tblFuncionarios tblFuncionarios { get; set; }
        public virtual tblMatrizWorkzoneTemp tblMatrizWorkzoneTemp { get; set; }
        public virtual tblPerfilItens tblPerfilItens { get; set; }
    }
}
