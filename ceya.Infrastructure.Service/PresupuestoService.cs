using ceya.Core.Common;
using ceya.Domain.Repository;
using ceya.Infrastructure.Service.Properties;
using ceya.Model.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using X.PagedList;
using ceya.Domain.Model.DTOs;

namespace ceya.Domain.Service
{
    public class PresupuestoService : IPresupuestoService
    {
        private readonly IPresupuestoRepository presupuestoRepository;
        private readonly IPresupuestoItemRepository presupuestoItemRepository;
        private readonly IPresupuestoEstadoRepository presupuestoEstadoRepository;
        private readonly IPresupuestoItemRepository itemRepository;
        private readonly IArchivoRepository archivoRepository;
        private readonly IPresupuestoSeguimientoService presupuestoSeguimientoService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IListaPrecioRepository listaPrecioRepository;

        public PresupuestoService(IPresupuestoRepository presupuestoRepository,
            IPresupuestoItemRepository presupuestoItemRepository,
            IPresupuestoEstadoRepository presupuestoEstadoRepository,
            IPresupuestoItemRepository itemRepository,
            IArchivoRepository archivoRepository,
            IPresupuestoSeguimientoService presupuestoSeguimientoService,
            IUnitOfWork unitOfWork,
            IListaPrecioRepository listaPrecioRepository)
        {
            this.presupuestoRepository = presupuestoRepository;
            this.presupuestoItemRepository = presupuestoItemRepository;
            this.presupuestoEstadoRepository = presupuestoEstadoRepository;
            this.itemRepository = itemRepository;
            this.archivoRepository = archivoRepository;
            this.presupuestoSeguimientoService = presupuestoSeguimientoService;
            this.unitOfWork = unitOfWork;
            this.listaPrecioRepository = listaPrecioRepository;
        }

        public Presupuesto GetPresupuesto(Guid id)
        {
            return this.presupuestoRepository.GetById(id);
        }

        public long GetNuevoCodigo()
        {
            var ultimo = this.presupuestoRepository.MaxCodigo();

            return (ultimo + 1);
        }

        public IPagedList<Presupuesto> GetPresupuestosByPage(int currentPage, int noOfRecords, string sortBy, string direction, string filterBy, string searchString, Guid? estadoId)
        {
            return this.presupuestoRepository.GetPresupuestosByPage(currentPage, noOfRecords, sortBy, direction, filterBy, searchString, estadoId);
        }

        public IEnumerable<ValidationResult> CanAddPresupuesto(Presupuesto nuevoPresupuesto)
        {
            //Presupuesto presupuesto;
           // if (nuevoPresupuesto.Id == Guid.Empty)
            //{
            //    presupuesto = this.presupuestoRepository.Get(x => x.Codigo == nuevoPresupuesto.Codigo);
            //}
            //else
            //{
            //    presupuesto = this.presupuestoRepository.Get(x => x.Codigo == nuevoPresupuesto.Codigo && x.Id != nuevoPresupuesto.Id);
            //}
            //if (presupuesto != null)
            //{
            //    yield return new ValidationResult("Codigo", Resources.PresupuestoExiste);
            //}

            if (nuevoPresupuesto.Id != Guid.Empty)
            {
                yield return new ValidationResult("Codigo", Resources.PresupuestoExiste);
            }
            if (nuevoPresupuesto.Codigo > 0)
            {
                yield return new ValidationResult("Codigo", Resources.CodigoNotRequired);
            }
            var estadoInicial = this.presupuestoEstadoRepository.Get(x => x.Codigo == "1"); // PRESUPUESTADO
            if (nuevoPresupuesto.PresupuestoEstadoId != Guid.Empty && nuevoPresupuesto.PresupuestoEstadoId != estadoInicial.Id)
            {
                yield return new ValidationResult("PresupuestoEstadoId", Resources.EstadoNotRequired);
            }
            if (nuevoPresupuesto.ClienteId == Guid.Empty || nuevoPresupuesto.ClienteId == null)
            {
                yield return new ValidationResult("ClienteId", Resources.ClienteRequired);
            }
            if (nuevoPresupuesto.ObraId == Guid.Empty || nuevoPresupuesto.ObraId == null)
            {
                yield return new ValidationResult("ObraId", Resources.ObraRequired);
            }
            foreach (var item in nuevoPresupuesto.PresupuestoItem)
            {
                if (item.Estado != "NUEVO" && item.Estado != String.Empty && item.Estado != null)
                {
                    yield return new ValidationResult("Items", Resources.InvalidItemState);
                }

                if (item.ArchivoTipologiaId != Guid.Empty && item.ArchivoTipologiaId != null && nuevoPresupuesto.ArchivoTransaccionId == Guid.Empty)
                {
                    yield return new ValidationResult("ArchivoTransaccionId", Resources.InvalidEmptyTransaccion);
                }
            }
        }

