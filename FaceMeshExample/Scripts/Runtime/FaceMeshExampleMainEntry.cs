using UnityEngine;
using com.Phantoms.ActionNotification.Runtime;
using com.Phantoms.ARMODAPI.Runtime;

namespace FaceMeshExample
{
    public class FaceMeshExampleMainEntry
    {
        private API api;
        private GameObject faceMesh;
        private Transform faceMeshTransform;

        //Please delete the function if it is not used
        public void OnLoad()
        {
            //Use this for initialization
            api = new API();
        }
    }
}