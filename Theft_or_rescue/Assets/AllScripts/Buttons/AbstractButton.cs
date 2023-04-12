using UnityEngine;

public abstract class AbstractButton : MonoBehaviour
{
    [Header("Abstract Data")]
    [SerializeField] private GameObject _oldPanel;
    [SerializeField] private GameObject _newPanel;

    public MainManager mainManager;

    private void Awake()
    {
        if (mainManager == null)
            mainManager = GameObject.FindGameObjectWithTag("DataHolder").GetComponentInChildren<MainManager>();
    }
    public virtual void OpenNewPanel()
    {
        mainManager.options.ClickAudio();

        if(_oldPanel != null)
            _oldPanel.SetActive(false);

        if(_newPanel != null)
            _newPanel.SetActive(true);
    }
}
