using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContentService.Models;

namespace ContentService.Data
{
    public class ContentServiceContext : DbContext
    {
        public ContentServiceContext (DbContextOptions<ContentServiceContext> options)
            : base(options)
        {
        }

        public DbSet<ContentService.Models.Content> Content { get; set; } = default!;
    }
}
