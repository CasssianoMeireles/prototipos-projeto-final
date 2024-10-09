using System;
using UnityEngine;
using UnityEngine.Video;  // Adiciona VideoPlayer

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Movie)]
    [Tooltip("Sets the Game Object as the Audio Source associated with the Video Player. The Game Object must have an AudioSource Component.")]
    public class VideoPlayerAudioSettings : FsmStateAction
    {
        [RequiredField]
        [ObjectType(typeof(VideoPlayer))]  // Agora usa VideoPlayer
        public FsmObject videoPlayer;

        [RequiredField]
        [CheckForComponent(typeof(AudioSource))]
        public FsmGameObject gameObject;

        public override void Reset()
        {
            videoPlayer = null;
            gameObject = null;
        }

        public override void OnEnter()
        {
            var video = videoPlayer.Value as VideoPlayer;

            if (video != null && gameObject.Value != null)
            {
                var audio = gameObject.Value.GetComponent<AudioSource>();
                if (audio != null)
                {
                    video.audioOutputMode = VideoAudioOutputMode.AudioSource;  // Associa o áudio ao AudioSource
                    video.SetTargetAudioSource(0, audio);  // Configura o AudioSource para a faixa de áudio
                }
            }

            Finish();
        }
    }
}