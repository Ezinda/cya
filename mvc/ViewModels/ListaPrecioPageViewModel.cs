using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc.ViewModels
{
    public class ListaPrecioPageViewModel
    {
        public IEnumerable<ListaPrecioListViewModel> List { get; set; }

        public string SearchString { get; set; }

        public IEnumerable<SelectListItem> FilterBy { get; set; }
        public string FilterByString { get; set; }

        public IEnumerable<SelectListItem> SortBy { get; set; }
        public string SortByString { get; set; }
        public bool IsSortAsc { get; set; }

        public ListaPrecioPageViewModel(string selectedFilter, string selectedSort)
        {
            FilterBy = new SelectList(new[]{
                       new SelectListItem{ Text = "All", Value = "All"}
            }, "Text", "Value", selectedFilter);

            FilterByString = selectedFilter;

            SortBy = new SelectList(new[] {
                       new SelectListItem{ Text = "Código", Value = "Codigo" }
                       }, "Text", "Value", selectedSort.Replace("_desc", ""));

            SortByString = selectedSort.Replace("_desc", "");

            IsSortAsc = selectedSort.Contains("_desc");
        }
    }
}