using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.UI)]
    [Tooltip("Gets the text value of a UI Text component.")]
    public class UiTextGetText : ComponentAction<UnityEngine.UI.Text>
    {
        [RequiredField]
        [CheckForComponent(typeof(UnityEngine.UI.Text))]
        [Tooltip("The GameObject with the UI Text component.")]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the text value in a string variable.")]
        public FsmString text;

        [Tooltip("Repeat every frame.")]
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
            GetTextValue();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            GetTextValue();
        }

        void GetTextValue()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                uiText = cachedComponent;
                text.Value = uiText.text;
            }
        }
    }
}