using UnityEngine;
using System.Collections;

namespace MoreMountains.CorgiEngine
{   
    public class DialogueBoxRainland : DialogueBox {

    	
        void LateUpdate()
        {
            transform.position = Camera.main.transform.position;
        }
    }
}