using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TherapyQualityController.Controllers
{
    public class PatientQuestionnaireController : Controller
    {
        // GET: PatientQuestionnaireController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PatientQuestionnaireController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PatientQuestionnaireController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientQuestionnaireController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientQuestionnaireController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PatientQuestionnaireController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientQuestionnaireController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PatientQuestionnaireController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
