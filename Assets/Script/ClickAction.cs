using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAction : MonoBehaviour
{
    private void OnMouseUp()
    {
        Debug.Log("On mouse up" + gameObject.name);
        GameManager.Instance.CatMoveAction(this.gameObject);
    }
}
