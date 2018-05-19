using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Windows.Media.Imaging;

namespace mvc.ViewModels
{
    public class RehauExcelMetadataViewModel
    {
        public string Proveedor { get; set; }
        public RehauHeaderEmpresaMetadataViewModel Empresa { get; set; }
        public RehauHeaderClienteMetadataViewModel Cliente { get; set; }
        public string ParrafoIntroductorio { get; set; }
        public IEnumerable<RehauItemMetadataViewModel> Items { get; set; }
        public RehauResumenMetadataViewModel Resumen { get; set; }
    }

    public class RehauHeaderEmpresaMetadataViewModel
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string CpLocalidadProvincia { get; set; }
        public string TelefonoFax { get; set; }
        public string Email { get; set; }
    }

    public class RehauHeaderClienteMetadataViewModel
    {
        public string Nombre { get; set; }
        public string NumeroPresupuesto { get; set; }
        public string NombreProyecto { get; set; }
        public string Version { get; set; }
        public string UbicacionProyecto { get; set; }
        public string Fecha { get; set; }
        public string AtencionA { get; set; }
    }

    public class RehauItemMetadataViewModel
    {
        public RehauItemShapeMetadataViewModel Tipologia { get; set; }
        public string NumeroItem { get; set; }
        public string Posicion { get; set; }
        public string Color { get; set; }
        public string HeaderDescripcion { get; set; }
        public string Descripcion { get; set; }
        public string HeaderUnitario { get; set; }
        public string HeaderUnidades { get; set; }
        public string HeaderTotal { get; set; }
        public string PrecioUnitario { get; set; }
        public string Unidades { get; set; }
        public string PrecioTotal { get; set; }
    }

    public class RehauItemShapeMetadataViewModel
    {
        public float Top { get; set; }
        public float Left { get; set; }
        public string Name { get; set; }
        public Bitmap Image { get; set; }
    }

    public class RehauResumenMetadataViewModel
    {
        public string TotalNeto { get; set; }
        public string TotalUnidades { get; set; }
        public string TotalM2 { get; set; }
        public string TotalML { get; set; }
        public string Iva { get; set; }
        public string TotalProyecto { get; set; }
    }
}