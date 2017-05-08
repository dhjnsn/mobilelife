using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace TaxManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            // Tell LiteDB which property to use as Id
            LiteDB.BsonMapper.Global.Entity<Models.Municipality>()
                .Id(x => x.Name);

            // Create an index on the lowercase name
            LiteDB.BsonMapper.Global.Entity<Models.Municipality>()
                .Index<String>("Name_lowercase", x => x.Name.ToLower(), true);

            host.Run();
        }
    }
}
