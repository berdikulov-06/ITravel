using ITravel.Domain.Entities.Payme;
using ITravel.Domain.Entities.SoftSettings;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Data.Contexts
{
    public class TourismDbContext : DbContext
    {
        public TourismDbContext(DbContextOptions<TourismDbContext> options)
            : base(options)
        {

        }
        public DbSet<SoftSettings> softsettings { get; set; }
        public DbSet<payme_transaction> payme_transaction { get; set; }
        public DbSet<order> order { get; set; }
        public DbSet<transaction> transaction { get; set; }
    }
}
