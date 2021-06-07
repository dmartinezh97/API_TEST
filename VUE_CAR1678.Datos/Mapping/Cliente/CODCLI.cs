using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entidades.Clientes;

namespace Datos.Mapping.Cliente {
    public class CODCLI : IEntityTypeConfiguration<CLIENTES> {
        public void Configure(EntityTypeBuilder<CLIENTES> builder) {
            builder.ToView("CLIENTES")
                .HasKey(x => x.CODCLI);
        }
    }
}
