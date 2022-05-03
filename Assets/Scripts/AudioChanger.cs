using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using easyar;
using UnityEngine;

public class AudioChanger : MonoBehaviour
{
    private ImageTrackerFrameFilter _filter;

    private Dictionary<string, AudioClip> _imageToAudio;

    public AudioClip huTao;
    public AudioClip inazuma;
    public AudioClip onay;
    public AudioSource current;

    public void Start()
    {
        _imageToAudio = new Dictionary<string, AudioClip>();
        _filter = gameObject.GetComponent<ImageTrackerFrameFilter>();
        if (_filter != null)
        {
            Debug.Log("Success");
        }

        if (huTao != null)
        {
            _imageToAudio["Hu Tao"] = huTao;
        }

        if (inazuma != null)
        {
            _imageToAudio["Ayaka"] = inazuma;
        }

        if (onay != null)
        {
            _imageToAudio["Onay"] = onay;
        }
    }

    public void Update()
    {
        var theme = _filter!.TargetControllers
            .Where(target => target.IsTracked)
            .Select(target => target.name)
            .FirstOrDefault();
        Debug.Log(theme);
        if (theme != null)
        {
            Debug.Log(current.clip != _imageToAudio[theme]);
            if (current.clip != _imageToAudio[theme])
            {
                current.clip = _imageToAudio[theme];
                current.Play();
            }
        }
        else
        {
            current.Pause();
        }
    }
}
