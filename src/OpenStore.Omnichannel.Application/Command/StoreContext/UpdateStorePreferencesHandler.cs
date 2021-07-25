using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OpenStore.Application;
using OpenStore.Application.Crud;
using OpenStore.Omnichannel.Domain.StoreContext;

namespace OpenStore.Omnichannel.Application.Command.StoreContext
{
    public class UpdateStorePreferencesHandler : IRequestHandler<UpdateStorePreferences>
    {
        private readonly ICrudRepository<StorePreferences> _repository;
        private readonly IOpenStoreObjectMapper _mapper;

        public UpdateStorePreferencesHandler(ICrudRepository<StorePreferences> repository, IOpenStoreObjectMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateStorePreferences request, CancellationToken cancellationToken)
        {
            var storePreferences = await _repository.Query.FirstOrDefaultAsync(cancellationToken);

            storePreferences = _mapper.Map(request.Model, storePreferences);

            storePreferences.Contact = new StorePreferencesContact
            {
                Email = storePreferences.Contact.Email,
                Address = storePreferences.Contact.Address,
                Phone = storePreferences.Contact.Phone,
                CopyrightText = storePreferences.Contact.CopyrightText,
                FacebookUrl = storePreferences.Contact.FacebookUrl,
                InstagramUrl = storePreferences.Contact.InstagramUrl,
                TwitterUrl = storePreferences.Contact.TwitterUrl,
                YoutubeUrl = storePreferences.Contact.YoutubeUrl,
            };

            await _repository.UpdateAsync(storePreferences, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}