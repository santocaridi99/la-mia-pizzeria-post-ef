using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;


namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public static listaPizze pizze = null;
        public static PizzeriaContext db = new PizzeriaContext();

        public IActionResult Index()
        {
            //if (pizze == null)
            //{
            //    pizze = new listaPizze();
            //    Pizza margherita = new Pizza { Nome = "Pizza Margherita", Descrizione = "pomodoro , mozzarella campana , basilico", sFoto = "/img/pizza-margherita-2-6yehdnu31vrv1puavcja7g753ipcgihq8vyh1ifv5pw.jpg", Prezzo = 3 };
            //    Pizza capricciosa = new Pizza { Nome = "Pizza Capricciosa", Descrizione = "Che soddisfa ogni capriccio", sFoto = "/img/salsicciaepatate.jpg", Prezzo = 4 };
            //    Pizza salsicciaPatate = new Pizza { Nome = "Pizza Salsiccia e Patate", Descrizione = "salsiccia , patate , mozzarella capana", sFoto = "/img/salsicciaepatate.jpg", Prezzo = 5 };
            //    Pizza marinara = new Pizza { Nome = "Pizza Marinara", Descrizione = "Grande classico", sFoto = "/img/marinara.jpg", Prezzo = 3 };
            //    Pizza quattroStagioni = new Pizza { Nome = "Pizza Quattro Stagioni", Descrizione = "La quattro Stagioni", sFoto = "/img/Pizza_Quattro_Stagioni_transparent.png", Prezzo = 4 };
            //    //pizze.listaDiPizze.Add(margherita);
            //    //pizze.listaDiPizze.Add(capricciosa);
            //    //pizze.listaDiPizze.Add(salsicciaPatate);
            //    //pizze.listaDiPizze.Add(marinara);
            //    //pizze.listaDiPizze.Add(quattroStagioni);
            //    db.Add(margherita);
            //    db.Add(capricciosa);
            //    db.Add(salsicciaPatate);
            //    db.Add(marinara);
            //    db.Add(quattroStagioni);
            //    db.SaveChanges();   
                

            //}

            

            //tramite db passo alla view il PizzaContext
            return View(db);
        }
        

        public IActionResult Show(int id)
        {
            var dataId = db.Pizze.Where(i => i.Id == id).FirstOrDefault();
            return View("Show",dataId);
        }

        public IActionResult PizzaForm()
        {
            Pizza NuovaPizza = new Pizza()
            {
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

            //pizze.listaDiPizze.Add(nuovaPizza);
            db.Add(nuovaPizza);
            db.SaveChanges();
            return View(nuovaPizza);
        }


        public IActionResult Edit(int id)
        {


            var dataId = db.Pizze.Where(i => i.Id == id).FirstOrDefault();
            return View("Edit", dataId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModificaPizza(Pizza ModificataPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("PizzaForm", ModificataPizza);
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

            FileInfo fileInfo = new FileInfo(ModificataPizza.Foto.FileName);
            string fileName = ModificataPizza.Nome + fileInfo.Extension;
            string fileNameWithPath = Path.Combine(path, fileName);
            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                ModificataPizza.Foto.CopyTo(stream);
            }


            
                var aggiornaPizza = db.Pizze
                         .Where(pizza => pizza.Id == ModificataPizza.Id).FirstOrDefault();
                aggiornaPizza.Nome =  ModificataPizza.Nome;
                aggiornaPizza.Descrizione = ModificataPizza.Descrizione;
                aggiornaPizza.sFoto = "/File/" + fileName;
                aggiornaPizza.Prezzo = ModificataPizza.Prezzo;
                db.Pizze.UpdateRange(aggiornaPizza);
                db.SaveChanges();

            
               


            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {

            var dataId = db.Pizze.Where(i => i.Id == id).FirstOrDefault();
            db.Pizze.Remove(dataId);
            db.SaveChanges();
            return RedirectToAction("Index");


        }



    }



}