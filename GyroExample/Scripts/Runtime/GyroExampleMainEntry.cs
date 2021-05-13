using UnityEngine;
using System;
using System.Collections;
using com.Phantoms.ActionNotification.Runtime;
using com.Phantoms.ARMODAPI.Runtime;

namespace GyroExample
{
    public class GyroExampleMainEntry
    {
        private const string CONST_PROJECT_NAME = "GyroExample";
        private const string CONST_AR_VIRTUAL_OBJECT_NAME = "SkyMeshes";
        private const string CONST_TRACKABLES = "Trackables";
        private API api = new API();

        private GameObject skyMesh;

        private Transform trackables;

        //Please delete the function if it is not used
        public void OnLoad()
        {
            trackables = GameObject.Find(CONST_TRACKABLES).transform;
            //Use this for initialization
            api.LoadAsset<GameObject>(CONST_PROJECT_NAME, CONST_AR_VIRTUAL_OBJECT_NAME, _skyMesh =>
            {
                skyMesh = api.InstanceGameObject(_skyMesh, string.Empty, trackables);
                skyMesh.transform.localPosition = Vector3.zero;
                skyMesh.transform.localRotation = Quaternion.identity;
            });
        }
    }
}