using System;
using UnityEngine;
using UnityEngine.Video;  // Adiciona o namespace do VideoPlayer

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Material)]
    [Tooltip("Sets a named texture in a game object's material to a video texture.")]
    public class SetMaterialVideoTexture : ComponentAction<Renderer>
    {
        [Tooltip("The GameObject that the material is applied to.")]
        [CheckForComponent(typeof(Renderer))]
        public FsmOwnerDefault gameObject;

        [Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
        public FsmInt materialIndex;

        [Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
        public FsmMaterial material;

        [UIHint(UIHint.NamedTexture)]
        [Tooltip("A named texture in the shader.")]
        public FsmString namedTexture;

        [RequiredField]
        [ObjectType(typeof(VideoPlayer))]  // Agora utiliza VideoPlayer
        public FsmObject videoPlayer;

        public override void Reset()
        {
            gameObject = null;
            materialIndex = 0;
            material = null;
            namedTexture = "_MainTex";
            videoPlayer = null;
        }

        public override void OnEnter()
        {
            DoSetMaterialTexture();
            Finish();
        }

        void DoSetMaterialTexture()
        {
            var video = videoPlayer.Value as VideoPlayer;

            var namedTex = namedTexture.Value;
            if (string.IsNullOrEmpty(namedTex)) namedTex = "_MainTex";

            if (material.Value != null)
            {
                material.Value.SetTexture(namedTex, video.texture);  // Acessa a textura de video
                return;
            }

            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (!UpdateCache(go))
            {
                return;
            }

            if (renderer.material == null)
            {
                LogError("Missing Material!");
                return;
            }

            if (materialIndex.Value == 0)
            {
                renderer.material.SetTexture(namedTex, video.texture);
            }
            else if (renderer.materials.Length > materialIndex.Value)
            {
                var materials = renderer.materials;
                materials[materialIndex.Value].SetTexture(namedTex, video.texture);
                renderer.materials = materials;
            }
        }
    }
}