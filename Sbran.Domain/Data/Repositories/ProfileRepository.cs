using Sbran.Domain.Data.Adapters;
using Sbran.Domain.Data.Repositories.Contracts;
using Sbran.Domain.Entities.System;
using Sbran.Domain.Models;
using Sbran.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Sbran.Domain.Data.Repositories
{
	public class ProfileRepository : IProfileRepository
    {
        private readonly SystemContext _systemContext;

        public ProfileRepository(
            SystemContext systemContext)
        {
            _systemContext = systemContext;
        }

		public Profile Create()
		{
			var createdProfile = new Profile();

			_systemContext.Profiles.Add(createdProfile);

			return createdProfile;
		}

        public Profile Add(ProfileDto newProfileData)
        {
            var profile = Create();

            var avatar = Convert.FromBase64String(newProfileData.Avatar);

            profile.SetPhoto(avatar);
            profile.SetWebPages(newProfileData.WebPages);

            return profile;
        }

        public async Task<Profile[]> GetAllAsync()
        {
            var profiles = await _systemContext.Profiles.ToArrayAsync();

            /*проверка на NULL*/

            return profiles;
        }

        public async Task<Profile> GetAsync(Guid id)
        {
            var profile = await _systemContext.Profiles.SingleOrDefaultAsync(ctx => ctx.Id == id);

            /*проверка на NULL*/

            return profile;
        }

        public async Task UpdateAsync(Guid profileId, ProfileDto newProfileData)
        {
            Contract.Argument.IsNotEmptyGuid(profileId, nameof(profileId));
            Contract.Argument.IsNotNull(newProfileData, nameof(newProfileData));

            var oldProfileData = await GetAsync(profileId);

            var avatar = Convert.FromBase64String(newProfileData.Avatar);

            oldProfileData.SetPhoto(avatar);
            oldProfileData.SetWebPages(newProfileData.WebPages);
        }
    }
}
