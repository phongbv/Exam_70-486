using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositoryDemo.Entity
{
    public interface IBaseEntity
    {
        decimal Id { get; set; }
    }
}