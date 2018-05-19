using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc.ViewModels
{
    public class PresupuestoPageViewModel
    {
        public IEnumerable<PresupuestoListViewModel> List { get; set; }

        public string SearchString { get; set; }

        public IEnumerable<SelectListItem> FilterBy { get; set; }
        public string FilterByString { get; set; }

        public IEnumerable<SelectListItem> SortBy { get; set; }
        public string SortByString { get; set; }
        public bool IsSortAsc { get; set; }

        public PresupuestoPageViewModel(string selectedFilter, string selectedSort)
        {
            FilterBy = new SelectList(new[]{
                       new SelectListItem{ Text = "All", Value = "All"},
                       new SelectListItem{ Text = "Presupuestos Finalizados", Value = "Presupuestos Finalizados"},
                       }, "Text", "Value", selectedFilter);
            FilterByString = selectedFilter;
            SortBy = new SelectList(new[] {
                       new SelectListItem{ Text = "Código", Value = "Codigo" },
                       new SelectListItem{ Text = "Solicita", Value = "Solicita" }
                       }, "Text", "Value", selectedSort.Replace("_desc", ""));
            SortByString = selectedSort.Replace("_desc", "");
            IsSortAsc = selectedSort.Contains("_desc");
        }
    }
}