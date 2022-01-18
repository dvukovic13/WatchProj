using DigitalRuby.Tween;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyController : MonoBehaviour
{
    public Button Jump, Death;
    public Animator Animator;

    System.Action<ITween<float>> hideButtonsTween;
    System.Action<ITween<float>> showButtonsTween;
    System.Action<ITween<float>> buttonHidden;
    System.Action<ITween<float>> buttonShown;

    private bool _animStarted = false;

    private void Awake()
    {
        hideButtonsTween = (t) =>
        {
            Color currentColor = Jump.GetComponent<RawImage>().color;
            Color textColor = Jump.GetComponentInChildren<TMP_Text>().color;

            currentColor = new Vector4(currentColor.r, currentColor.g, currentColor.b, t.CurrentValue);
            textColor = new Vector4(textColor.r, textColor.g, textColor.b, t.CurrentValue);

            Jump.GetComponent<RawImage>().color = currentColor;
            Jump.GetComponentInChildren<TMP_Text>().color = textColor;
            Death.GetComponent<RawImage>().color = currentColor;
            Death.GetComponentInChildren<TMP_Text>().color = textColor;

        };
        
        showButtonsTween = (t) =>
        {
            Jump.gameObject.SetActive(true);
            Death.gameObject.SetActive(true);

            Color currentColor = Jump.GetComponent<RawImage>().color;
            Color textColor = Jump.GetComponentInChildren<TMP_Text>().color;

            currentColor = new Vector4(currentColor.r, currentColor.g, currentColor.b, t.CurrentValue);
            textColor = new Vector4(textColor.r, textColor.g, textColor.b, t.CurrentValue);

            Jump.GetComponent<RawImage>().color = currentColor;
            Jump.GetComponentInChildren<TMP_Text>().color = textColor;
            Death.GetComponent<RawImage>().color = currentColor;
            Death.GetComponentInChildren<TMP_Text>().color = textColor;

        };
    }


    // Start is called before the first frame update
    void Start()
    {
        Jump.onClick.AddListener(() =>
        {
            runAnim("Jump");
       
        });
        Death.onClick.AddListener(() =>
        {
            runAnim("Death");
        });

    }

    private void tweenShow(string anim) {
        Animator.SetBool(anim, false);
        Jump.gameObject.Tween("TweenButton", 0.0f, 1.0f, 1.0f, TweenScaleFunctions.CubicEaseIn, showButtonsTween);
    }
    private void runAnim(string anim)
    {
        
        buttonHidden = (t) =>
        {
            Animator.SetBool(anim, true);
            _animStarted = true;
            Jump.gameObject.SetActive(false);
            Death.gameObject.SetActive(false);
        };

        Jump.gameObject.Tween("TweenButton", 1.0f, 0.0f, 1.0f, TweenScaleFunctions.CubicEaseIn, hideButtonsTween, buttonHidden);
    }


    // Update is called once per frame
    void Update()
    {
        if (_animStarted)
        {
            if(Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7)
            {
                Debug.Log("Animation " + Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name + " over");
                tweenShow(Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
                _animStarted = false;
            }
        }
    }
}
