# How to create a CRUD repository

```csharp
using Limbo.DataAccess.Repositories.Crud;
using Microsoft.Extensions.Logging;

namespace MyNamespace.Repositories {
    public class MyRepository : DbCrudRepositoryBase<MyModel>, IMyRepository {
        public MyRepository(IMyContext dbContext, ILogger<DbCrudRepositoryBase<MyModel>> logger) : base(dbContext, logger) {
        }
    }
}
```

```csharp
using Limbo.DataAccess.Repositories.Crud;

namespace MyNamespace.Repositories {
    public interface IAMyRepository : IDbCrudRepositoryBase<MyModel> {
    }
}
```