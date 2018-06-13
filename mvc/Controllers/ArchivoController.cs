using ceya.Domain.Service;
using ceya.Infrastructure.DataAccess;
using mvc.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc.Controllers
{
    public class ArchivoController : Controller
    {
        private GestionComercialWebEntities db = new GestionComercialWebEntities();
        private readonly IArchivoService _archivoService;
        public ArchivoController(IArchivoService archivoService)
        {
            _archivoService = archivoService;
        }

        public FileResult Get(Guid id)
        {
            var archivo = this.db.Archivo.Find(id);

            return File(archivo.Ubicacion, archivo.MimeType);
        }

        public JsonResult Eliminar(Guid id)
        {            
            var exito = true;
            var mensaje = "";
            try
            {
                _archivoService.Eliminar(id);
            }
            catch (Exception ex)
            {
                exito = false;
                mensaje = ex.Message;
            }
            var respuesta = new
            {
                exito = exito,
                mensaje = mensaje
            };
            return Json(respuesta, JsonRequestBehavior.AllowGet);

        }

        public FileResult GetThumbnail(Guid id, int sizeClass)
        {
            var archivo = this.db.Archivo.Find(id);
            var file = new FileInfo(archivo.Ubicacion);
            var thumbnailDir =
                Constants.ThumbnailSizes[sizeClass].Width
                + "x"
                + Constants.ThumbnailSizes[sizeClass].Height;

            string  storeDirectory = new DirectoryInfo(Constants.PathTipologia).ToString();

            var path = Path.Combine(storeDirectory, file.Name);

            if (!System.IO.File.Exists(path))
            {
                Thumbnail.GenerateThumbnail(new FileInfo(archivo.Ubicacion), Constants.ThumbnailSizes[sizeClass], new DirectoryInfo(Constants.PathTipologiaThumbnailDirs[sizeClass]));
            }

            return File(path, archivo.MimeType);
        }
    }
}