        public IEnumerable<ValidationResult> CanAddPresupuestoEnBase(Presupuesto nuevoPresupuesto)
        {
            var estadoInicial = this.presupuestoEstadoRepository.Get(x => x.Codigo == "1"); // PRESUPUESTADO
            if (nuevoPresupuesto.PresupuestoEstadoId != Guid.Empty && nuevoPresupuesto.PresupuestoEstadoId != estadoInicial.Id)
            {
                yield return new ValidationResult("PresupuestoEstadoId", Resources.EstadoNotRequired);
            }
            if (nuevoPresupuesto.ClienteId == Guid.Empty || nuevoPresupuesto.ClienteId == null)
            {
                yield return new ValidationResult("ClienteId", Resources.ClienteRequired);
            }
            if (nuevoPresupuesto.ObraId == Guid.Empty || nuevoPresupuesto.ObraId == null)
            {
                yield return new ValidationResult("ObraId", Resources.ObraRequired);
            }
            foreach (var item in nuevoPresupuesto.PresupuestoItem)
            {
                if (item.Estado != "NUEVO" && item.Estado != String.Empty && item.Estado != null)
                {
                    yield return new ValidationResult("Items", Resources.InvalidItemState);
                }

                if (item.ArchivoTipologiaId != Guid.Empty && item.ArchivoTipologiaId != null && nuevoPresupuesto.ArchivoTransaccionId == Guid.Empty)
                {
                    yield return new ValidationResult("ArchivoTransaccionId", Resources.InvalidEmptyTransaccion);
                }
            }
        }

        public IEnumerable<ValidationResult> CanAddNewRevisionPresupuesto(Presupuesto nuevoPresupuesto)
        {
            Presupuesto presupuestoPadre = null;
            if (nuevoPresupuesto.Id != Guid.Empty)
            {
                presupuestoPadre = this.presupuestoRepository.Get(x => x.Id == nuevoPresupuesto.Id && x.Codigo == nuevoPresupuesto.Codigo);
            }
            if (presupuestoPadre == null)
            {
                yield return new ValidationResult("Codigo", Resources.PresupuestoNoExiste);
            }
            if (presupuestoPadre.PresupuestoNuevo != null)
            {
                yield return new ValidationResult("Codigo", Resources.PresupuestoRevisionNoActualizable);
            }

            if (nuevoPresupuesto.ClienteId == Guid.Empty || nuevoPresupuesto.ClienteId == null)
            {
                yield return new ValidationResult("ClienteId", Resources.ClienteRequired);
            }
            if (nuevoPresupuesto.ObraId == Guid.Empty || nuevoPresupuesto.ObraId == null)
            {
                yield return new ValidationResult("ObraId", Resources.ObraRequired);
            }
            var itemPadres = presupuestoPadre.PresupuestoItem.ToList();
            foreach (var item in nuevoPresupuesto.PresupuestoItem)
            {
                if (item.Estado != "GUARDADO")
                {
                    var itemPadre = itemPadres.Where(x => x.Id == item.Id).SingleOrDefault();
                    if (itemPadre == null)
                    {
                        yield return new ValidationResult("Items", Resources.ItemNotExists);
                    }
                }
                else if (item.Estado != "MODIFICADO")
                {
                    var itemPadre = itemPadres.Where(x => x.Id == item.Id).SingleOrDefault();
                    if (itemPadre == null)
                    {
                        yield return new ValidationResult("Items", Resources.ItemNotExists);
                    }
                }
                else if (item.Estado == "NUEVO" || item.Estado == String.Empty || item.Estado == null)
                {
                    ;
                }

                if (item.ArchivoTipologiaId != Guid.Empty && item.ArchivoTipologiaId != null && nuevoPresupuesto.ArchivoTransaccionId == Guid.Empty)
                {
                    yield return new ValidationResult("ArchivoTransaccionId", Resources.InvalidEmptyTransaccion);
                }
            }
        }

