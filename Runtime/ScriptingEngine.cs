using UnityEngine;
using Unity.VisualScripting;

namespace VisualPinball.Unity.VisualScripting
{
    [AddComponentMenu("Visual Pinball/Scripting Engine")]
    public class ScriptingEngine : MonoBehaviour
    {
        public IGamelogicEngine _gamelogicEngine;

        private void OnEnable()
        {
            _gamelogicEngine = GetComponentInParent<IGamelogicEngine>();
            _gamelogicEngine.OnLampChanged += OnLampChanged;
        }

        private void OnDisable()
        {
            _gamelogicEngine.OnLampChanged -= OnLampChanged;
        }

        private void OnLampChanged(object sender, LampEventArgs e)
        {

            EventBus.Trigger(EventNames.LampEvent, e);
        }
    }
}
