using Sandbox;

[Library( "uc_pistolammo", Title = "Small Pistol Ammo Box" )]

[Hammer.EntityTool( "Small Pistol Ammo Box", "Ammo", "Small ammo box for the pistol" )]
[Hammer.EditorModel( "models/pistol_ammobox.vmdl" )]
partial class PistolAmmo : ItemBase
{
	public override Type ItemType => Type.Ammo;
	public virtual string WorldModelPath => "models/pistol_ammobox.vmdl";
	
	public override string PickupSound => "smg_pickup";
	
	public override void Spawn()
	{
		base.Spawn();

		SetModel(WorldModelPath);
	}

	public override void OnCarryStart( Entity carrier )
	{
		base.OnCarryStart( carrier );

		if ( PickupTrigger.IsValid() )
		{
			PickupTrigger.EnableTouch = false;

			var ply = carrier as PlayerBase;
			ply.GiveAmmo( AmmoType.Pistol, 12 );
		}
	}
}

