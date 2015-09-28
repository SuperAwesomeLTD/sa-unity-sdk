using UnityEngine;
using System.Collections;

public class AndroidHttpsExample : MonoBehaviour
{
    private bool mInitialized = false;

    private string mResponse = "";

    void Awake()
    {
        string cert1 = 
@"-----BEGIN CERTIFICATE-----
MIIGPjCCBSagAwIBAgIHBVR6r4lN4jANBgkqhkiG9w0BAQsFADCBjDELMAkGA1UE
BhMCSUwxFjAUBgNVBAoTDVN0YXJ0Q29tIEx0ZC4xKzApBgNVBAsTIlNlY3VyZSBE
aWdpdGFsIENlcnRpZmljYXRlIFNpZ25pbmcxODA2BgNVBAMTL1N0YXJ0Q29tIENs
YXNzIDEgUHJpbWFyeSBJbnRlcm1lZGlhdGUgU2VydmVyIENBMB4XDTE1MDMwNzIz
MzUzMFoXDTE2MDMwODExNDQxOFowTTELMAkGA1UEBhMCTloxGDAWBgNVBAMTD3d3
dy40c2NpZW5jZS5jbzEkMCIGCSqGSIb3DQEJARYVdGVjNHNjaWVuY2VAZ21haWwu
Y29tMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAu+HsXz0RoezRfiyd
pi7w5d+7XX4Dd4UJULHQex/dLWgXKnars6LzCgSrY1yKqpMVpnQCzcBh9pGKc6UB
ehpQOg5i9Md8vDYIURgG456yvTi60lbfI04nQFz1eVJJBiqUsLOkhXn4qJv5RB9X
HF820WrR3zu6aawr1b1Y2iHyaP0D3r602AiQ4P9yFcUcpZeTc7UMqSAaZmb2p0QO
1cl2F4OAe1wYAeV+W90wr4AJeYd7AxGbH3xpODAxg9Y1IqLv8t7b91olX838zZQH
FNmudKZEdUtY4B54gByqT9r8soxnft2aGvWXlWKAuKwy9VOYjmkQSkWXjI5stHci
+LcskQIDAQABo4IC4TCCAt0wCQYDVR0TBAIwADALBgNVHQ8EBAMCA6gwEwYDVR0l
BAwwCgYIKwYBBQUHAwEwHQYDVR0OBBYEFGtVHea+QqwkrZHPb54nyTJi5LsbMB8G
A1UdIwQYMBaAFOtCNNCYsKuf9BtrCPfMZC7vDixFMCcGA1UdEQQgMB6CD3d3dy40
c2NpZW5jZS5jb4ILNHNjaWVuY2UuY28wggFWBgNVHSAEggFNMIIBSTAIBgZngQwB
AgEwggE7BgsrBgEEAYG1NwECAzCCASowLgYIKwYBBQUHAgEWImh0dHA6Ly93d3cu
c3RhcnRzc2wuY29tL3BvbGljeS5wZGYwgfcGCCsGAQUFBwICMIHqMCcWIFN0YXJ0
Q29tIENlcnRpZmljYXRpb24gQXV0aG9yaXR5MAMCAQEagb5UaGlzIGNlcnRpZmlj
YXRlIHdhcyBpc3N1ZWQgYWNjb3JkaW5nIHRvIHRoZSBDbGFzcyAxIFZhbGlkYXRp
b24gcmVxdWlyZW1lbnRzIG9mIHRoZSBTdGFydENvbSBDQSBwb2xpY3ksIHJlbGlh
bmNlIG9ubHkgZm9yIHRoZSBpbnRlbmRlZCBwdXJwb3NlIGluIGNvbXBsaWFuY2Ug
b2YgdGhlIHJlbHlpbmcgcGFydHkgb2JsaWdhdGlvbnMuMDUGA1UdHwQuMCwwKqAo
oCaGJGh0dHA6Ly9jcmwuc3RhcnRzc2wuY29tL2NydDEtY3JsLmNybDCBjgYIKwYB
BQUHAQEEgYEwfzA5BggrBgEFBQcwAYYtaHR0cDovL29jc3Auc3RhcnRzc2wuY29t
L3N1Yi9jbGFzczEvc2VydmVyL2NhMEIGCCsGAQUFBzAChjZodHRwOi8vYWlhLnN0
YXJ0c3NsLmNvbS9jZXJ0cy9zdWIuY2xhc3MxLnNlcnZlci5jYS5jcnQwIwYDVR0S
BBwwGoYYaHR0cDovL3d3dy5zdGFydHNzbC5jb20vMA0GCSqGSIb3DQEBCwUAA4IB
AQCMAORkldm5kdlKRwpFvolq2gMqJwcsvL+bxB+9fub+erXtZ/gtg03wuyYY+OFD
1npEb/fb8zUKJb89hXTlC+e8KysuZIwHWddh9vB4k182MtAI1ru/C4PPSIVxF2ra
k0LEKZcAc/a9FkkdOMQZYA3BZPuGHUOkPx92V3Yf0/aPgXl0tB2JIBSD3RGuhwBS
UIxtJ9RMozIKdiL4SU9wm2fdNowtHWYwCd86n3LrDYT0hixH0c+1xYcVEZLVx0lT
UCrqLWdlIhFfAygh4U0DjCItkcEtWZ0mSyRjdbzLwGgO76uyocOuiMHB5bv0IsA9
TiQhVKnT/a8KxWgGDyzO6xIg
-----END CERTIFICATE-----";
            
        string cert2 = "-----BEGIN CERTIFICATE-----\n" +
            "MIIDdTCCAl0CBFV013wwDQYJKoZIhvcNAQEFBQAwfzELMAkGA1UEBhMCTloxEjAQ\n" +
            "BgNVBAgTCVNvdXRobGFuZDENMAsGA1UEBxMER29yZTENMAsGA1UEChMEbm9uZTEY\n" +
            "MBYGA1UEAxMPbHV6LjRzY2llbmNlLmNvMSQwIgYJKoZIhvcNAQkBFhV0ZWM0c2Np\n" +
            "ZW5jZUBnbWFpbC5jb20wHhcNMTUwNjA3MjM0NTAwWhcNMTYwNjA2MjM0NTAwWjB/\n" +
            "MQswCQYDVQQGEwJOWjESMBAGA1UECBMJU291dGhsYW5kMQ0wCwYDVQQHEwRHb3Jl\n" +
            "MQ0wCwYDVQQKEwRub25lMRgwFgYDVQQDEw9sdXouNHNjaWVuY2UuY28xJDAiBgkq\n" +
            "hkiG9w0BCQEWFXRlYzRzY2llbmNlQGdtYWlsLmNvbTCCASIwDQYJKoZIhvcNAQEB\n" +
            "BQADggEPADCCAQoCggEBALz3DJ/HB3T9/6Vs8JcQQlnZvqUVgy//RE5iySMrDVd7\n" +
            "i7oYsTG3zZ0ATNXAd8hDlL6wimp/8DAWm3S0Dk5pBB71knCkNqaKtIHAHinSmTMd\n" +
            "caYizMfVUIdbe43o/GM58y4dXtCNLMUvIGN5DFDiY/g5uRdmEUcxkhYGLv7WCNqn\n" +
            "zEdLHUrgmjLdud6Ldooomb7R+R1FVEapp9szcYQONNx960clXZKdZcKb6YcTncQN\n" +
            "V3wnpLAo/FfOQduv+iNVFhhE7nF5Eue8AaT53uRtUMBV7vUnVblN7yfWukOKUaDZ\n" +
            "xKoZhloUQpiN8pg3QkaxzInEt9yheUeq7mRLsHDNVycCAwEAATANBgkqhkiG9w0B\n" +
            "AQUFAAOCAQEAQG8FlKJZsC255kRLL54BAjd2fOqE8GAR7eGVt9mr+HdmU4m/gMrS\n" +
            "PTB2ZwMquqrvw+v3/bO1Lj1HyQiYIgfPo5hlLXaZWc1Ao2SlnooK3rO1FI6++/yi\n" +
            "OARjBg2VvSGjYQH93h2cm+ZGqsYL14wJo86HxxkRjGZQU/FhaLKWsabwh/9XPcgl\n" +
            "6z47K9VR76c3MalFEc08ErILZXKwPUNpUdU1IzfpaySMq3RZvPnsZWVU4kWYaCLp\n" +
            "UvxYEu37FRdy6YkTDOXd/PWcbOhdohcTmhJTrzQt6lhb1lJd+mhv5PHoYVPqzpc3\n" +
            "7aXonx43QzkKUgDq+JBtcJlfzaX5NvOLkQ==\n" +
            "-----END CERTIFICATE-----";

        AndroidHttpsHelper.AddCertificate(cert1);
        AndroidHttpsHelper.AddCertificate(cert2);
        mInitialized = true;
    }

