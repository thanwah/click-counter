using GooglePlayInstant;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public int Count;

    public void DoCountClick()
    {
        Count++;
        Debug.LogFormat("DoCountClick {0}", Count);
        GameObject.Find("Counter").GetComponentInChildren<Text>().text = Count.ToString();
    }

    public void DoInstallClick()
    {
        Debug.Log("DoInstallClick");
#if PLAY_INSTANT
        Debug.LogFormat("Cookie max size: {0}", CookieApi.GetInstantAppCookieMaxSizeBytes());
        var result = CookieApi.SetInstantAppCookie("test-cookie");
        Debug.LogFormat("Set cookie result: {0}", result);

        using (var activity = UnityPlayerHelper.GetCurrentActivity())
        using (var postInstallIntent = InstallLauncher.CreatePostInstallIntent(activity))
        {
            InstallLauncher.PutPostInstallIntentStringExtra(postInstallIntent, "payload", "test-payload-value");
            InstallLauncher.ShowInstallPrompt(activity, 1234, postInstallIntent, "test-referrer");
        }
#else
        Debug.LogErrorFormat("Intent result: \"{0}\"", InstallLauncher.GetPostInstallIntentStringExtra("payload"));
        Debug.LogErrorFormat("Cookie: \"{0}\"", CookieApi.GetInstantAppCookie());
#endif
    }

    public void DoJavaCallback(string message)
    {
        Debug.LogErrorFormat("Received Java message: {0}", message);
    }
}