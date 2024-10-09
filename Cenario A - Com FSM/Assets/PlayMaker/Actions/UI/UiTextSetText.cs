using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.UI)]
    [Tooltip("Sets the text value of a UI Text component.")]
    public class UiTextSetText : ComponentAction<UnityEngine.UI.Text>
    {
        [RequiredField]
        [CheckForComponent(typeof(UnityEngine.UI.Text))]
        [Tooltip("The GameObject with the UI Text component.")]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [Tooltip("The text value to set.")]
        public FsmString text;

        [Tooltip("Repeats every frame.")]
        public bool everyFrame;

        private new UnityEngine.UI.Text uiText;

        public override void Reset()
        {
            gameObject = null;
            text = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            SetTextValue();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            SetTextValue();
        }

        void SetTextValue()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                uiText = cachedComponent;
                uiText.text = text.Value;
            }
        }
    }
}