using UnityEngine;

public abstract class AbstractButton : MonoBehaviour
{
    [Header("Abstract Data")]
    [SerializeField] private GameObject _oldPanel;
    [SerializeField] private GameObject _newPanel;

    public MainManager mainManager;

    private void OnEnable()
    {
        if (mainManager == null)
            mainManager = GameObject.FindGameObjectWithTag("MainManager").GetComponent<MainManager>();
    }
    public virtual void OpenNewPanel()
    {
        mainManager.options.ClickAudio();

        if (_oldPanel != null)
            _oldPanel.SetActive(false);

        if (_newPanel != null)
        {
            _newPanel.transform.SetSiblingIndex(transform.parent.childCount);
            _newPanel.SetActive(true);
        }
    }
    public virtual void OpenNewPanel(GameObject newPanel)
    {
        mainManager.options.ClickAudio();

        if (_oldPanel != null)
            _oldPanel.SetActive(false);

        if (newPanel != null)
        {
            newPanel.transform.SetSiblingIndex(transform.parent.childCount);
            newPanel.SetActive(true);
        }
    }
}
