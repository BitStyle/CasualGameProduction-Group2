using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransparency : MonoBehaviour
{
  
        public float DistanceToPlayer = 5.0f;
        public Material TransparentMaterial = null;
        public float FadeInTimeout = 0.6f;
        public float FadeOutTimeout = 0.2f;
        public float TargetTransparency = 0.3f;
        private void Update()
        {
            RaycastHit[] hits; //Store array of objects hit by raycast

            //If object is hit by ray within given player distance
            hits = Physics.RaycastAll(transform.position, transform.forward, DistanceToPlayer);
            foreach (RaycastHit hit in hits)
            {
                //Get render
                Renderer R = hit.collider.GetComponent<Renderer>();
                if (R == null)
                {
                    continue;
                }
                // no renderer attached? go to next hit 
                AutoTransparent AT = R.GetComponent<AutoTransparent>();
                if (AT == null) // if no script is attached to object, attach one
                {
                    AT = R.gameObject.AddComponent<AutoTransparent>();
                    AT.TransparentMaterial = TransparentMaterial;
                    AT.FadeInTimeout = FadeInTimeout;
                    AT.FadeOutTimeout = FadeOutTimeout;
                    AT.TargetTransparency = TargetTransparency;
                }
                AT.BeTransparent(); // get called every frame to reset the falloff after object leaves range
            }
        }
    
}
