using Sandbox;
using System;


partial class PlayerBase
{
	public static bool AllowFlashlight { get; set; } = false;
	
	private SuitFlashlight flashlight;
	
	private void EnableFlashlight(bool shouldEnable)
	{
		if ( !AllowFlashlight )
			return;
		
		flashlight.worldLight.SetParent( this.Owner, "head", new Transform(Vector3.Forward * 25) );
		flashlight.worldLight.Enabled = shouldEnable;
	}
}

