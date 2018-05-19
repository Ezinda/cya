using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mvc.ViewModels;
using ceya.Model.Models;
using ceya.Infrastructure.DataAccess;
using ceya.Domain.Service;
using AutoMapper;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;

namespace mvc.Controllers
{
    public class LoginController : Controller
    {
        private GestionComercialWebEntities db;

        public LoginController(IDatabaseFactory dbFactory)
        {
            this.db = dbFactory.Get();
        }

        public ActionResult Index()
        {
            Session["rol"] = string.Empty;
            return View();
        }

        //public ActionResult Registrar()
        //{
            //var Empresas = db.Empresa.Where(x=>x.Nombre != "Administrador");
         
            //ViewBag.EmpresaId = new SelectList(Empresas, "Id", "Nombre",Guid.Parse("D0FFE2AA-AB17-400D-926A-20893565416C"));

            //var tipoSuscripcion = db.TipoSuscripcion.Where(x => x.Nombre != "Suscripcion Premium");
            //ViewBag.TipoSuscripcionId = new SelectList(tipoSuscripcion, "Id", "Nombre");
            //return View();
        //}

        //[HttpPost]
        //public ActionResult Registrar(RegistrarViewModel registrar)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (!ValidarUsuario(registrar))
        //        {
        //            Empleado empleado = new Empleado();
        //            empleado.Id = Guid.NewGuid();
        //            empleado.Nombre = registrar.Nombre;
        //            empleado.Apellido = registrar.Apellido;
        //            empleado.Direccion = registrar.Direccion;
        //            empleado.Celular = registrar.Celular;
        //            empleado.Telefono = registrar.Telefono;
        //            empleado.FechaNacimiento = registrar.FechaNacimiento;
        //            empleado.Email = registrar.Email;
        //            int edad = registrar.FechaNacimiento < DateTime.Now.Date ? DateTime.Today.AddTicks(-registrar.FechaNacimiento.Ticks).Year - 1 : 0;
        //            empleado.Edad = edad;
        //            db.Empleado.Add(empleado);

        //            Guid EmpresaId = Guid.Empty;
        //            if (NuevaEmpresa(registrar))
        //            {
        //                Empresa empresa = new Empresa();
        //                empresa.Id = Guid.NewGuid();
        //                empresa.Nombre = registrar.Empresa;
        //                empresa.Contacto = registrar.Apellido + " " + registrar.Nombre;
        //                empresa.Cargo = "Encargado";
        //                empresa.Direccion = registrar.Direccion;
        //                empresa.Celular = registrar.Celular;
        //                empresa.Telefono = registrar.Telefono;
        //                empresa.Email = registrar.Empresa;
        //                db.Empresa.Add(empresa);
        //                EmpresaId = empresa.Id;
        //            }
        //            else
        //            {
        //                var empresa = db.Empresa.Where(x => x.Id == registrar.EmpresaId).First();
        //                EmpresaId = empresa.Id; 
        //            }


        //            var rol = db.Rol
        //                .Where(x => x.Nombre == "Empleado")
        //                .FirstOrDefault();

        //            Usuario u = new Usuario();
        //            u.Id = Guid.NewGuid();
        //            u.EmpleadoId = empleado.Id;
        //            u.RolId = rol.Id;
        //            u.Nombre = registrar.Usuario;
        //            u.Password = Encrypt(registrar.Password, true);
        //            u.EmpresaId = EmpresaId;
        //            u.TipoSuscripcionId = registrar.TipoSuscripcionId;
        //            u.Activo = false;
        //            db.Usuario.Add(u);
        //            db.SaveChanges();

        //            //Session["username"] = u.Nombre;
        //            //Session["rol"] = rol.Descripcion;

        //            //TemData.EmpID = u.EmpleadoId;
        //            //TemData.Empresa = u.Empresa.Nombre;
        //            //TemData.EmpresaId = u.EmpresaId.Value;
        //            return Json(new { status = true, responseText = string.Empty });
        //        }
        //        return Json(new { status = false, responseText = "Usuario Existente." });
        //    }
        //    var tipoSuscripcion = db.TipoSuscripcion.Where(x => x.Nombre != "Suscripcion Premium");
        //    ViewBag.TipoSuscripcionId = new SelectList(tipoSuscripcion, "Id", "Nombre");
        //    return Json(new { status = false, responseText = "Datos Inválidos." });
            
        //}
        
        //private bool NuevaEmpresa(RegistrarViewModel registrar)
        //{
        //    if (registrar.EmpresaId == Guid.Parse("D0FFE2AA-AB17-400D-926A-20893565416C"))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //private bool ValidarUsuario(RegistrarViewModel registrar)
        //{
        //    var usuario = db.Empleado
        //        .Where(x => x.Email == registrar.Email)
        //            .Any();

        //    return usuario;
        //}
        
        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                string pass = Encrypt(usuario.Password,true);
                var model =
                    (from m in db.Usuario
                     where m.Nombre == usuario.Nombre
                     && m.Password == pass 
                     && m.Activo == true
                     select m)
                     .Any();

                if (model)
                {
                    var loginInfo = db.Usuario
                        .Where(x => x.Nombre == usuario.Nombre
                            && x.Password == pass)
                        .FirstOrDefault();

                    Session["username"] = loginInfo.Nombre;
                    Session["rol"] = loginInfo.Rol.Descripcion;
                   
                    TemData.EmpID = loginInfo.Id;
                    return RedirectToAction("Index","Home");//Json(new { status = true, responseText = string.Empty });
                }       
            }
            return Json(new { status = false, responseText = "Usuario Inválido."});
        }
    
        public ActionResult Logout()
        {
            Session.Clear();
            TemData.EmpID = Guid.Empty;
            return RedirectToAction("Index", "Login");
        }

        public ContentResult GenerarHash(string texto)
        {
            return Content(Encrypt(texto, true));
        }

        private static string Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            // Get the key from config file

            string key = "Pass";
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

    }
}