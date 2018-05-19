using AutoMapper;
using ceya.Core.Helpers;
using ceya.Model.Models;
using mvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        public ViewModelToDomainMappingProfile()
        {
            CreateMap<PresupuestoFormModel, Presupuesto>()
                .ForMember(d => d.Codigo, opt => opt.MapFrom(src => Codificable.ParseCodigo(src.Codigo)))
                .ForMember(d => d.ColocacionNombre, opt => opt.MapFrom(s => s.NombreColocacion))
                .ForMember(d => d.PresupuestoItem, opt => opt.MapFrom(s => s.Items));
            CreateMap<PresupuestoItemFormModel, PresupuestoItem>()
                .ForMember(d => d.ArchivoTipologiaId, opt => opt.MapFrom(src => src.TipologiaId));
            CreateMap<ProductoFormModel, Producto>()
               .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.Codigo ));
            CreateMap<ListaPrecioFormModel, ListaPrecio>()
                           .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.Codigo));
            CreateMap<PrecioFormModel, Precio>()
                          .ForMember(d => d.FechaDesde, opt => opt.MapFrom(src => src.Desde))
              .ForMember(d => d.FechaHasta, opt => opt.MapFrom(src => src.Hasta))
              .ForMember(d => d.PrecioProducto, opt => opt.MapFrom(src => src.Precio));
        }
    }
}