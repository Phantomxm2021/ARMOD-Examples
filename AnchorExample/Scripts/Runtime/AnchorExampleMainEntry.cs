using UnityEngine;
using System;
using com.Phantoms.ActionNotification.Runtime;
using com.Phantoms.ARMODAPI.Runtime;

namespace AnchorExample
{
    public class AnchorExampleMainEntry
    {
        internal static API API = new API();

        private GameObject linePrefab;
        private const string CONST_PROJECT_NAME = "AnchorExample";
        private const string CONST_ANCHOR_ASSET = "Anchor";

        private GameObject anchor;

        //Please delete the function if it is not used
        public void OnLoad()
        {
            //Use this for initialization
            API.LoadAsset<GameObject>(CONST_PROJECT_NAME,CONST_ANCHOR_ASSET, _cubePrefab =>
            {
                anchor = _cubePrefab;
            });
            
        }
        
        //Please delete the function if it is not used
        public void OnUpdate()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            var tmp_AnchorData = new AnchorNotificationData
            {
                Position = Input.mousePosition,
                StickType = AnchorNotificationData.StickTypeEnum.ByTrackableType,
                TrackableType = AnchorNotificationData.TrackableTypeEnum.PlaneWithinBounds,
                ControllerTargetNode = API.InstanceGameObject(anchor, String.Empty, null),
            };
            API.StickObject(tmp_AnchorData);
        }
    }
}