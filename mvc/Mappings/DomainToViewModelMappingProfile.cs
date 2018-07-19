using AutoMapper;
using ceya.Model.Models;
using ceya.Domain.Model.Extensions;
using mvc.Helpers;
using mvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        public DomainToViewModelMappingProfile()
        {
            CreateMap<Presupuesto, PresupuestoFormModel>()
                .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.GetCodigoConFormato()))
                .ForMember(d => d.NombreCliente, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.ToSearchNameString() : String.Empty))
                .ForMember(d => d.NombreObra, opt => opt.MapFrom(src => src.Obra != null ? src.Obra.ToSearchNameString() : String.Empty))
                .ForMember(d => d.NombreConstructora, opt => opt.MapFrom(src => src.Constructora != null ? src.Constructora.ToSearchNameString() : String.Empty));

            CreateMap<PresupuestoItem, PresupuestoItemFormModel>()
                .ForMember(d => d.TipologiaId, opt => opt.MapFrom(src => src.ArchivoTipologiaId));

            CreateMap<Presupuesto, PresupuestoListViewModel>()
                .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.GetCodigoConFormato()))
                .ForMember(d => d.Fecha, opt => opt.MapFrom(src => src.Fecha.ToShortDateString()))
                .ForMember(d => d.CodigoObra, opt => opt.MapFrom(src => src.Obra != null ? src.Obra.CodigoObra : String.Empty))
                .ForMember(d => d.Obra, opt => opt.MapFrom(src => src.Obra != null ? src.Obra.Nombre : String.Empty))
                .ForMember(d => d.Cliente, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.RazonSocial + src.Cliente.Apellido + ", " + src.Cliente.Nombre : String.Empty))
                .ForMember(d => d.Solicita, opt => opt.MapFrom(src => src.Solicita != null ? src.Solicita : src.Cliente.RazonSocial + src.Cliente.Apellido + ", " + src.Cliente.Nombre))
                .ForMember(d => d.Vendedor, opt => opt.MapFrom(src => src.Vendedor != null ? src.Vendedor : String.Empty))
                .ForMember(d => d.CodigoCategoria, opt => opt.MapFrom(src => src.PresupuestoCategoria != null ? src.PresupuestoCategoria.GetCodigoConFormato() : String.Empty))
                .ForMember(d => d.Estado, opt => opt.MapFrom(src => src.PresupuestoEstado != null ? src.PresupuestoEstado.Descripcion : String.Empty))
                .ForMember(d => d.FechaAlerta, opt => opt.MapFrom(src =>
                src.PresupuestoSeguimiento.Where(x => x.Activo == true).FirstOrDefault() != null ?
                src.PresupuestoSeguimiento.Where(x => x.Activo == true).FirstOrDefault().FechaAlerta.ToShortDateString() : String.Empty));

            CreateMap<Presupuesto, CuboPresupuestoViewModel>()
                .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.GetCodigoConFormato()))
                .ForMember(d => d.Fecha, opt => opt.MapFrom(src => src.Fecha.ToShortDateString()))
                .ForMember(d => d.CodigoObra, opt => opt.MapFrom(src => src.Obra != null ? src.Obra.CodigoObra : String.Empty))
                .ForMember(d => d.Obra, opt => opt.MapFrom(src => src.Obra != null ? src.Obra.Nombre : String.Empty))
                .ForMember(d => d.Cliente, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.RazonSocial + src.Cliente.Apellido + ", " + src.Cliente.Nombre : String.Empty))
                .ForMember(d => d.CodigoCategoria, opt => opt.MapFrom(src => src.PresupuestoCategoria != null ? src.PresupuestoCategoria.GetCodigoConFormato() : String.Empty))
                .ForMember(d => d.Estado, opt => opt.MapFrom(src => src.PresupuestoEstado != null ? src.PresupuestoEstado.Descripcion : String.Empty))
                .ForMember(d => d.FechaAlerta, opt => opt.MapFrom(src =>
                    src.PresupuestoSeguimiento.Where(x => x.Activo == true).FirstOrDefault() != null ?
                    src.PresupuestoSeguimiento.Where(x => x.Activo == true).FirstOrDefault().FechaAlerta.ToShortDateString() : String.Empty));

            CreateMap<PresupuestoSeguimiento, SeguimientoListViewModel>()
                .ForMember(d => d.Estado, opt => opt.MapFrom(src => src.PresupuestoEstado != null ? src.PresupuestoEstado.Descripcion : String.Empty))
                .ForMember(d => d.Fecha, opt => opt.MapFrom(src => src.Fecha.ToShortDateString()))
                .ForMember(d => d.FechaAlerta, opt => opt.MapFrom(src => src.FechaAlerta.ToShortDateString()))
                .ForMember(d => d.Observacion, opt => opt.MapFrom(src => src.Observacion));

            CreateMap<UnidadMedida, UnidadMedidaListViewModel>()
                .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.Codigo));

            CreateMap<Color, ColorListViewModel>()
                .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.Codigo));

            CreateMap<Clase, ClaseListViewModel>()
                .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.Codigo));

            CreateMap<Subrubro, SubrubroListViewModel>()
                .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.Codigo))
                .ForMember(d => d.Clase, opt => opt.MapFrom(src => src.Clase != null ? src.Clase.Descripcion : String.Empty));

            CreateMap<RubroMaestro, RubroMaestroListViewModel>()
             .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.Codigo));

            CreateMap<Rubro, RubroListViewModel>()
                .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.Codigo));

            CreateMap<Producto, ProductoListViewModel>()
                .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.Codigo))
                .ForMember(d => d.CodigoProveedor, opt => opt.MapFrom(src => src.CodigoProveedor))
                .ForMember(d => d.CodigoCompuesto, opt => opt.MapFrom(src => src.CodigoCompuesto))
                .ForMember(d => d.Rubro, opt => opt.MapFrom(src => src.Rubro != null ? src.Rubro.Descripcion : string.Empty))
                .ForMember(d => d.Subrubro, opt => opt.MapFrom(src => src.Subrubro != null ? src.Subrubro.Descripcion : string.Empty))
                .ForMember(d => d.Unidad, opt => opt.MapFrom(src => src.UnidadMedida != null ? src.UnidadMedida.Abreviacion : string.Empty));

            CreateMap<Producto, ProductoFormModel>()
                .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.Codigo))
                .ForMember(d => d.CodigoProveedor, opt => opt.MapFrom(src => src.CodigoProveedor))
                .ForMember(d => d.CodigoCompuesto, opt => opt.MapFrom(src => src.CodigoCompuesto))
                .ForMember(d => d.NombreRubroMaestro, opt => opt.MapFrom(src => src.RubroMaestro != null ? src.RubroMaestro.Descripcion : string.Empty))
                .ForMember(d => d.NombreRubro, opt => opt.MapFrom(src => src.Rubro != null ? src.Rubro.Descripcion : string.Empty))
                .ForMember(d => d.NombreSubrubro, opt => opt.MapFrom(src => src.Subrubro != null ? src.Subrubro.Descripcion : string.Empty))
                .ForMember(d => d.NombreUnidad, opt => opt.MapFrom(src => src.UnidadMedida != null ? src.UnidadMedida.Abreviacion : string.Empty))
                .ForMember(d => d.NombreTipoProducto, opt => opt.MapFrom(src => src.TipoProducto != null ? src.TipoProducto.Descripcion : string.Empty));

            CreateMap<ListaPrecio, ListaPrecioListViewModel>()
               .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.Codigo))
               .ForMember(d => d.Activo, opt => opt.MapFrom(src => src.Activo == true ? "SI" : "NO"));

            CreateMap<ListaPrecio, ListaPrecioFormModel>()
                          .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.Codigo));

            CreateMap<Precio, PrecioListViewModel>()
                .ForMember(d => d.CodigoCompuesto, opt => opt.MapFrom(src => src.Producto.CodigoCompuesto))
                .ForMember(d => d.Descripcion, opt => opt.MapFrom(src => src.Producto.Descripcion))
                .ForMember(d => d.Unidad, opt => opt.MapFrom(src => src.Producto != null ? src.Producto.UnidadMedida.Abreviacion : string.Empty))
                .ForMember(d => d.Rubro, opt => opt.MapFrom(src => src.Producto.Rubro != null ? src.Producto.Rubro.Descripcion : string.Empty))
                .ForMember(d => d.Subrubro, opt => opt.MapFrom(src => src.Producto.Subrubro != null ? src.Producto.Subrubro.Descripcion : string.Empty))
                .ForMember(d => d.ListaPrecio, opt => opt.MapFrom(src => src.ListaPrecio != null ? src.ListaPrecio.Nombre : string.Empty));

            CreateMap<Precio, PrecioFormModel>()
                        .ForMember(d => d.Producto, opt => opt.MapFrom(src => src.Producto.Descripcion))
                        .ForMember(d => d.Desde, opt => opt.MapFrom(src => src.FechaDesde))
                        .ForMember(d => d.Hasta, opt => opt.MapFrom(src => src.FechaHasta))
                        .ForMember(d => d.Precio, opt => opt.MapFrom(src => src.PrecioProducto));

            CreateMap<TipoProducto, TipoProductoListViewModel>()
              .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.Codigo));

            CreateMap<Obra, ObraListViewModel>()
                .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.CodigoObra))
                .ForMember(d => d.Cliente, opt => opt.MapFrom(src => src.Cliente != null ?
                    src.Cliente.RazonSocial + src.Cliente.Apellido + ", " + src.Cliente.Nombre : String.Empty))
                .ForMember(d => d.estado, opt => opt.MapFrom(src => src.estado == true ? "SI" : "NO"));

            CreateMap<Cliente, ClienteListViewModel>()
             .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.Codigo))
             .ForMember(d => d.Cliente, opt => opt.MapFrom(src => (src.RazonSocial == null || src.RazonSocial == string.Empty ? src.Apellido + ", " + src.Nombre : src.RazonSocial + ", " + src.Apellido + " " + src.Nombre)));
            //.ForMember(d => d.Cliente, opt => opt.MapFrom(src => src.RazonSocial + src.Apellido + ", " + src.Nombre));

            CreateMap<Constructora, ConstructoraListViewModel>()
            .ForMember(d => d.Codigo, opt => opt.MapFrom(src => src.Codigo))
            .ForMember(d => d.Constructora, opt => opt.MapFrom(src => src.RazonSocial + src.Apellido + ", " + src.Nombre));

            CreateMap<Contacto, ContactoListViewModel>()
                .ForMember(d => d.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(d => d.Constructora, opt => opt.MapFrom(src => (src.Constructora != null) ?
                    (src.Constructora.Nombre != null ? src.Constructora.Nombre : src.Constructora.RazonSocial) : string.Empty));



        }
    }
}