using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VUE_CAR1678.Datos;
using System.Linq;
using System.Threading.Tasks;
using VUE_CAR1678.Web.Helpers;
using VUE_CAR1678.Web.Models.Product;
using VUE_CAR1678.Web.Models.Category;
using System.Collections.Generic;
using VUE_CAR1678.Entidades.Categoria;
using VUE_CAR1678.Web.Models.Language;
using System;
using VUE_CAR1678.Web.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace VUE_CAR1678.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly Context _context;

        public CategoriesController(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna la categoría filtrada con sus idiomas. parameters.Id = "abrigos_chubasqueros_perros"
        /// </summary>
        /// <param name="parameters">Ejemplo: parameters.Id = "abrigos_chubasqueros_perros"</param>
        /// <returns>CategoryViewModel</returns>
        /// <response code="500">Server Error</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        // GET: api/Categories/Listar
        [HttpGet]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CategoryViewModel))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Listar([FromQuery] CategoryParametersViewModel parameters)
        {
            var queryable = _context.Magento_Categories.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Id))
            {
                queryable = queryable.Where(x => x.Id == parameters.Id);
            }

            if (parameters.Display.HasValue)
            {
                queryable = queryable.Where(x => x.Display == parameters.Display);
            }

            double totalPages = await HttpContext.InsertarParametrosPaginacion(queryable,
                parameters.itemsInPage);

            var categories = await queryable.Paginar(parameters.page, parameters.itemsInPage).ToListAsync();

            /////
            ResponseViewModel response = new ResponseViewModel();
            response.response = await ObtenerCategoriasViewModel(categories);
            response.page = parameters.page;
            response.itemsInPage = parameters.itemsInPage;
            response.totalPages = Convert.ToInt32(totalPages);
            ///

            var resp = await ObtenerCategoriasViewModel(categories);
            return Ok(resp);
        }

        /// <summary>
        /// Desmarca el campo actualizado por el id de la Categoría.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<bool> UncheckUpdatedFieldByCategory(string id)
        {
            return true;
        }

        /// <summary>
        /// Desmarca campo actualizado con una lista de IDs de la Categoría.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<bool> UncheckUpdatedFieldByListCategory(List<string> ids)
        {
            return true;
        }

        private async Task<List<CategoryViewModel>> ObtenerCategoriasViewModel(List<Magento_Category> entities)
        {
            List<CategoryViewModel> resp = new List<CategoryViewModel>();

            foreach (var category in entities)
            {
                CategoryViewModel item = new CategoryViewModel(category.Id, category.Display, category.IdMagento);

                var queryable = _context.Magento_CategoriesLanguage.AsQueryable();

                queryable = queryable.Where(x => x.AT_IDCAT == category.Id);

                var languages = await queryable.ToListAsync();

                item.languages = ObtenerIdiomas(languages);
                resp.Add(item);
            }

            return resp;
        }

        private List<LanguageViewModel> ObtenerIdiomas(List<Magento_CategoryLanguage> entity)
        {
            List<LanguageViewModel> resp = new List<LanguageViewModel>();

            for (int i = 1; i <= (int)IDIOMAS.TOTAL; i++)
            {
                string codigo = Convert.ToString((IDIOMAS)i);
                string valor = (string)entity.ValorNombrePropiedad(codigo.ToString());
                LanguageViewModel language = new LanguageViewModel(codigo, valor);
                resp.Add(language);
            }

            return resp;
        }
    }
}