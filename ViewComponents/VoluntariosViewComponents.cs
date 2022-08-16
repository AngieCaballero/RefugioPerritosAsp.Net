using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Practica1.Models;

namespace Practica1.ViewComponents
{
    public class VoluntariosViewComponent : ViewComponent
    {
        public List<Voluntarios> ListaVoluntarios = null;
        public VoluntariosViewComponent()
        {
            var myJsonString = System.IO.File.ReadAllText("Models/Voluntarios.json");
            ListaVoluntarios = JsonConvert.DeserializeObject<List<Voluntarios>>(myJsonString);
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(ListaVoluntarios);
        }
    }
}
