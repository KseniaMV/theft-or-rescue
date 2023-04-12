using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public int NumberSelectedLanguage { get; private set; }
    public int NumberSelectedAvatar { get; private set; }

    public Options options;

    private void Awake()
    {
        if (!transform.parent.gameObject.activeSelf)
            transform.parent.gameObject.SetActive(true);

        if (options == null)
            options = GetComponent<Options>();
    }
    public void SelectNumberLanguage(int number)
    {
        NumberSelectedLanguage = number;
        //save
        //change languge
    }
    public void SelectNumberAvatar(int number)
    {
        NumberSelectedAvatar = number;
        //save
        //change avatars in other panels
    }
}
