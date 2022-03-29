using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryListItem : MonoBehaviour
{
    
		[SerializeField] private Text			nameText				= null;
		[SerializeField] private Image			iconImage				= null;
		[SerializeField] private Image			backgroundImage			= null;
		[SerializeField] private ProgressBar	levelProgressBar		= null;
		[SerializeField] private Text			levelProgressText		= null;

		[Space]

		[SerializeField] private GameObject		progressBarContainer	= null;
		[SerializeField] private GameObject		lockedContainer			= null;
		[SerializeField] private GameObject		coinsUnlockContainer	= null;
		[SerializeField] private GameObject		keysUnlockContainer		= null;
		[SerializeField] private GameObject		iapUnlockContainer		= null;

		[Space]

		[SerializeField] private Text			coinsUnlockAmountText	= null;
		[SerializeField] private Text			keysUnlockAmountText	= null;
		[SerializeField] private Text			iapUnlockPriceText		= null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
