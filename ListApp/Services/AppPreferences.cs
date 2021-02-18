using ListApp.Constants;
using System;

namespace ListApp.Services
{
    public class AppPreferences : IAppPreferences
    {
        private readonly IPreferenceService _preferenceService;

        public AppPreferences(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService 
                ?? throw new InvalidOperationException("Preference Service cannot be null");
        }

        public bool HasDatabaseBeenInitialized
        {
            get => _preferenceService.Get(PreferenceConstants.HasDatabaseBeenInitialized, false);
            set => _preferenceService.Set(PreferenceConstants.HasDatabaseBeenInitialized, value);
        }
    }
}
