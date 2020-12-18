namespace Socializer.Services.Data.Tests.Common
{
    using System;
    using System.Reflection;

    using Microsoft.EntityFrameworkCore;
    using Socializer.Data;
    using Socializer.Services.Mapping;

    public class TestBase
    {
        public TestBase()
        {
            AutoMapperConfig.RegisterMappings(Assembly.GetCallingAssembly());
        }

        public static ApplicationDbContext GetDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new ApplicationDbContext(options);

            return db;
        }
    }
}
