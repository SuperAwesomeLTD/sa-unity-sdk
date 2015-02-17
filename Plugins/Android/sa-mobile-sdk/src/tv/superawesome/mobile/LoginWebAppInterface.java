package tv.superawesome.mobile;

import android.content.Context;
import android.webkit.JavascriptInterface;
import android.widget.Toast;

public class LoginWebAppInterface {
    Context mContext;

    /** Instantiate the interface and set the context */
    LoginWebAppInterface(Context c) {
        mContext = c;
    }

    /** Show a toast from the web page */
    @JavascriptInterface
    public void showToast(String toast) {
        Toast.makeText(mContext, toast, Toast.LENGTH_SHORT).show();
        
    }
    
    @JavascriptInterface
    public void receivedToken(String token){
    	LoginActivity activity = (LoginActivity)mContext;
    	activity.handleToken(token);
    }
}