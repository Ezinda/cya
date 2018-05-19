using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace mvc.ViewModels
{
    public class AlcemarExcelMetadataViewModel
    {
        public string Proveedor { get; set; }
        public string Fecha { get; set; }
        public AlcemarHeaderEmpresaMetadataViewModel Empresa { get; set; }
        public AlcemarHeaderClienteMetadataViewModel Cliente { get; set; }
        public IEnumerable<AlcemarItemMetadataViewModel> Items { get; set; }
        public AlcemarResumenMetadataViewModel Resumen { get; set; }
    }

    public class AlcemarHeaderEmpresaMetadataViewModel
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string LocalidadProvincia { get; set; }
        public string Contacto { get; set; }
    }

    public class AlcemarHeaderClienteMetadataViewModel
    {
        public string Nombre { get; set; }
        public string Obra { get; set; }
        public string NumeroPresupuesto { get; set; }
        public string Tratamiento { get; set; }
    }

    public class AlcemarItemMetadataViewModel
    {
        public AlcemarItemShapeMetadataViewModel Tipologia { get; set; }
        public string Posicion { get; set; }
        public string Cantidad { get; set; }
        public string Descripcion { get; set; }

        public string HeaderCarpinteria { get; set; }
        public string UnitarioCarpinteria { get; set; }
        public string TotalCarpinteria { get; set; }

        public string HeaderVidrios { get; set; }
        public string UnitarioVidrios { get; set; }
        public string TotalVidrios { get; set; }

        public string HeaderTapajuntas { get; set; }
        public string UnitarioTapajuntas { get; set; }
        public string TotalTapajuntas { get; set; }

        public string HeaderColocacion { get; set; }
        public string UnitarioColocacion { get; set; }
        public string TotalColocacion { get; set; }

        public string Unitario { get; set; }
        public string Total { get; set; }
    }

    public class AlcemarItemShapeMetadataViewModel
    {
        public float Top { get; set; }
        public float Left { get; set; }
        public string Name { get; set; }
        public Bitmap Image { get; set; }
    }

    public class AlcemarResumenMetadataViewModel
    {
        public string TotalCarpinteria { get; set; }
        public string TotalVidrios { get; set; }
        public string TotalContramarcos { get; set; }
        public string TotalColocacion { get; set; }
        public string HeaderPrecio { get; set; }
        public string Precio { get; set; }
        public string HeaderIva { get; set; }
        public string Iva { get; set; }
        public string HeaderFinal { get; set; }
        public string Final { get; set; }
        public string LapsoValidez { get; set; }
        public string PlazoEntrega { get; set; }
    }
}