	// Use this for initialization
	void Start ()
    {
	    if(mInitialized == false)
        {
            Debug.LogError("Initialization failed. Default WWW class is used.");
        }
        StartCoroutine(Download("https://4science.co/justatest.txt"));
        StartCoroutine(Download("https://luz.4science.co/anothertest.txt"));
        StartCoroutine(Download("https://google.com"));
	}

    IEnumerator Download(string url)
    {
        WWW wwwget = new WWW(url);
        yield return wwwget;

        mResponse += "URL: " + url;
        if (!string.IsNullOrEmpty(wwwget.error))
        {
            mResponse += "\n<color=#FF0000>Error: " + wwwget.error + "</color>";
            mResponse += "\nResponse: " + wwwget.text;
        }else
        {
            string response = wwwget.text;
            if (response.Length > 200)
                response = response.Substring(0, 200);
            mResponse += "\n<color=#00FF00>Connection successfull!</color> ";
            mResponse += "\nResponse: " + response;
        }

        mResponse += "\n\n";
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        if (mInitialized)
        {
            GUILayout.Label("Plugin initialized.");
        }else
        {
            GUILayout.Label("Failed to initialize the plugin.");
        }

        if (mResponse == null)
        {
            GUILayout.Label("No server response.");
        }
        else
        {
            GUILayout.Label(mResponse);
        }
        GUILayout.EndVertical();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
