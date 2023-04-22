using UnityEngine;

public abstract class AbstractButton : MonoBehaviour
{
    [Header("Abstract Data")]
    [SerializeField] private GameObject[] _oldPanels;
    [SerializeField] private GameObject[] _newPanels;

    public MainManager mainManager;

    private void OnEnable()
    {
        if (mainManager == null)
            mainManager = GameObject.FindGameObjectWithTag("MainManager").GetComponent<MainManager>();
    }
    public virtual void OpenNewPanel()
    {
        mainManager.options.ClickAudio();

        if (_oldPanels != null)
            for (int i = 0; i < _oldPanels.Length; i++)
                _oldPanels[i].SetActive(false);


        if (_newPanels != null)
            for (int i = 0; i < _newPanels.Length; i++)
            {
                _newPanels[i].transform.SetSiblingIndex(transform.parent.childCount);
                _newPanels[i].SetActive(true);
            }
    }
    public virtual void OpenNewPanel(GameObject newPanel)
    {
        mainManager.options.ClickAudio();

        if (_oldPanels != null)
            for (int i = 0; i < _oldPanels.Length; i++)
                _oldPanels[i].SetActive(false);

        if (newPanel != null)
        {
            newPanel.transform.SetSiblingIndex(transform.parent.childCount);
            newPanel.SetActive(true);
        }
    }
}
