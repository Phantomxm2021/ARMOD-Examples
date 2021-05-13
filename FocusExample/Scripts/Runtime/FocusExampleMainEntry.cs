using UnityEngine;
using com.Phantoms.ActionNotification.Runtime;
using com.Phantoms.ARMODAPI.Runtime;
using PlaceExample;
using UnityEngine.UI;

namespace FocusExample
{
    public class FocusExampleMainEntry
    {
        private readonly API api = new API();

        private GameObject augmentedRealityObject;
        private Transform augmentedRealityObjectTrans;
        private Transform focusGroupTrans;

        private GameObject uiCanvasGameObject;
        private GameObject focusGroup;
        private GameObject augmentedRealityUIView;
        private GameObject focusFoundGameObject;
        private GameObject focusFindingGameObject;

        private Button placeCheckerButton;
        private GameObject placeCheckerButtonGameObject;

        private Vector3 focusWorldPosition;
        private Quaternion focusWorldRotation;

        private AssetsKey assetsKey;

        private bool placed;

        //Please delete the function if it is not used
        public void OnLoad()
        {
            assetsKey = new AssetsKey();
            var tmp_ProjectName = assetsKey.ProjectName;

            api.LoadGameObject(tmp_ProjectName,
                assetsKey.Canvas,
                _canvasPrefab =>
                {
                    uiCanvasGameObject = api.InstanceGameObject(_canvasPrefab, string.Empty, null);
                    augmentedRealityUIView = api.FindGameObjectByName(assetsKey.ARView);
                    placeCheckerButtonGameObject = api.FindGameObjectByName(assetsKey.PlaceButton);
                    placeCheckerButton = placeCheckerButtonGameObject.GetComponent<Button>();
                    placeCheckerButton.onClick.AddListener(() =>
                    {
                        focusGroup.SetActive(false);
                        placeCheckerButtonGameObject.SetActive(false);
                        augmentedRealityUIView.gameObject.SetActive(false);

                        augmentedRealityObject.SetActive(true);
                        augmentedRealityObjectTrans.position = focusWorldPosition;
                        augmentedRealityObjectTrans.rotation = focusWorldRotation;
                        placed = true;
                    });
                });

            api.LoadGameObject(tmp_ProjectName,
                assetsKey.FocusGroup,
                _focusPrefab =>
                {
                    focusGroup = api.InstanceGameObject(_focusPrefab, string.Empty, null);
                    focusGroupTrans = focusGroup.transform;

                    focusFindingGameObject = api.FindGameObjectByName(assetsKey.Finding);
                    focusFoundGameObject = api.FindGameObjectByName(assetsKey.Found);
                });

            api.LoadGameObject(tmp_ProjectName,
                assetsKey.PlaceObjectName,
                _arObject =>
                {
                    augmentedRealityObject = api.InstanceGameObject(_arObject, string.Empty, null);
                    augmentedRealityObjectTrans = augmentedRealityObject.transform;
                    augmentedRealityObject.SetActive(false);
                });
        }

        //Please delete the function if it is not used
        public void OnEvent(BaseNotificationData _data)
        {
            //General event callback
            if (placed) return;

            if (focusGroup == null) return;
            if (focusFindingGameObject == null) return;
            if (focusFoundGameObject == null) return;
            if (focusGroupTrans == null) return;
            if (!(_data is FocusResultNotificationData tmp_FocusResultNotificationData)) return;
            switch (tmp_FocusResultNotificationData.FocusState)
            {
                case FindingType.Finding:
                    focusFoundGameObject.SetActive(false);
                    focusFindingGameObject.SetActive(true);
                    placeCheckerButtonGameObject.SetActive(false);
                    break;
                case FindingType.Found:
                    focusFoundGameObject.SetActive(true);
                    focusFindingGameObject.SetActive(false);
                    placeCheckerButtonGameObject.SetActive(true);
                    break;
                case FindingType.Limit:
                    focusGroup.SetActive(false);
                    placeCheckerButtonGameObject.SetActive(false);
                    break;
            }

            if (!focusGroup.activeSelf)
                focusGroup.SetActive(tmp_FocusResultNotificationData.FocusState ==
                                     FindingType.Limit);

            focusWorldPosition = tmp_FocusResultNotificationData.FocusPos;
            focusWorldRotation = tmp_FocusResultNotificationData.FocusRot;

            focusGroupTrans.position = focusWorldPosition;
            focusGroupTrans.rotation = focusWorldRotation;
        }
    }
}