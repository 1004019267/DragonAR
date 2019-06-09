using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
public class Buttons : MonoBehaviour,ITrackableEventHandler
{
    public Animator an;
    public TrackableBehaviour targe;
    Button fly;
    Button yow;

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
               newStatus == TrackableBehaviour.Status.TRACKED ||
               newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
            fly.gameObject.SetActive(true);
            yow.gameObject.SetActive(true);
            targe.enabled = false;
        }
        else
        {
            //OnTrackingLost();
        }
    }
    void OnTrackingLost()
    {
        var childs = targe.transform.GetComponentsInChildren<Transform>();
        foreach (var item in childs)
        {
            item.gameObject.SetActive(false);
        }
    }

    void OnTrackingFound()
    {
        var childs = targe.transform.GetComponentsInChildren<Transform>(true);
        foreach (var item in childs)
        {           
            item.gameObject.SetActive(true);           
        }
    }

    // Use this for initialization
    void Start()
    {
        targe.RegisterTrackableEventHandler(this);
        fly = transform.Find("Button").GetComponent<Button>();
        fly.onClick.AddListener(() =>
        {
            an.Play("Fly");
        });
        yow = transform.Find("Button (1)").GetComponent<Button>();
        yow.onClick.AddListener(() =>
        {
            an.Play("Yowl");
        });      
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);        
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {               
                if (hit.collider.CompareTag("Head"))
                {
                    an.Play("Yowl");
                }
                if (hit.collider.CompareTag("Wing"))
                {
                    an.Play("Fly");
                }
            }
        }
    }
}
