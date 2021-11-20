namespace MyEdo.Business.Test.Common
{
    using IdentityServer4.EntityFramework.Options;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Moq;
    using MyEdo.Data;
    using System;

    public static class MyEdoDbContextInMemoryFactory
    {
        public static MyEdoDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<MyEdoDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            var operationalStoreOptions = new Mock<IOptions<OperationalStoreOptions>>();
            var mockedIdentityServer = new Mock<IdentityServer4.EntityFramework.Options.OperationalStoreOptions>();
            operationalStoreOptions.Setup(m => m.Value)
                .Returns(() => (mockedIdentityServer.Object));

            return new MyEdoDbContext(options, operationalStoreOptions.Object);
        }
    }
}
