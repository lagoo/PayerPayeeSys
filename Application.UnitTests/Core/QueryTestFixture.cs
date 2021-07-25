using Application.Common.Mappings;
using AutoMapper;
using Persistence;
using System;
using Xunit;

namespace Application.UnitTests.Core
{
    public class QueryTestFixture : IDisposable
    {
        public ApplicationContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestFixture()
        {
            Context = ApplicationContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            ApplicationContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
