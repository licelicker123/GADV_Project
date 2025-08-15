using UnityEngine;
using UnityEngine.Video;

public class PlayJumpscare: MonoBehaviour
{
    public string videoFileName = "Videos/jumpscare"; // name of jumpscare video
    private VideoPlayer videoPlayer;
    public AudioSource jumpscareSfx; //jumpscare audio

    void Start()
    {
        // make sure have video player component
        videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer == null)
        {
            videoPlayer = gameObject.AddComponent<VideoPlayer>();
        }

        // play on the screen
        videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        videoPlayer.targetCamera = Camera.main;
        videoPlayer.playOnAwake = false;
    }

    public void Jumpscare()
    {
        // get the vid from resources
        VideoClip clip = Resources.Load<VideoClip>(videoFileName);
        if (clip != null)
        {
            videoPlayer.clip = clip;
            videoPlayer.Play(); //play video 

            if (jumpscareSfx != null)
            {
                jumpscareSfx.Play(); //play audio
            }
            else
            {
                Debug.LogWarning("No AudioSource assigned for jumpscare");
            }
        }
        else
        {
            Debug.LogError("Video file not found in Resources: " + videoFileName);
        }
    }
}
