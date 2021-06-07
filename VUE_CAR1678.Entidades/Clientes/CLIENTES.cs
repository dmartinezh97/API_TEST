using System.ComponentModel.DataAnnotations;

namespace Entidades.Clientes {
    public partial class CLIENTES {
        [Key]
        public string CODCLI { get; set; }
        public string NOMCLI { get; set; }
        public string CODREP { get; set; }
        public string CODPAIS { get; set; }
        public string E_MAIL { get; set; }
        public string TELCLI { get; set; }
        public string NIFCLI { get; set; }
        public string DIRCLI1 { get; set; }
        public string POBCLI { get; set; }
        public string CODPROVI { get; set; }
        public string OBSOLETO { get; set; }
        public string BLOQUEADO { get; set; }
    }
}
