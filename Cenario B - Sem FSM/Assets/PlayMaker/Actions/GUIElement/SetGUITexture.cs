using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Sets the Texture used by the Image component attached to a Game Object.")]
	public class SetImageTexture : ComponentAction<Image>
	{
		[RequiredField]
		[CheckForComponent(typeof(Image))]
		[Tooltip("The GameObject that owns the Image.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Texture to apply.")]
		public FsmTexture texture;

		public override void Reset()
		{
			gameObject = null;
			texture = null;
		}

		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(go))
			{
				var imageComponent = cachedComponent;
				imageComponent.sprite = Sprite.Create((Texture2D)texture.Value, new Rect(0, 0, texture.Value.width, texture.Value.height), new Vector2(0.5f, 0.5f));
			}

			Finish();
		}
	}
}