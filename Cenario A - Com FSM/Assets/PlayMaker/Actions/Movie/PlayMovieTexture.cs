using System;
using UnityEngine;
using UnityEngine.Video;  // Agora usa VideoPlayer

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Movie)]
    [Tooltip("Plays a Video Player. Use the Video Player in a Material, or in the GUI.")]
    public class PlayVideoPlayer : FsmStateAction
    {
        [RequiredField]
        [ObjectType(typeof(VideoPlayer))]  // Agora usa VideoPlayer
        public FsmObject videoPlayer;

        public FsmBool loop;

        public override void Reset()
        {
            videoPlayer = null;
            loop = false;
        }

        public override void OnEnter()
        {
            var video = videoPlayer.Value as VideoPlayer;

            if (video != null)
            {
                video.isLooping = loop.Value;
                video.Play();  // Inicia o vídeo
            }

            Finish();
        }
    }
}