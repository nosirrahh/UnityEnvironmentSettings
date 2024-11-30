using Nosirrahh.UnityEnvironmentSettings.Runtime;

namespace Nosirrahh.UnityEnvironmentSettings.Editor
{
    /// <summary>
    /// Defines an interface for environment configuration providers, responsible for supplying 
    /// the current environment settings in a standardized way.
    /// </summary>
    public interface IEnvironmentProvider
    {
        /// <summary>
        /// Retrieves the current environment settings and returns them via an output parameter.
        /// </summary>
        /// <param name="environmentSettings">The current environment settings, provided when the method is called.</param>
        public void GetEnvironmentSettings (out EnvironmentSettings environmentSettings);
    }
}