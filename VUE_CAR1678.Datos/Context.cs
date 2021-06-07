using Microsoft.EntityFrameworkCore;
using System;
using Datos.Mapping.Cliente;
using Entidades.Clientes;

namespace VUE_CAR1678.Datos
{
    public class Context : DbContext
    {
        public DbSet<CLIENTES> CLIENTES { get; set; }


        public Context(DbContextOptions<Context> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CODCLI());
            
            //Funciones
            //modelBuilder.Entity<AT_HIST_FACT_REP_CLI>(e => e.HasNoKey());
            //modelBuilder.Entity<ATDouble>(e => e.HasNoKey());
            //modelBuilder.Entity<ATFloat>(e => e.HasNoKey());
            //modelBuilder.Entity<ComparativaVentasAnyoCliente>(e => e.HasNoKey());

            base.OnModelCreating(modelBuilder);
        }

        //Esto no se ejecuta, pasa directamente a la llamada SQL
        //public float AT_DESCOMPTE(string CODART, string CODCLI, float cantidad)  => throw new NotSupportedException();

    }
}