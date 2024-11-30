using Nosirrahh.UnityEnvironmentSettings.Runtime;

namespace Nosirrahh.UnityEnvironmentSettings.Editor
{
    /// <summary>
    /// Defines the interface for building and destroying an environment configuration, 
    /// enabling the application of custom settings for the build process.
    /// </summary>
    public interface IEnvironmentBuilder
    {
        /// <summary>
        /// Builds the environment based on the provided settings and optionally saves it to a specified path.
        /// </summary>
        /// <param name="settings">An instance of <see cref="EnvironmentSettings"/> containing the environment's configurations.</param>
        /// <param name="path">Optional path where the environment will be saved; defaults to the standard path if not provided.</param>
        /// <returns>Returns <c>true</c> if the environment was successfully built; otherwise, <c>false</c>.</returns>
        public bool Build (EnvironmentSettings settings, string path = null);

        /// <summary>
        /// Destroys the configured environment, releasing resources or reverting changes made during the build process.
        /// </summary>
        public void Destroy ();
    }
}

