using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Practica1.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Practica1.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Practica1.Controllers
{
    public class PerritosController : Controller
    {
        private AppDbContext _context;
        public static List<Perritos> ListaPerritos = null;
        public static List<Testimonios> ListaTesti = null;
        
        
        public PerritosController(AppDbContext context)
        {
            _context = context;
            /*var MiJson = System.IO.File.ReadAllText("Models/Perritos.json");
            ListaPerritos = JsonConvert.DeserializeObject<List<Perritos>>(MiJson);*/

            var MiJson = System.IO.File.ReadAllText("Models/Testimonios.json");
            ListaTesti = JsonConvert.DeserializeObject<List<Testimonios>>(MiJson);
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var ListPerritos = await _context.Perritos.ToListAsync();

            return View(ListPerritos);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var perrito = await _context.Perritos.Where(x => x.id == Id).Include(x => x.Interesados).FirstAsync();
            
            PerritosTestimoniosViewModel elem = new PerritosTestimoniosViewModel();

            List<Testimonios> testi = new List<Testimonios>();

            var random = new Random();
            int i = random.Next(0, 6);

            testi.Add(ListaTesti.ElementAt(i));
            i = random.Next(0, 6);
            testi.Add(ListaTesti.ElementAt(i));
            
            elem.Perrito = perrito;
            elem.Testimonios = testi;
            return View(elem);
        }

        public async Task<IActionResult> Find(string nombre)
        {
            var ListPerritos = await _context.Perritos.ToListAsync();
            List<Perritos> PerritosBuscados = new List<Perritos>();

            foreach (var dog in ListPerritos)
            {
                if(dog.Nombre.ToLower().Contains(nombre.ToLower())){
                    PerritosBuscados.Add(dog);
                }
            }

            return View("Index", PerritosBuscados);
        }

        public async Task<IActionResult> filtrar(string raza)
        {
            var ListPerritos = await _context.Perritos.ToListAsync();
            List<Perritos> PerritosBuscados = new List<Perritos>();

            foreach (var dog in ListPerritos)
            {
                if(String.Equals(dog.Raza, raza)){
                    PerritosBuscados.Add(dog);
                }
            }
            
            return View("Index", PerritosBuscados);
        }

        [Authorize(Roles = "Voluntario")]
        public IActionResult Add()
        {

            return View(new Perritos());
        }
        public async Task<IActionResult> Save(IFormCollection formCollection)
        {
            Perritos NuevoPerrito = NuevoPerrito = new Perritos();
            //var p = await _context.Perritos.ToListAsync();
            //NuevoPerrito.id = p.Last().id + 1;
            NuevoPerrito.Nombre = formCollection["Nombre"];
            NuevoPerrito.Raza = formCollection["Raza"];
            NuevoPerrito.Color = formCollection["Color"];
            NuevoPerrito.Edad = formCollection["Edad"];
            NuevoPerrito.Sexo = formCollection["Sexo"];
            NuevoPerrito.Salud = formCollection["Salud"];
            NuevoPerrito.Personalidad = formCollection["Personalidad"];
            NuevoPerrito.Descripcion = formCollection["Descripcion"];
            NuevoPerrito.Tema = formCollection["Tema"];


            if(formCollection.Files["Foto"].Length > 0){
                var NombreFoto = DateTime.Now.ToString("dd-MM-yyyy") + DateTime.Now.ToString("hh-mm-ss") + Path.GetFileName(formCollection.Files["Foto"].FileName);
                var path = Path.Combine(Path.GetFullPath("wwwroot/Images/Perritos"), NombreFoto);
                formCollection.Files["Foto"].CopyTo(new FileStream(path, FileMode.Create));
                NuevoPerrito.Foto = NombreFoto;
            }

            _context.Perritos.Add(NuevoPerrito);
            await _context.SaveChangesAsync();

            /*var JasonPerrito = JsonConvert.SerializeObject(ListaPerritos);

            var path2 = Path.GetFullPath("Models/Perritos.json");

            using (FileStream fr = System.IO.File.Create(path2, 1024))
            {
                byte[] jp = new UTF8Encoding(true).GetBytes(JasonPerrito);
                 fr.Write(jp, 0, jp.Length);
            }*/
            
            //ViewData["nom"] = "Se guardo ";
            return View("Index", await _context.Perritos.ToListAsync());
        }
        public IActionResult Requisitos()
        {
            return PartialView("~/Views/Perritos/Requisitos.cshtml");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete (int id)
        {
            var perrito = await _context.Perritos.FindAsync(id);

            if (perrito == null){
                return NotFound();
            }
            
            _context.Remove(perrito);
            await _context.SaveChangesAsync();
            return View("Index", await _context.Perritos.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit (int id)
        {

            var perrito = await _context.Perritos.FindAsync(id);

            return View(perrito);

        }

        public async Task<IActionResult> Update (IFormCollection formCollection)
        {
            Perritos NuevoPerrito = NuevoPerrito = new Perritos();
            var perrito = await _context.Perritos.FindAsync(int.Parse(formCollection["id"]));
            
            //NuevoPerrito.id = int.Parse(formCollection["id"]);
            perrito.Nombre = formCollection["Nombre"];
            perrito.Raza = formCollection["Raza"];
            perrito.Color = formCollection["Color"];
            perrito.Edad = formCollection["Edad"];
            perrito.Sexo = formCollection["Sexo"];
            perrito.Salud = formCollection["Salud"];
            perrito.Personalidad = formCollection["Personalidad"];
            perrito.Descripcion = formCollection["Descripcion"];
            perrito.Tema = formCollection["Tema"];


            if(formCollection.Files["Foto"] != null){
                var NombreFoto = DateTime.Now.ToString("dd-MM-yyyy") + DateTime.Now.ToString("hh-mm-ss") + Path.GetFileName(formCollection.Files["Foto"].FileName);
                var path = Path.Combine(Path.GetFullPath("wwwroot/Images/Perritos"), NombreFoto);
                formCollection.Files["Foto"].CopyTo(new FileStream(path, FileMode.Create));
                perrito.Foto = NombreFoto;
            } 

            //_context.Perritos.Where(x => x.id == int.Parse(formCollection["id"]));
            _context.Perritos.Update(perrito);
            //_context.Remove(perrito);
            await _context.SaveChangesAsync();
            return View("Index", await _context.Perritos.ToListAsync());
        }
    }
}