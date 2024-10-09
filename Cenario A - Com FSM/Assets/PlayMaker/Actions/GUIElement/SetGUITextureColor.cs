using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Sets the Color of the Image component attached to a Game Object.")]
	public class SetImageColor : ComponentAction<Image>
	{
		[RequiredField]
		[CheckForComponent(typeof(Image))]
		public FsmOwnerDefault gameObject;
		[RequiredField]
		public FsmColor color;
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			color = Color.white;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetColor();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetColor();
		}

		void DoSetColor()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				var imageComponent = cachedComponent;
				imageComponent.color = color.Value;
			}
		}
	}
}