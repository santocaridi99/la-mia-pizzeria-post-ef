using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;


namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public static listaPizze pizze = null;

        public IActionResult Index()
        {
            if (pizze == null)
            {
                pizze = new listaPizze();
                Pizza margherita = new Pizza(0, "Pizza Margherita", "pomodoro , mozzarella campana , basilico", "/img/pizza-margherita-2-6yehdnu31vrv1puavcja7g753ipcgihq8vyh1ifv5pw.jpg", 3);
                Pizza capricciosa = new Pizza(1, "Pizza Capricciosa", "Che soddisfa ogni capriccio", "/img/salsicciaepatate.jpg", 4);
                Pizza salsicciaPatate = new Pizza(2, "Pizza Salsiccia e Patate", "salsiccia , patate , mozzarella capana", "/img/salsicciaepatate.jpg", 5);
                Pizza marinara = new Pizza(3, "Pizza Marinara", "Grande classico", "/img/marinara.jpg", 3);
                Pizza quattroStagioni = new Pizza(4, "Pizza Quattro Stagioni", "La quattro Stagioni", "/img/Pizza_Quattro_Stagioni_transparent.png", 4);
                pizze.listaDiPizze.Add(margherita);
                pizze.listaDiPizze.Add(capricciosa);
                pizze.listaDiPizze.Add(salsicciaPatate);
                pizze.listaDiPizze.Add(marinara);
                pizze.listaDiPizze.Add(quattroStagioni);
            }



            return View(pizze);
        }

        public IActionResult Show(int id)
        {
            return View("Show", pizze.listaDiPizze[id]);
        }

        public IActionResult PizzaForm()
        {
            Pizza NuovaPizza = new Pizza()
            {
                Id = 1,
                Nome = "",
                Descrizione = "",
                sFoto = "",
                Prezzo = 1
            };
            return View(NuovaPizza);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreaSchedaPizza(Pizza DatiPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("PizzaForm", DatiPizza);
            }

            //Da qui estraggo il file e me lo salvo su file system.
            //agendo su Request ci prendiamo il file e lo salviamo su
            //file system
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\File");
            //crea folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //get file extension

            FileInfo fileInfo = new FileInfo(DatiPizza.Foto.FileName);
            string fileName = DatiPizza.Nome + fileInfo.Extension;
            string fileNameWithPath = Path.Combine(path, fileName);
            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                DatiPizza.Foto.CopyTo(stream);
            }

            Pizza nuovaPizza = new Pizza()
            {
                Id = DatiPizza.Id,
                Nome = DatiPizza.Nome,
                Descrizione = DatiPizza.Descrizione,
                sFoto = "/File/" + fileName,
                Prezzo = DatiPizza.Prezzo,
            };

            pizze.listaDiPizze.Add(nuovaPizza);
            return View(nuovaPizza);
        }


        public IActionResult Edit(int id)
        {



            return View("Edit", pizze.listaDiPizze[id]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModificaPizza(Pizza DatiPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("PizzaForm", DatiPizza);
            }

            //Da qui estraggo il file e me lo salvo su file system.
            //agendo su Request ci prendiamo il file e lo salviamo su
            //file system
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\File");
            //crea folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            //get file extension

            FileInfo fileInfo = new FileInfo(DatiPizza.Foto.FileName);
            string fileName = DatiPizza.Nome + fileInfo.Extension;
            string fileNameWithPath = Path.Combine(path, fileName);
            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                DatiPizza.Foto.CopyTo(stream);
            }



            Pizza updatePizza = pizze.listaDiPizze.Find(x => x.Id == DatiPizza.Id);

            updatePizza.Nome = DatiPizza.Nome;
            updatePizza.Descrizione = DatiPizza.Descrizione;
            if (updatePizza.Foto != DatiPizza.Foto)
            {
                updatePizza.Foto = DatiPizza.Foto;
                updatePizza.sFoto = "/File/" + fileName;


            }
            else
            {
                updatePizza.Foto = DatiPizza.Foto;
                updatePizza.sFoto = DatiPizza.sFoto;
            }

            updatePizza.Prezzo = DatiPizza.Prezzo;



            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {

            Pizza pizzaRemove = pizze.listaDiPizze.Find(x => x.Id ==id);
            pizze.listaDiPizze.Remove(pizzaRemove);
            return RedirectToAction("Index");


        }



    }



}