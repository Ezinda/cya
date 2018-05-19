using ceya.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ceya.Domain.Service
{
    public interface IColorService
    {
        IEnumerable<Color> GetColores();
        IEnumerable<Color> GetColorFilter(string search);
        IPagedList<Color> GetColoresByPage(int currentPage, int noOfRecords, string sortBy, string filterBy, string searchString);
        Color GetColor(Guid Id);
        void Add(Color color);
        void Update(Color color);
        void Delete(Guid id);
        void Save();
        bool GetColorAny(Guid id);
    }
}
