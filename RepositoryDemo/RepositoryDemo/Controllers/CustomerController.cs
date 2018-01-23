using RepositoryDemo.Entity;
using RepositoryDemo.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositoryDemo.Controllers
{
    public class CustomerController : Controller
    {
        private IGenericUnitOfWork<CustomerEntity> genericUnitOfWork;
        // GET: Customer
        public ActionResult Index()
        {
            genericUnitOfWork = new GenericUnitOfWork<CustomerEntity>();
            return View(genericUnitOfWork.GetRepository().GetAllRecordsWithoutTracking());
        }
    }
}