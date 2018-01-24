//-----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="None">
//     Copyright (c) Allow to distribute this code.
// </copyright>
// <author>Asma Khalid</author>
//-----------------------------------------------------------------------

namespace ImmediateRefreshWithLongDBQuery.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using ImmediateRefreshWithLongDBQuery.Models;

    /// <summary>
    /// Home controller class
    /// </summary>
    public class HomeController : Controller
    {
        #region Index method

        /// <summary>
        /// GET: Index method.
        /// </summary>
        /// <returns>Returns - index view page</returns> 
        public ActionResult Index()
        {
            // Info.
            return this.View();
        }

        #endregion

        #region Slow Page method

        /// <summary>
        /// GET: Slow Page method.
        /// </summary>
        /// <returns>Returns - index view page</returns> 
        public ActionResult IndexSlow()
        {
            // Info.
            return this.View();
        }

        #endregion

        #region Get data method.

        /// <summary>
        /// GET: /Home/GetData
        /// </summary>
        /// <param name="cancellationToken">Cancellation token parameter</param>
        /// <returns>Return data</returns>
        [NoAsyncTimeout]
        public async Task<ActionResult> GetData(CancellationToken cancellationToken)
        {
            // Initialization.
            JsonResult result = new JsonResult();

            try
            {
                // Initialization.
                CancellationToken disconnectedToken = Response.ClientDisconnectedToken;
                var source = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, disconnectedToken);

                // Initialization.
                string search = Request.Form.GetValues("search[value]")[0];
                string draw = Request.Form.GetValues("draw")[0];
                string order = Request.Form.GetValues("order[0][column]")[0];
                string orderDir = Request.Form.GetValues("order[0][dir]")[0];
                int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
                int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);

                // Loading.
                List<sp_slow_test_Result> data = await this.LoadData(source);

                // Total record count.
                int totalRecords = data.Count;

                // Verification.
                if (!string.IsNullOrEmpty(search) &&
                    !string.IsNullOrWhiteSpace(search))
                {
                    // Apply search
                    data = data.Where(p => p.Sr.ToString().ToLower().Contains(search.ToLower()) ||
                                           p.Title.ToLower().Contains(search.ToLower()) ||
                                           p.FirstName.ToString().ToLower().Contains(search.ToLower()) ||
                                           p.MiddleName.ToLower().Contains(search.ToLower()) ||
                                           p.LastName.ToLower().Contains(search.ToLower())).ToList();
                }

                // Sorting.
                data = this.SortByColumnWithOrder(order, orderDir, data);

                // Filter record count.
                int recFilter = data.Count;

                // Apply pagination.
                data = data.Skip(startRec).Take(pageSize).ToList();

                // Loading drop down lists.
                result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = recFilter, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Info
                Console.Write(ex);
            }

            // Return info.
            return result;
        }

        /////// <summary>
        /////// GET: /Home/GetData
        /////// </summary>
        /////// <returns>Return data</returns>
        ////public ActionResult GetData()
        ////{
        ////    // Initialization.
        ////    JsonResult result = new JsonResult();

        ////    try
        ////    {
        ////        // Initialization.
        ////        string search = Request.Form.GetValues("search[value]")[0];
        ////        string draw = Request.Form.GetValues("draw")[0];
        ////        string order = Request.Form.GetValues("order[0][column]")[0];
        ////        string orderDir = Request.Form.GetValues("order[0][dir]")[0];
        ////        int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
        ////        int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);

        ////        // Loading.
        ////        List<sp_slow_test_Result> data = this.LoadData();

        ////        // Total record count.
        ////        int totalRecords = data.Count;

        ////        // Verification.
        ////        if (!string.IsNullOrEmpty(search) &&
        ////            !string.IsNullOrWhiteSpace(search))
        ////        {
        ////            // Apply search
        ////            data = data.Where(p => p.Sr.ToString().ToLower().Contains(search.ToLower()) ||
        ////                                   p.Title.ToLower().Contains(search.ToLower()) ||
        ////                                   p.FirstName.ToString().ToLower().Contains(search.ToLower()) ||
        ////                                   p.MiddleName.ToLower().Contains(search.ToLower()) ||
        ////                                   p.LastName.ToLower().Contains(search.ToLower())).ToList();
        ////        }

        ////        // Sorting.
        ////        data = this.SortByColumnWithOrder(order, orderDir, data);

        ////        // Filter record count.
        ////        int recFilter = data.Count;

        ////        // Apply pagination.
        ////        data = data.Skip(startRec).Take(pageSize).ToList();

        ////        // Loading drop down lists.
        ////        result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = recFilter, data = data }, JsonRequestBehavior.AllowGet);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        // Info
        ////        Console.Write(ex);
        ////    }

        ////    // Return info.
        ////    return result;
        ////}

        #endregion

        #region Helpers

        #region Load Data

        /// <summary>
        /// Load data method.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token parameter</param>
        /// <returns>Returns - Data</returns>
        private async Task<List<sp_slow_test_Result>> LoadData(CancellationTokenSource cancellationToken)
        {
            // Initialization.
            List<sp_slow_test_Result> lst = new List<sp_slow_test_Result>();

            try
            {
                // Initialization.
                testEntities databaseManager = new testEntities();

                // Loading.
                lst = await databaseManager.Database.SqlQuery<sp_slow_test_Result>("EXEC sp_slow_test").ToListAsync(cancellationToken.Token);
            }
            catch (Exception ex)
            {
                // info.
                Console.Write(ex);
            }

            // info.
            return lst;
        }

        /////// <summary>
        /////// Load data method.
        /////// </summary>
        /////// <returns>Returns - Data</returns>
        ////private List<sp_slow_test_Result> LoadData()
        ////{
        ////    // Initialization.
        ////    List<sp_slow_test_Result> lst = new List<sp_slow_test_Result>();

        ////    try
        ////    {
        ////        // Initialization.
        ////        testEntities databaseManager = new testEntities();

        ////        // Loading.
        ////        lst = databaseManager.Database.SqlQuery<sp_slow_test_Result>("EXEC sp_slow_test").ToList();
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        // info.
        ////        Console.Write(ex);
        ////    }

        ////    // info.
        ////    return lst;
        ////}

        #endregion

        #region Sort by column with order method

        /// <summary>
        /// Sort by column with order method.
        /// </summary>
        /// <param name="order">Order parameter</param>
        /// <param name="orderDir">Order direction parameter</param>
        /// <param name="data">Data parameter</param>
        /// <returns>Returns - Data</returns>
        private List<sp_slow_test_Result> SortByColumnWithOrder(string order, string orderDir, List<sp_slow_test_Result> data)
        {
            // Initialization.
            List<sp_slow_test_Result> lst = new List<sp_slow_test_Result>();

            try
            {
                // Sorting
                switch (order)
                {
                    case "0":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Sr).ToList()
                                                                                                 : data.OrderBy(p => p.Sr).ToList();
                        break;

                    case "1":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Title).ToList()
                                                                                                 : data.OrderBy(p => p.Title).ToList();
                        break;

                    case "2":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.FirstName).ToList()
                                                                                                 : data.OrderBy(p => p.FirstName).ToList();
                        break;

                    case "3":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.MiddleName).ToList()
                                                                                                 : data.OrderBy(p => p.MiddleName).ToList();
                        break;

                    case "4":
                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.LastName).ToList()
                                                                                                   : data.OrderBy(p => p.LastName).ToList();
                        break;

                    default:

                        // Setting.
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Sr).ToList()
                                                                                                 : data.OrderBy(p => p.Sr).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                // info.
                Console.Write(ex);
            }

            // info.
            return lst;
        }

        #endregion

        #endregion
    }
}