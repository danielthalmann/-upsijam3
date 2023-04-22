using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoudnessDetection : MonoBehaviour
{
    public int sampleWindow = 64;

    private AudioClip microphoneClip;

    #if UNITY_WEBGL && !UNITY_EDITOR
    void Awake()
    {
        Microphone.Init();
        Microphone.QueryAudioInput();
    }
    #endif

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
#else
        MicrophoneToAudioClip();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        Microphone.Update();
        #endif
    }

#if UNITY_WEBGL && !UNITY_EDITOR
#else
    public void MicrophoneToAudioClip()
    {
        string microphoneName = Microphone.devices[0];
        microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
    }
#endif

    public float GetLoudnessFromMicrophone()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return Microphone.volumes[0];
#else
        string microphoneName = Microphone.devices[0];
        return GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip);
#endif
    }

    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - sampleWindow;
        float[] waveData = new float[sampleWindow];

        clip.GetData(waveData, startPosition);

        float totalLoudness = 0;

        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / sampleWindow;
    }
}
