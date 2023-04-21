using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string defaultSceneName = "Menu";
    public Animator animator;
    public Canvas canvas;
    public string outAnimationName = "FadeOut";
    public string inAnimationName = "FadeIn";

    private bool onLoadScene = false;
    private bool onStart = true;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (onLoadScene)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName(outAnimationName))
            {
                animator.Play(outAnimationName);
                canvas.enabled = true;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(outAnimationName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
        else
        {
            if(onStart)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName(inAnimationName))
                {
                    if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0)
                    {
                        canvas.enabled = false;
                        onStart = false;
                    }
                }
            }
        }
    }

    public void LoadDefaultScene()
    {
        onLoadScene = true;
        sceneName = defaultSceneName;
    }

    public void LoadScene(string sceneName)
    {
        this.sceneName = sceneName;
        onLoadScene = true;
    }
}
