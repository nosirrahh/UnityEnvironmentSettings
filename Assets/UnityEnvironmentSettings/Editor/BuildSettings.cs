using UnityEngine;

namespace Nosirrahh.UnityEnvironmentSettings.Editor
{
    /// <summary>
    /// Represents the build settings that define which environment provider will be used.
    /// The environment provider can be of the Editor or Doppler type.
    /// </summary>
    [System.Serializable]
    public class BuildSettings
    {
        #region Enumerators

        // TODO: MOVER PARA UMA INTERFACE?
        /// <summary>
        /// Enumeration that defines the available types of environment providers.
        /// </summary>
        public enum ProviderType
        {
            /// <summary>
            /// Environment provider based on the editor.
            /// </summary>
            Editor = 0,
            /// <summary>
            /// Environment provider based on the Doppler API.
            /// </summary>
            Doppler = 1,
        }

        #endregion

        #region Fields

        /// <summary>
        /// Defines the type of environment provider to be used (Editor or Doppler).
        /// </summary>
        [SerializeField]
        private ProviderType provider;

        // TODO: MOVER PARA UMA INTERFACE?
        /// <summary>
        /// Environment provider that uses local editor configurations.
        /// </summary>
        public EditorEnvironmentProvider editorEnvironmentProvider;
        /// <summary>
        /// Environment provider that uses the Doppler API to fetch configurations.
        /// </summary>
        public DopplerEnvironmentProvider dopplerEnvironmentProvider;

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the environment provider based on the type defined in the <see cref="provider"/> field.
        /// Depending on the selected provider type, the appropriate provider will be returned:
        /// <see cref="EditorEnvironmentProvider"/> or <see cref="DopplerEnvironmentProvider"/>.
        /// </summary>
        /// <returns>
        /// An object that implements the <see cref="IEnvironmentProvider"/> interface corresponding to the provider type.
        /// Returns <c>null</c> if the provider type is invalid.
        /// </returns>
        public IEnvironmentProvider GetProvider ()
        {
            switch (provider)
            {
                case ProviderType.Editor:
                    return editorEnvironmentProvider;
                case ProviderType.Doppler:
                    return dopplerEnvironmentProvider;
                default:
                    return null;
            }
        }

        #endregion
    }
}

