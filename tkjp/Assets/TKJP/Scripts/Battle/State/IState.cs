using UnityEngine.Events;

namespace TKJP.Battle.State
{
    public interface IState
    {
        void Initialize();
        void OnChanged();
        void OnUpdate();
        bool IsFinish();
        void NextTo();
    }

    public class TimeEvent : UnityEvent<float> { }
}