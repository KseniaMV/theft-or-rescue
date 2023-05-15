using System.Collections;
using UnityEngine;

public sealed class Coroutines : MonoBehaviour
{
    private static Coroutines instance
    {
        get
        {
            if (m_instance == null)
            {
                var go = new GameObject("[Coroutine manager]");
                m_instance = go.AddComponent<Coroutines>();
            }
            return m_instance;
        }
    }
    private static Coroutines m_instance;
    public static Coroutine StartRoutine(IEnumerator enumerator)
    {
        return instance.StartCoroutine(enumerator);
    }
    public static void StopRoutine(Coroutine routine)
    {
        if (routine != null)
            instance.StopCoroutine(routine);
    }
}
