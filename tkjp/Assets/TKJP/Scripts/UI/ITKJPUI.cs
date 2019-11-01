namespace TKJP.UI
{
    public interface ITKJPGrab
    {
        void OnGrabBegin();
        void OnGrab();
        void OnGrabEnd();
    }
    public interface ITKJPTouch
    {
        void OnTouchBegin();
        void OnTouch();
        void OnTouchEnd();
    }
}