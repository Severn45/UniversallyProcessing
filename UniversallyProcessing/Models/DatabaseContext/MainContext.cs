using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UniversallyProcessing.Api.Models.DatabaseContext
{
    public class MainContext : IdentityDbContext
    {
        public MainContext(DbContextOptions options) : base(options)
        {
        }
    }
}
