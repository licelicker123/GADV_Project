using UnityEngine;
using UnityEngine.Video;

public class PlayVideoOnButton : MonoBehaviour
{
    public string videoFileName = "Videos/jump_scare"; // Name without extension
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Ensure there is a VideoPlayer component
        videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer == null)
        {
            videoPlayer = gameObject.AddComponent<VideoPlayer>();
        }

        // Optional: play directly on the screen
        videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        videoPlayer.targetCamera = Camera.main;
        videoPlayer.playOnAwake = false;
    }

    public void PlayVideo()
    {
        // Load the video from Resources
        VideoClip clip = Resources.Load<VideoClip>(videoFileName);
        if (clip != null)
        {
            videoPlayer.clip = clip;
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("Video file not found in Resources: " + videoFileName);
        }
    }
}
