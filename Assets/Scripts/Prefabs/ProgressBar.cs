using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{

    [SerializeField] private RectTransform barFillArea = null;
    [SerializeField] private RectTransform bar = null;
    [SerializeField] private float minSize = 60;

    public void SetProgress(float progress)
		{
			// if (gameObject.activeInHierarchy)
			// {
			// 	StartCoroutine(SetNextFrame(progress));
			// }
			// else
			// {
			// 	setOnUpdate = true;
			// 	setProgress = progress;
			// }
		}
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
