using Nosirrahh.UnityEnvironmentSettings.Runtime;

namespace Nosirrahh.UnityEnvironmentSettings.Editor
{
    /// <summary>
    /// Em construção.
    /// </summary>
    public interface IEnvironmentBuilder
    {
        /// <summary>
        /// Em construção.
        /// </summary>
        /// <param name="settings">Em construção.</param>
        /// <param name="path">Em construção.</param>
        public bool Build (EnvironmentSettings settings, string path = null);

        /// <summary>
        /// Em construção.
        /// </summary>
        public void Destroy ();
    }
}

