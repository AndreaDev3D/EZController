using UnityEngine;

namespace EZController.Modules
{
    public class EZAudioModule : EZBaseModule
    {
        public AudioSource AudioSource;
        public AudioClip IdleSound;
        public AudioClip EngineSound;
        public AudioClip SkidmarkSound;

        public KeyCode BrakeKey = KeyCode.Space;

        void Start()
        {
            Start();
        }

        void Update()
        {
            AudioSkidmarks();
        }

        private void AudioSkidmarks()
        {
            if (Controller.AngularVelocity.y >= 1.0f || Controller.AngularVelocity.y <= -1.0f)
            {
                PlaySkidmark();
            }
            else
            {
                StopSkidmark();
            }

            if (Input.GetKeyDown(BrakeKey))
            {
                PlaySkidmark();
            }
            if (Input.GetKeyUp(BrakeKey))
            {
                StopSkidmark();
            }
        }

        private void PlaySkidmark()
        {
            AudioSource.clip = SkidmarkSound;
            AudioSource.Play();
        }

        private void StopSkidmark()
        {
            if (AudioSource.isPlaying)
            {
                AudioSource.Stop();
            }
        }
    }
}