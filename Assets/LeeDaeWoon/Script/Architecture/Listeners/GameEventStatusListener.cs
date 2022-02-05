using UnityEngine;
using UnityEngine.Events;

namespace CCGKit
{
    public class GameEventStatusListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        public GameEventStatus Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent<StatusTemplate, int> Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(StatusTemplate status, int value)
        {
            Response.Invoke(status, value);
        }
    }
}