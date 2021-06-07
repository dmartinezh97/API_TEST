namespace VUE_CAR1678.Web.Models.Almacen
{
    public class PaginacionExtensions
    {
        public int page { get; set; } = 1;
        private int cantidadRegistrosPorPagina { get; set; } = 5;
        private readonly int cantidadMaximaRegistrosPorPagina = 50;

        public int itemsInPage
        {
            get => cantidadRegistrosPorPagina;
            set
            {
                cantidadRegistrosPorPagina = (value > cantidadMaximaRegistrosPorPagina) ? cantidadMaximaRegistrosPorPagina : value;
            }
        }
    }
}