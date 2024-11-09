using Nosirrahh.UnityEnvironmentSettings.Runtime;

namespace Nosirrahh.UnityEnvironmentSettings.Editor
{
    /// <summary>
    /// Em construção.
    /// </summary>
    public interface IEnvironmentProvider
    {
        /// <summary>
        /// Em construção.
        /// </summary>
        /// <param name="environmentSettings">Em construção.</param>
        public void GetEnvironmentSettings (out EnvironmentSettings environmentSettings);
    }
}