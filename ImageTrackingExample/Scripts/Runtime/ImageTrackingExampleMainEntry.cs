using UnityEngine;
using System;
using System.Collections;
using com.Phantoms.ActionNotification.Runtime;
using com.Phantoms.ARMODAPI.Runtime;

namespace ImageTrackingExample
{
    public class ImageTrackingExampleMainEntry
    {
        private API api = new API();
        private const string CONST_MARKER_NAME = "NincadaCard";
        private const string CONST_LOAD_ASSET_NAME = "NincadaCard";
        private const string CONST_PROJECT_NAME = "ImageTrackingExample";

        private GameObject nincadaCard;
        private Transform nincadaCardTrans;

        private void OnLoad()
        {
            api.LoadGameObject(CONST_PROJECT_NAME, CONST_LOAD_ASSET_NAME, _result =>
            {
                nincadaCard = api.InstanceGameObject(_result, "", null);
                nincadaCard.SetActive(false);
                nincadaCardTrans = nincadaCard.transform;
            });
        }

        //Please delete the function if it is not used
        public void OnEvent(BaseNotificationData _data)
        {
            if (!(_data is MarkerNotificationData tmp_MarkerNotificationData)) return;
            if (!tmp_MarkerNotificationData.MarkerName.Equals(CONST_MARKER_NAME)) return;
            if (!nincadaCardTrans) return;
            if (!nincadaCardTrans.parent)
            {
                nincadaCardTrans.SetParent(tmp_MarkerNotificationData.MarkerTrackable);
                nincadaCardTrans.localPosition = Vector3.zero;
                nincadaCardTrans.localRotation = Quaternion.identity;
            }

            var tmp_NincadaCardState = nincadaCard.activeSelf;
            switch (tmp_MarkerNotificationData.MarkerState)
            {
                case MarkerTrackingState.Limited:
                    if (tmp_NincadaCardState)
                        nincadaCard.SetActive(false);
                    break;
                case MarkerTrackingState.Tracking:
                    if (!tmp_NincadaCardState)
                        nincadaCard.SetActive(true);
                    break;
                case MarkerTrackingState.Added:
                    if (!tmp_NincadaCardState)
                        nincadaCard.SetActive(true);
                    break;
            }
        }
    }
}