using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.UI)]
    [Tooltip("Performs a Hit Test on a Game Object with a UI Text or UI Image component.")]
    public class GUIElementHitTest : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(UnityEngine.UI.Graphic))]
        [Tooltip("The GameObject that has a UI Text or UI Image component.")]
        public FsmOwnerDefault gameObject;
        
        [Tooltip("Specify camera or use MainCamera as default.")]
        public Camera camera;
        
        [Tooltip("A vector position on screen. Usually stored by actions like GetTouchInfo, or World To Screen Point.")]
        public FsmVector3 screenPoint;

        [Tooltip("Specify screen X coordinate.")]
        public FsmFloat screenX;

        [Tooltip("Specify screen Y coordinate.")]
        public FsmFloat screenY;

        [Tooltip("Whether the specified screen coordinates are normalized (0-1).")]
        public FsmBool normalized;

        [Tooltip("Event to send if the Hit Test is true.")]
        public FsmEvent hitEvent;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the result of the Hit Test in a bool variable (true/false).")]
        public FsmBool storeResult;

        [Tooltip("Repeat every frame. Useful if you want to wait for the hit test to return true.")]
        public FsmBool everyFrame;

        private UnityEngine.UI.Graphic uiElement;

        public override void Reset()
        {
            gameObject = null;
            camera = null;
            screenPoint = new FsmVector3 { UseVariable = true };
            screenX = new FsmFloat { UseVariable = true };
            screenY = new FsmFloat { UseVariable = true };
            normalized = true;
            hitEvent = null;
            everyFrame = true;
        }

        public override void OnEnter()
        {
            DoHitTest();

            if (!everyFrame.Value)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoHitTest();
        }

        void DoHitTest()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            uiElement = go.GetComponent<UnityEngine.UI.Graphic>();

            if (uiElement == null)
            {
                Finish();
                return;
            }

            var testPoint = screenPoint.IsNone ? new Vector3(0, 0) : screenPoint.Value;

            if (!screenX.IsNone)
            {
                testPoint.x = screenX.Value;
            }

            if (!screenY.IsNone)
            {
                testPoint.y = screenY.Value;
            }

            if (normalized.Value)
            {
                testPoint.x *= Screen.width;
                testPoint.y *= Screen.height;
            }

            // Perform hit test using RectTransformUtility
            if (RectTransformUtility.RectangleContainsScreenPoint(uiElement.rectTransform, testPoint, camera))
            {
                storeResult.Value = true;
                Fsm.Event(hitEvent);
            }
            else
            {
                storeResult.Value = false;
            }
        }
    }
}