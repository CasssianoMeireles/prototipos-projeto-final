using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Sets the Text used by the Text Component attached to a Game Object.")]
	public class SetText : ComponentAction<Text>
	{
		[RequiredField]
		[CheckForComponent(typeof(Text))]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.TextArea)]
		public FsmString text;

		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			text = "";
		}

		public override void OnEnter()
		{
			DoSetText();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetText();
		}

		void DoSetText()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				var textComponent = cachedComponent;
				textComponent.text = text.Value;
			}
		}
	}
}