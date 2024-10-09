using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.UI)]
    [Tooltip("Sends events when a UI element (Text or Image) is touched. Optionally filter by a fingerID.")]
    public class TouchGUIEvent : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(UnityEngine.UI.Graphic))]
        [Tooltip("The Game Object that owns the UI element (Text or Image).")]
        public FsmOwnerDefault gameObject;

        [Tooltip("Only detect touches that match this fingerID, or set to None.")]
        public FsmInt fingerId;

        [ActionSection("Events")]

        [Tooltip("Event to send on touch began.")]
        public FsmEvent touchBegan;

        [Tooltip("Event to send on touch moved.")]
        public FsmEvent touchMoved;

        [Tooltip("Event to send on stationary touch.")]
        public FsmEvent touchStationary;

        [Tooltip("Event to send on touch ended.")]
        public FsmEvent touchEnded;

        [Tooltip("Event to send on touch cancel.")]
        public FsmEvent touchCanceled;

        [Tooltip("Event to send if not touching (finger down but not over the UI element).")]
        public FsmEvent notTouching;

        [ActionSection("Store Results")]

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the fingerId of the touch.")]
        public FsmInt storeFingerId;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the screen position where the UI element was touched.")]
        public FsmVector3 storeHitPoint;

        [Tooltip("Normalize the hit point screen coordinates (0-1).")]
        public FsmBool normalizeHitPoint;

        [Tooltip("How to measure the offset.")]
        public OffsetOptions relativeTo;

        public enum OffsetOptions
        {
            TopLeft,
            Center,
            TouchStart
        }

        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        private Vector3 touchStartPos;
        private UnityEngine.UI.Graphic uiElement;

        public override void Reset()
        {
            gameObject = null;
            fingerId = new FsmInt { UseVariable = true };
            touchBegan = null;
            touchMoved = null;
            touchStationary = null;
            touchEnded = null;
            touchCanceled = null;
            storeFingerId = null;
            storeHitPoint = null;
            normalizeHitPoint = false;
            relativeTo = OffsetOptions.Center;
            everyFrame = true;
        }

        public override void OnEnter()
        {
            DoTouchGUIEvent();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoTouchGUIEvent();
        }

        void DoTouchGUIEvent()
        {
            if (Input.touchCount > 0)
            {
                var go = Fsm.GetOwnerDefaultTarget(gameObject);
                if (go == null)
                {
                    return;
                }

                uiElement = go.GetComponent<UnityEngine.UI.Graphic>();

                if (uiElement == null)
                {
                    return;
                }

                foreach (var touch in Input.touches)
                {
                    DoTouch(touch);
                }
            }
        }

        void DoTouch(Touch touch)
        {
            if (fingerId.IsNone || touch.fingerId == fingerId.Value)
            {
                Vector3 touchPos = touch.position;

                if (RectTransformUtility.RectangleContainsScreenPoint(uiElement.rectTransform, touchPos))
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        touchStartPos = touchPos;
                    }

                    storeFingerId.Value = touch.fingerId;

                    if (normalizeHitPoint.Value)
                    {
                        touchPos.x /= Screen.width;
                        touchPos.y /= Screen.height;
                    }

                    storeHitPoint.Value = touchPos;

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            Fsm.Event(touchBegan);
                            return;

                        case TouchPhase.Moved:
                            Fsm.Event(touchMoved);
                            return;

                        case TouchPhase.Stationary:
                            Fsm.Event(touchStationary);
                            return;

                        case TouchPhase.Ended:
                            Fsm.Event(touchEnded);
                            return;

                        case TouchPhase.Canceled:
                            Fsm.Event(touchCanceled);
                            return;
                    }
                }
                else
                {
                    Fsm.Event(notTouching);
                }
            }
        }
    }
}