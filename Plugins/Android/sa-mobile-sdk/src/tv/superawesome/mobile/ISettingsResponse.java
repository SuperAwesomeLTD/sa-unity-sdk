package tv.superawesome.mobile;

import java.util.List;

import tv.superawesome.mobile.model.Placement;
import tv.superawesome.mobile.model.Preroll;

public interface ISettingsResponse {
	void receivedConfiguration(List<Placement> placements, List<Preroll> prerolls);
}
