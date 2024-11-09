using UnityEngine;

namespace Nosirrahh.UnityEnvironmentSettings.Editor
{
    [System.Serializable]
    public class BuildSettings
    {
        public enum ProviderType
        {
            Editor = 0,
            Doppler = 1,
        }

        /// <summary>
        /// Em contrução.
        /// </summary>
        [SerializeField]
        private ProviderType provider;

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

        // TODO: MOVER PARA UMA INTERFACE?
        public EditorEnvironmentProvider editorEnvironmentProvider;
        public DopplerEnvironmentProvider dopplerEnvironmentProvider;
    }
}

