using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Data;
using UserManagement.Models;

namespace UserManagement.Controllers
{
    public class GridController : Controller
    {
        private CsvDb db;
        public GridController()
        {
            db = new CsvDb("uj.txt");
        }

        public ActionResult Users_Read([DataSourceRequest] DataSourceRequest request)
        {
            var users = db.GetAllAsync().Result;
            var result = Enumerable.Range(0, users.Count).Select(i => new User
            {
                BirthPlace = users[i].BirthPlace,
                BirthDate = users[i].BirthDate,
                Name = users[i].Name,
                LastName = users[i].LastName,
                FirstName = users[i].FirstName,
                City = users[i].City,
                Password = users[i].Password,
                Id = users[i].Id
            });

            var dsResult = result.ToDataSourceResult(request);
            return Json(dsResult);
        }
    }
}
