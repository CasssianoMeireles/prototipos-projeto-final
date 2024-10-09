using System;
using UnityEngine;
using UnityEngine.Video;  // Adiciona o VideoPlayer

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Movie)]
    [Tooltip("Pauses the Video Player.")]
    public class PauseVideoPlayer : FsmStateAction
    {
        [RequiredField]
        [ObjectType(typeof(VideoPlayer))]  // Agora usa VideoPlayer
        public FsmObject videoPlayer;

        public override void Reset()
        {
            videoPlayer = null;
        }

        public override void OnEnter()
        {
            var video = videoPlayer.Value as VideoPlayer;

            if (video != null)
            {
                video.Pause();  // Pausa o vídeo
            }

            Finish();
        }
    }
}