        public void CreatePresupuesto(Presupuesto presupuesto)
        {
            Presupuesto presupuestoPadre = null;
            if (presupuesto.Id != Guid.Empty)
            {
                presupuestoPadre = this.presupuestoRepository.Get(x => x.Id == presupuesto.Id && x.Codigo == presupuesto.Codigo);
                presupuesto.Codigo = presupuestoPadre.Codigo;
            }
            presupuesto.Id = Guid.NewGuid();
            if (presupuesto.Codigo == 0)
            {
                presupuesto.Codigo = this.GetNuevoCodigo();
            }
            presupuesto.FechaCreacion = DateTime.Now;
            if (presupuesto.PresupuestoEstadoId == Guid.Empty)
            {
                presupuesto.PresupuestoEstado = this.presupuestoEstadoRepository.Get(x => x.Codigo == "1"); // PRESUPUESTADO
            }
            presupuesto.PresupuestoAnterior = presupuestoPadre;

            foreach (var item in presupuesto.PresupuestoItem)
            {
                if (item.Estado == "GUARDADO" || item.Estado == "MODIFICADO")
                {
                    // TODO: Check para saber si realmente se ha modificado
                    var itemPadre = presupuestoPadre.PresupuestoItem.Where(x => x.Id == item.Id).Single();
                    item.PresupuestoItemAnterior = itemPadre;
                }
                item.Id = Guid.NewGuid();
                if (item.ArchivoTipologiaId != Guid.Empty && item.ArchivoTipologiaId != null)
                {
                    var archivo = this.archivoRepository.Get(x => x.Id == item.ArchivoTipologiaId && x.TransaccionId == presupuesto.ArchivoTransaccionId);
                    if (archivo.TransaccionCompletada != true)
                    {
                        archivo.TransaccionCompletada = true;
                        this.archivoRepository.Update(archivo);
                    }
                }
            }

            this.presupuestoRepository.Add(presupuesto);
            this.SavePresupuesto();
            SeguimientoEstado(presupuesto);
        }

        public void CreatePresupuestoEnBase(Presupuesto presupuesto)
        {
            Presupuesto presupuestoPadre = null;
            if (presupuesto.Id != Guid.Empty)
            {
                presupuestoPadre = this.presupuestoRepository.Get(x => x.Id == presupuesto.Id && x.Codigo == presupuesto.Codigo);
                //   presupuesto.Codigo = presupuestoPadre.Codigo;
                presupuesto.Codigo =  this.GetNuevoCodigo();
            }
            presupuesto.Id = Guid.NewGuid();
            if (presupuesto.Codigo == 0)
            {
                presupuesto.Codigo = this.GetNuevoCodigo();
            }
            presupuesto.FechaCreacion = DateTime.Now;
            if (presupuesto.PresupuestoEstadoId == Guid.Empty)
            {
                presupuesto.PresupuestoEstado = this.presupuestoEstadoRepository.Get(x => x.Codigo == "1"); // PRESUPUESTADO
            }
            presupuesto.PresupuestoAnterior = presupuestoPadre;

            foreach (var item in presupuesto.PresupuestoItem)
            {
                if (item.Estado == "GUARDADO" || item.Estado == "MODIFICADO")
                {
                    // TODO: Check para saber si realmente se ha modificado
                    var itemPadre = presupuestoPadre.PresupuestoItem.Where(x => x.Id == item.Id).Single();
                    item.PresupuestoItemAnterior = itemPadre;
                }
                item.Id = Guid.NewGuid();
                if (item.ArchivoTipologiaId != Guid.Empty && item.ArchivoTipologiaId != null)
                {
                    var archivo = this.archivoRepository.Get(x => x.Id == item.ArchivoTipologiaId && x.TransaccionId == presupuesto.ArchivoTransaccionId);
                    if (archivo.TransaccionCompletada != true)
                    {
                        archivo.TransaccionCompletada = true;
                        this.archivoRepository.Update(archivo);
                    }
                }
            }

            this.presupuestoRepository.Add(presupuesto);
            this.SavePresupuesto();
            SeguimientoEstado(presupuesto);
        }

