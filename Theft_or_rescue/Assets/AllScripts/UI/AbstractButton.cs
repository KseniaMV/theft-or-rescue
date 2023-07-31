using UnityEngine;

public abstract class AbstractButton : MonoBehaviour
{
    [Header("Abstract Data")]
    public GameObject[] oldPanels;
    public GameObject[] newPanels;

    public MainManager mainManager;
    private void OnEnable()
    {
        if (mainManager == null)
            mainManager = GameObject.FindGameObjectWithTag("MainManager").GetComponent<MainManager>();
    }
    public virtual void OpenNewPanel(bool changeIndex = true)
    {
        mainManager.options.ClickAudio();

        if (oldPanels != null)
            for (int i = 0; i < oldPanels.Length; i++)
                oldPanels[i].SetActive(false);

        if (newPanels != null)
        {
            for (int i = 0; i < newPanels.Length; i++)
            {
                if(changeIndex)
                    newPanels[i].transform.SetSiblingIndex(transform.parent.childCount);

                newPanels[i].SetActive(true);
            }
        }
    }
    public virtual void OpenNewPanel(GameObject newPanel, bool changeIndex = true)
    {
        mainManager.options.ClickAudio();

        if (oldPanels != null)
            for (int i = 0; i < oldPanels.Length; i++)
                oldPanels[i].SetActive(false);

        if (newPanel != null && changeIndex)
        {
            newPanel.transform.SetSiblingIndex(transform.parent.childCount);
            newPanel.SetActive(true);
        }
    }
}
