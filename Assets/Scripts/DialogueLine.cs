using UnityEngine;
using System.Collections;

namespace MoreMountains.CorgiEngine
{ 
    [System.Serializable]
    public class DialogueLine {
        public CorgiController character;

        [TextArea(3,10)]
        public string text;
    }
}