        public void EditPresupuesto(Presupuesto presupuestoNuevo)
        {
            var presupuestoPadre = this.presupuestoRepository.Get(x => x.Id == presupuestoNuevo.Id && x.Codigo == presupuestoNuevo.Codigo);

            CreatePresupuesto(presupuestoNuevo);

            presupuestoPadre.PresupuestoNuevo = presupuestoNuevo;

            this.presupuestoRepository.Update(presupuestoPadre);
            foreach (var itemPadre in presupuestoPadre.PresupuestoItem)
            {
                var itemNuevo = presupuestoNuevo.PresupuestoItem.Where(x => x.PresupuestoItemAnteriorId == itemPadre.Id).Single();
                itemPadre.PresupuestoItemNuevo = itemNuevo;
                this.presupuestoItemRepository.Update(itemPadre);
            }

            this.SavePresupuesto();
            SeguimientoEstado(presupuestoNuevo);
        }

        public void SavePresupuesto()
        {
            this.unitOfWork.Commit();
        }

        public void UpdateEstado(PresupuestoSeguimiento seguimiento)
        {
            Presupuesto presupuesto = presupuestoRepository.Get(x => x.Id == seguimiento.PresupuestoId);
            presupuesto.PresupuestoEstadoId = seguimiento.PresupuestoEstadoId;
            presupuestoRepository.Update(presupuesto);
            SavePresupuesto();
        }

        public void SeguimientoEstado(Presupuesto presupuesto)
        {
            PresupuestoSeguimiento seguimiento = new PresupuestoSeguimiento();
            seguimiento.Id = Guid.NewGuid();
            seguimiento.PresupuestoEstadoId = presupuesto.PresupuestoEstadoId;
            seguimiento.PresupuestoId = presupuesto.Id;
            seguimiento.Fecha = presupuesto.Fecha;
            seguimiento.FechaAlerta = presupuesto.Fecha;
            seguimiento.Activo = true;
            presupuestoSeguimientoService.Add(seguimiento);
        }

        public IEnumerable<PrecioActualizado> ObtenerPreciosActualizados(Guid presupuestoId)
        {
            var preciosActualizados = new List<PrecioActualizado>();
            var items = presupuestoItemRepository.GetMany(x => x.PresupuestoId == presupuestoId);
            foreach (var item in items)
            {
                var precioColocacion = ObtenerPrecioActualizadoPorProducto("COLOCACION", item.ColocacionId.Value);
                var precioVidrio = ObtenerPrecioActualizadoPorProducto("VIDRIOS", item.VidriosId.Value);
                preciosActualizados.Add(new PrecioActualizado
                {
                    PrecioVidrio = ((item.Alto * item.Ancho) * precioColocacion) * item.Unidades,
                    PrecioColocacion = (((item.Alto + item.Ancho) * 2) * precioColocacion) * item.Unidades,
                    Id = item.Id
                });
            }
            return preciosActualizados;
        }

        private decimal ObtenerPrecioActualizadoPorProducto(string nombre, Guid id)
        {
            var fechaActual = DateTime.Now;
            var colocacion = listaPrecioRepository.Get(x => x.Nombre == nombre);
            var idProducto = colocacion.Precio.FirstOrDefault(x => x.Id == id).Producto.Id;
            var precioActual = colocacion.Precio.FirstOrDefault(x => x.ProductoId == idProducto && x.FechaDesde <= fechaActual && fechaActual <= x.FechaHasta);
            if (precioActual == null) throw new Exception(string.Format("No hay precio Actual para {0}", nombre));
            return precioActual.PrecioProducto;
        }

        public void EliminarItem(Guid itemId)
        {
            var itemParaEliminar = itemRepository.GetById(itemId);
            if (itemParaEliminar != null)
            {
                itemRepository.Delete(itemParaEliminar);
                SavePresupuesto();
            }
            else
                throw new Exception("Se intentó eliminar un item que no existe");
        }

        public void EliminarArchivoItem(Guid itemId)
        {
            var itemParaModificar = itemRepository.GetById(itemId);
            var archivoId = itemParaModificar.Archivo.Id;
            if (itemParaModificar != null)
            {
                itemParaModificar.ArchivoTipologiaId = null;               
                SavePresupuesto();

                //var archivoParaEliminar = archivoRepository.GetById(archivoId);
                //if (archivoParaEliminar != null)
                //{
                //    archivoRepository.Delete(archivoParaEliminar);
                //    unitOfWork.Commit();
                //}               
            }
            else
                throw new Exception("Se intentó eliminar un item que no existe");
        }
    }
}
