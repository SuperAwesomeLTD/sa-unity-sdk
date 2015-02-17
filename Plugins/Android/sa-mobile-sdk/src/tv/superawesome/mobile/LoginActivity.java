package tv.superawesome.mobile;

import android.app.Activity;
import android.app.ActionBar.LayoutParams;
import android.content.Intent;
import android.net.http.SslError;
import android.os.Bundle;
import android.os.Message;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.webkit.ConsoleMessage;
import android.webkit.SslErrorHandler;
import android.webkit.WebChromeClient;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.FrameLayout;
import android.widget.LinearLayout;

public class LoginActivity extends Activity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_login);
		
		setTitle("Sign in with SuperAwesome");
        
        final FrameLayout layout1 = (FrameLayout) findViewById(R.id.layout1);
        final WebView webview1 = (WebView) findViewById(R.id.webView1);
        
        webview1.setWebViewClient(new WebViewClient(){	        	
        	@Override
        	public void onPageFinished (WebView view, String url){
        		webview1.loadUrl("javascript:if(window.orgOpen == null){window.orgOpen = window.open; window.open = function (url, d1, d2, d3, title, callback){ window.mycb = callback; return window.orgOpen(url, d1, d2, d3); }}");
        		webview1.loadUrl("javascript:login('superawesomegames', 'http://superawesome.club')");
        	}
        }
	);
    
    webview1.setWebChromeClient(new WebChromeClient(){
    	@Override
    	public boolean onCreateWindow (WebView view, boolean isDialog, boolean isUserGesture, Message resultMsg){   
            WebView childView = new WebView(getBaseContext());
            childView.getSettings().setJavaScriptEnabled(true);
            childView.setWebChromeClient(this);
            childView.setWebViewClient(new WebViewClient(){
    	        	@Override
    	        	public void onReceivedSslError(WebView view, SslErrorHandler handler, SslError error) {
    	        	    handler.proceed(); // Ignore SSL certificate errors
    	        	}
    	        }
    		);
            childView.setLayoutParams(new LinearLayout.LayoutParams(LayoutParams.MATCH_PARENT, LayoutParams.MATCH_PARENT));
            layout1.addView(childView);
            WebView.WebViewTransport transport = (WebView.WebViewTransport) resultMsg.obj;
            transport.setWebView(childView);
            resultMsg.sendToTarget();
            return true;
    	}
    	
    	public void onCloseWindow (WebView window){
    		layout1.removeView(window);
    		webview1.loadUrl("javascript:window.mycb();");
    	}
    	
    	public boolean onConsoleMessage(ConsoleMessage cm)  {
            Log.d("ShowMote", cm.message() + " -- From line "+ cm.lineNumber() + " of "+ cm.sourceId() );
            return true;
        }
    });
        
        WebSettings webSettings = webview1.getSettings();
        webSettings.setJavaScriptEnabled(true);
        webview1.getSettings().setSupportMultipleWindows(true);
        webview1.addJavascriptInterface(new LoginWebAppInterface(this), "Android");
        webview1.loadUrl("file:///android_asset/superawesome/login/index.html");
	}
	
	public void handleToken(String token){
    	Intent intent = new Intent();
    	Bundle backpack = new Bundle();
    	backpack.putString("token", token);
    	intent.putExtras(backpack);
    	setResult(RESULT_OK, intent);
        finish();
    }
}
