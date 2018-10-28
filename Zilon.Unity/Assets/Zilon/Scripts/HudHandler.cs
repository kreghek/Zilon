using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HudHandler : MonoBehaviour
{
    public void CombatButtonClickHandler()
    {
        SceneManager.LoadScene("Combat");
    }
}
