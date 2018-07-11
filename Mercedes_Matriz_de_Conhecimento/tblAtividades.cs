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
    
    public partial class tblAtividades
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblAtividades()
        {
            this.tblAtividadeXTreinamentos = new HashSet<tblAtividadeXTreinamentos>();
            this.tblWorkzoneXAtividades = new HashSet<tblWorkzoneXAtividades>();
        }
    
        public int idAtividade { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public string Descricao { get; set; }
        public string UsuarioCriacao { get; set; }
        public System.DateTime DataCriacao { get; set; }
        public bool IndicaGrupoDeAtividades { get; set; }
        public Nullable<int> idPerfilAtividade { get; set; }
        public Nullable<int> idTipoEquipamentoGSA { get; set; }
    
        public virtual tblPerfilAtividade tblPerfilAtividade { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAtividadeXTreinamentos> tblAtividadeXTreinamentos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblWorkzoneXAtividades> tblWorkzoneXAtividades { get; set; }
    }
}