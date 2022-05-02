# How to create a CRUD repository

```csharp
using Limbo.EntityFramework.Services;
using Limbo.EntityFramework.Services.Crud;
using Microsoft.Extensions.Logging;

namespace MyNamespace.Services {
    public class MyService : CrudServiceBase<MyModel, IMyRepository>, IMyService {
        public MyService(IMyRepository repository, ILogger<ServiceBase<IMyRepository>> logger, EntityFrameworkSettings EntityFrameworkSettings, IUnitOfWork<TRepository> unitOfWork) : base(repository, logger) {
        }
    }
}

```

```csharp
using Limbo.EntityFramework.Services.Crud;

namespace MyNamespace.Services {
    public interface IMyService : ICrudServiceBase<MyModel, IMyRepository> {
    }
}
```