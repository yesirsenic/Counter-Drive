using UnityEngine;

public class NoAdPurcahsed : MonoBehaviour
{
    
    void Start()
    {
        if(NoAdsManager.Instance.HasNoAds)
        {
            gameObject.SetActive(false);
        }
    }

    public void BuyNoAdsButton()
    {
        IAPManager.Instance.BuyNoAds();
    }

    
}
