using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles the spots wher etowers can be built
public class TowerBuilder : MonoBehaviour
{
    private Renderer ren;
    private GameObject buyingPlanePrefab;

    private GameObject buyingPlane;

    bool selected = false;
    private void Start()
    {
        ren = GetComponent<Renderer>();
        buyingPlanePrefab = GameManager.Instance.GetTowerManager().GetBuyingPlanePrefab();
    }


    //visual feedback for players
    private void OnMouseEnter()
    {
        if (!selected && GameManager.Instance.State == GameManager.GameState.Buying && !GameManager.Instance.GetTowerManager().SelectedTowerInUse)
        {
            ren.material.color = Color.red;
            buyingPlane = Instantiate(buyingPlanePrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }

    }

    private void OnMouseOver()
    {
        //open the buying a tower interface
        if (Input.GetMouseButton(0) && !selected && GameManager.Instance.State == GameManager.GameState.Buying && !GameManager.Instance.GetTowerManager().SelectedTowerInUse)
        {
            selected = true;
            ren.material.color = Color.yellow;
            StartCoroutine(GameManager.Instance.GetUIManager().ShowBuyingPanel(true));
            EventBus<SelectTowerBuilderEvent>.Publish(new SelectTowerBuilderEvent(this));
        }

    }

    //visual feedback for players
    private void OnMouseExit()
    {
        if (!selected)
        {
            ren.material.color = Color.white;
        }
        Destroy(buyingPlane);
    }
    
    //unselect a tower builder
    public void Unselect()
    {
        ren.material.color = Color.white;
        selected = false;
    }
}
