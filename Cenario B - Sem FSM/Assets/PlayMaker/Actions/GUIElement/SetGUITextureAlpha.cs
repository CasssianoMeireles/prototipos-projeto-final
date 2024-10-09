using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Sets the Alpha of the Image component attached to a Game Object.")]
	public class SetImageAlpha : ComponentAction<Image>
	{
		[RequiredField]
		[CheckForComponent(typeof(Image))]
		public FsmOwnerDefault gameObject;
		[RequiredField]
		public FsmFloat alpha;
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			alpha = 1.0f;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetAlpha();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetAlpha();
		}

		void DoSetAlpha()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				var imageComponent = cachedComponent;
				var color = imageComponent.color;
				imageComponent.color = new Color(color.r, color.g, color.b, alpha.Value);
			}
		}
	}
}