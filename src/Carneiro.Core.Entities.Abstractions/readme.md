# Carneiro.Core.Entities.Abstractions

This library includes only interfaces that represents entities:

- `IEntity`
- `IAuditableEntity`

The `IEntity` is the smallest known entity alive with just an `int` as identifier. The `IAuditableEntity` has more information:

- `IsActive`
- `CreateDate`
- `UpdateDate`
- `DeleteDate`
- `IsDeleted`

These entities are automatically used and populated using `Carneiro.Core.Repository` and `Carneiro.Core.Repository.Abstractions`.
