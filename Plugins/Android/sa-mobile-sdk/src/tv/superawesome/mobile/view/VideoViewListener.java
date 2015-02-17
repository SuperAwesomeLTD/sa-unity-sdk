package tv.superawesome.mobile.view;

import java.io.Serializable;

public interface VideoViewListener {
	public void onLoaded();

	public void onPlaybackCompleted();

	public void onAdError();
}
