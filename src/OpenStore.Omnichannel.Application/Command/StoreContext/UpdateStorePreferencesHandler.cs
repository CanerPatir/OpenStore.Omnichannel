using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.StoreContext;

namespace OpenStore.Omnichannel.Application.Command.StoreContext
{
    public class UpdateStorePreferencesHandler : IRequestHandler<UpdateStorePreferences>
    {
        private readonly ICrudRepository<StorePreferences> _repository;

        public UpdateStorePreferencesHandler(ICrudRepository<StorePreferences> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateStorePreferences request, CancellationToken cancellationToken)
        {
            var storePreferences = await _repository.Query.FirstOrDefaultAsync(cancellationToken);

            storePreferences.Update(request.Model);
            await _repository.UpdateAsync(storePreferences, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}