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
    
    public partial class tblPerfilItens
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblPerfilItens()
        {
            this.tblMatrizFuncXAtividades = new HashSet<tblMatrizFuncXAtividades>();
            this.tblMatrizFuncXAtividadesTemp = new HashSet<tblMatrizFuncXAtividadesTemp>();
            this.tblMatrizFuncXTreinamento = new HashSet<tblMatrizFuncXTreinamento>();
            this.tblMatrizFuncXTreinamentoTemp = new HashSet<tblMatrizFuncXTreinamentoTemp>();
            this.tblPerfilAtividadeXPerfilAtItem = new HashSet<tblPerfilAtividadeXPerfilAtItem>();
            this.tblPerfilTreinamentoxPerfilItem = new HashSet<tblPerfilTreinamentoxPerfilItem>();
        }
    
        public int IdPerfilItem { get; set; }
        public string Sigla { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblMatrizFuncXAtividades> tblMatrizFuncXAtividades { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblMatrizFuncXAtividadesTemp> tblMatrizFuncXAtividadesTemp { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblMatrizFuncXTreinamento> tblMatrizFuncXTreinamento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblMatrizFuncXTreinamentoTemp> tblMatrizFuncXTreinamentoTemp { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPerfilAtividadeXPerfilAtItem> tblPerfilAtividadeXPerfilAtItem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPerfilTreinamentoxPerfilItem> tblPerfilTreinamentoxPerfilItem { get; set; }
    }
}
