using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOffers : Interactable
{

    [SerializeField] GameObject hijo;
    new BoxCollider2D collider;
    int days = 0;

    public override void Interact(Character character)
    {
        ShopOffersCharacter shop = character.GetComponent<ShopOffersCharacter>();
        if (shop == null) { return; }
        shop.begin();
    }

    private void Start()
    {
        if ((DayTimeController.days + 1) % 5 == 0)
        {
            if (DayTimeController.days > days)
            {
                days = DayTimeController.days;
                StartCoroutine(HideShop());
                Invoke(nameof(EncenderRender), 0.2f);
            }
        }
        else
        {
            Invoke(nameof(ApagarRender),0.1f);
        }
    }

    private void OnEnable()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();
    }

    void EncenderRender()
    {
        hijo.SetActive(true);
        collider.enabled = true;
    }

    void ApagarRender()
    {
        hijo.SetActive(false);
        collider.enabled = false;
    }

    IEnumerator HideShop()
    {
        yield return new WaitForSeconds(60);
        gameObject.SetActive(false);
    }

}
