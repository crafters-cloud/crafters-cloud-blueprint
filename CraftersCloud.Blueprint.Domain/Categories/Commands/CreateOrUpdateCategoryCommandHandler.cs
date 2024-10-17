using CraftersCloud.Core.Data;
using CraftersCloud.Core.Entities;
using CraftersCloud.Core.EntityFramework;
using JetBrains.Annotations;
using MediatR;

namespace CraftersCloud.Blueprint.Domain.Categories.Commands;

[UsedImplicitly]
public class CreateOrUpdateCategoryCommandHandler(IRepository<Category, Guid> categoryRepository) 
    : IRequestHandler<CreateOrUpdateCategory.Command, Category>
{
    public async Task<Category> Handle(CreateOrUpdateCategory.Command request, CancellationToken cancellationToken)
    {
        Category category;
        if (request.Id.HasValue)
        {
            category = await categoryRepository.QueryAll().QueryById(request.Id.Value)
                .SingleOrNotFoundAsync(cancellationToken);
            category.Update(request);
        }
        else
        {
            category = Category.Create(request);
            categoryRepository.Add(category);
        }

        return category;
    }
}
