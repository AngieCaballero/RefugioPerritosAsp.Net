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


namespace Practica1.Controllers
{
    public class InteresadosController : Controller
    {
        private AppDbContext _context;
        
        public InteresadosController(AppDbContext context)
        {
            _context = context;
            
        }
        

        public IActionResult Add(int id)
        {
            ViewBag.PerritoId = id;
            return View();
        }
        public async Task<IActionResult> Save(Interesados interesados)
        {
             if (ModelState.IsValid)
            {
                _context.Interesados.Add(interesados);
                await _context.SaveChangesAsync();
                return Redirect("/Perritos/Details/" + interesados.PerritosId);
            }
            return Redirect("/Perritos/Index");
        }

        public async Task<IActionResult> Delete (int id)
        {
            var inter = await _context.Interesados.FindAsync(id);

            int PerritoId = inter.PerritosId;

            if (inter == null){
                return NotFound();
            }
            
            _context.Remove(inter);
            await _context.SaveChangesAsync();
            return Redirect("/Perritos/Details/" + PerritoId);
        }
    }
}