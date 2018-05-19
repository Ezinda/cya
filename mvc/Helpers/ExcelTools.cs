using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Excel = Microsoft.Office.Interop.Excel;

namespace mvc.Helpers
{
    public class ExcelTools
    {
        public static Excel.Workbook OpenBook(Excel.Application excelInstance, string fileName, bool readOnly = false, Excel.XlCorruptLoad corruptLoad = Excel.XlCorruptLoad.xlRepairFile)
        {
            Excel.Workbook book = excelInstance.Workbooks.Open(
                fileName, Type.Missing, readOnly,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, corruptLoad);
            return book;
        }

        public static void ReleaseRCM(object o)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            }
            catch
            {
            }
            finally
            {
                o = null;
            }
        }
    }
}