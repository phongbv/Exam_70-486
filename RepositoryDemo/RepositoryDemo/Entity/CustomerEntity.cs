using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositoryDemo.Entity
{
    public class CustomerEntity : IBaseEntity
    {
        public decimal Id { get; set; }
        public string CustomerNumber { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
    }
}