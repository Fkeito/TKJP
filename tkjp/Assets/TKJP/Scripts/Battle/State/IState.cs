using UnityEngine.Events;

namespace TKJP.Battle.State
{
    public interface IState
    {
        void Initialize();
        void Start();
        void Update();
        bool IsFinish();
        void NextTo();
    }

    public class TimeEvent : UnityEvent<float> { }
}