using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TKJP
{
    public class Settings
    {
        public enum Device { None, Editor, Quest, Go, Else, }
        public static Device device;
    }
}
