using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RepositoryDemo.Repo
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=DataContext")
        {
        }
    }
}