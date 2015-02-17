package tv.superawesome.mobile;

import java.util.Random;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.text.Editable;
import android.text.InputType;
import android.widget.EditText;

public class ParentalGate {
	
	public interface ParentalGateViewCallback{
		void onCorrectAnswer();
	}
	
	private ParentalGateViewCallback viewCallback;

	public void setViewCallback(ParentalGateViewCallback viewCallback) {
		this.viewCallback = viewCallback;
	}

	public ParentalGate(final Context context){
		Random r = new Random();
		int i1 = r.nextInt(80)+20;
		int i2 = r.nextInt(80)+20;
		final int sum = i1 + i2;
		
		AlertDialog.Builder alert = new AlertDialog.Builder(context);

		alert.setTitle("Parental Gate");
		alert.setMessage("Solve the following problem to continue: "+i1+" + "+i2+" = ?");

		// Set an EditText view to get user input 
		final EditText input = new EditText(context);
		input.setInputType(InputType.TYPE_CLASS_NUMBER);
		alert.setView(input);

		alert.setPositiveButton("Continue", new DialogInterface.OnClickListener() {
		public void onClick(DialogInterface dialog, int whichButton) {
			Editable value = input.getText();
			// Do something with value!
			try{
				int answer = Integer.parseInt(value.toString());
				if(answer == sum){
					if(viewCallback != null){
						viewCallback.onCorrectAnswer();
					}
				}else{
					AlertDialog.Builder alert2 = new AlertDialog.Builder(context);
					alert2.setTitle("Sorry, that was the wrong answer.");
					alert2.show();
				}
			}catch(NumberFormatException ex){
				
			}
		  }
		});

		alert.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
		  public void onClick(DialogInterface dialog, int whichButton) {
		    // Canceled.
		  }
		});

		alert.show();
	}

}
