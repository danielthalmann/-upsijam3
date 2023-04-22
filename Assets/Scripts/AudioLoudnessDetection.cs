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
    #if !UNITY_WEBGL || UNITY_EDITOR
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

    public void MicrophoneToAudioClip()
    {
    #if !UNITY_WEBGL || UNITY_EDITOR
        string microphoneName = Microphone.devices[0];
        microphoneClip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
    #endif
    }

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

        if (startPosition < 0)
        {
            startPosition = 0;
        }
        else if (startPosition + sampleWindow > clip.samples)
        {
            startPosition = clip.samples - sampleWindow;
        }

        float[] waveData = new float[sampleWindow];

        if (! clip.GetData(waveData, startPosition))
        {
            return 0.0f;
        }

        float totalLoudness = 0;

        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }

        return totalLoudness / sampleWindow;
    }
}
