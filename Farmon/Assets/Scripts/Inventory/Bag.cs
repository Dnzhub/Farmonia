using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GrassMan.Manager;

namespace GrassMan.Inventory
{
    public class Bag : MonoBehaviour
    {
        public static event System.Action<int,int> OnDeliver;
        public int currentCrop;
        public int bagCapacity;
        public int totalCrop;

        [SerializeField] GameObject bag;
        [SerializeField] GameObject cropPrefab;
        [SerializeField] Transform deliveryPlace;
             
        private void Start()
        {           
            currentCrop = 0;
            if(bagCapacity == 0)
            {
                bagCapacity = 40;
            }
        }
        public void PutToBag()
        {
            if (!bag.activeInHierarchy)
            {
                bag.SetActive(true);
            }         
            currentCrop++;
            
        }
        public void DeliverToBarn()
        {
            totalCrop += currentCrop;                   
            bag.SetActive(false);
           
            if (currentCrop != 0)
            {              
                GameObject crop = Instantiate(cropPrefab, transform.position, Quaternion.identity);

                crop.transform.DOMove(deliveryPlace.position, 2)
                    .OnComplete(() => Destroy(crop)).OnStepComplete(() => OnDeliver?.Invoke(totalCrop, currentCrop));
                   
                UIManager.Instance.CoinAnimation();               
                currentCrop = 0;
             
            }
        }
    }
}
