using System.Collections.Generic;
using UnityEngine.Events;

namespace Nosirrahh.UnityEnvironmentSettings.Runtime
{
    /// <summary>
    /// Em construção.
    /// </summary>
    public interface IEnvironmentLoader
    {
        /// <summary>
        /// Em construção.
        /// </summary>
        /// <param name="settings">Em construção.</param>
        public void Init (Dictionary<string, string> settings);
        /// <summary>
        /// Em construção.
        /// </summary>
        /// <param name="onCompleted">Em construção.</param>
        public void Load (UnityAction<EnvironmentSettings> onCompleted);
    }
}