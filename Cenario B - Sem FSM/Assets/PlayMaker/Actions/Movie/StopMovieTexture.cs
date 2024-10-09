// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

#if !(UNITY_SWITCH || UNITY_TVOS || UNITY_IPHONE || UNITY_IOS || UNITY_ANDROID || UNITY_FLASH || UNITY_PS3 || UNITY_PS4 || UNITY_XBOXONE || UNITY_BLACKBERRY || UNITY_METRO || UNITY_WP8 || UNITY_PSM || UNITY_WEBGL)

using System;
using UnityEngine;
using UnityEngine.Video;  // <-- Adicionado para incluir o VideoPlayer

#pragma warning disable 618

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Movie)]
    [Tooltip("Stops playing the Movie Texture, and rewinds it to the beginning.")]
    public class StopVideoPlayer : FsmStateAction
    {
        [RequiredField]
        [ObjectType(typeof(VideoPlayer))]
        public FsmObject videoPlayer;

        public override void Reset()
        {
            videoPlayer = null;
        }

        public override void OnEnter()
        {
            var movie = videoPlayer.Value as VideoPlayer;

            if (movie != null)
            {
                movie.Stop();
            }

            Finish();
        }
    }
}

#endif