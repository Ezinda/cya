using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace mvc.Helpers
{
    public static class Constants
    {
        // public static string PathFileStore = String.Format("~/Uploads/Images/Tipologia");
        //public static string PathFileStore = String.Format("C:/Ceya/Uploads");
        //public static string PathTipologia = PathFileStore + "/Images/Tipologia";//"~/Content/Images/Tipologia";
        //public static string PathExcelsDePresupuesto = PathFileStore + "/Images/ExcelPresupuesto";

        public static string PathFileStore = String.Format("~/Content/Images");
        public static string PathTipologia = PathFileStore + "/Tipologia";
        public static string PathExcelsDePresupuesto = PathFileStore + "/ExcelPresupuesto";

        public static Size[] ThumbnailSizes = new Size[]
        {
            new Size(75, 75),
            new Size(100, 100),
            new Size(125, 125),
            new Size(150, 150),
            new Size(200, 200)
        };
        public static String[] PathTipologiaThumbnailDirs = new String[]
        {
            PathTipologia + "/75x75",
            PathTipologia + "/100x100",
            PathTipologia + "/125x125",
            PathTipologia + "/150x150",
            PathTipologia + "/200x200"
        };
        public static class ListaDePreciosDelSistema
        {
            public static string ColocacionCodigo { get { return "SYS01"; } }
            public static string VidriosCodigo { get { return "SYS02"; } }
        }
    }
}