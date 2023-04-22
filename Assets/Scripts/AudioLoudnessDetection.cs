using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        float rmsValue = 0.0f;

    #if UNITY_WEBGL && !UNITY_EDITOR
        float maxAmplitudeWebGL = 1.0f; // Assuming WebGL returns values in the range of -1 to 1
        rmsValue = CalculateRMS(Microphone.volumes) / maxAmplitudeWebGL;
        Debug.Log(rmsValue);
    #else
        string microphoneName = Microphone.devices[0];
        float maxAmplitudeNonWebGL = 0.8f;
        rmsValue = GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), microphoneClip) / maxAmplitudeNonWebGL;
        Debug.Log(rmsValue);
    #endif

        return rmsValue;
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

        if (!clip.GetData(waveData, startPosition))
        {
            return 0.0f;
        }

        return CalculateRMS(waveData);
    }

    private float CalculateRMS(float[] samples)
    {
        float sum = 0.0f;

        for (int i = 0; i < samples.Length; i++)
        {
            sum += samples[i] * samples[i];
        }

        return Mathf.Sqrt(sum / samples.Length);
    }
}
