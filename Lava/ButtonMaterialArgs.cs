using System;

namespace Lava
{
	public class ButtonMaterialArgs
	{
		public ButtonMaterialArgs(int index, bool state)
		{
			buttonIndex = index;
			buttonState = state;
		}
		public int buttonIndex { get; private set; }
		public bool buttonState { get; private set; }
	}
}
