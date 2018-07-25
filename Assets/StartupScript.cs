using UnityEngine;
using UnityEngine.UI;

public static class StartupScript
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void OnAfterSceneLoadRuntimeMethod()
    {
        Debug.Log("Handling OnAfterSceneLoadRuntimeMethod");
        string appType;
#if PLAY_INSTANT
            appType = "instant";
#else
        appType = "installed";
        GameObject.Find("InstallButton").GetComponentInChildren<Text>().text = "Check intent";
#endif
        GameObject.Find("Version").GetComponentInChildren<Text>().text =
            string.Format("{0} v{1}", appType, Application.version);
    }
}