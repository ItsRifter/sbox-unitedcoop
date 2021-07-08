using Sandbox;

[Library( "uc_smallmedkit", Title = "Small Medkit Box" )]
[Hammer.EntityTool( "Small Medkit", "Health", "Small medkit box for players" )]
[Hammer.EditorModel( "models/medkit.vmdl" )]
partial class SmallMedkit : ItemBase
{
	public override Type ItemType => Type.Health;
	public virtual string WorldModelPath => "models/medkit.vmdl";
	
	public override string PickupSound => "medkit_pickup";
	
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
			ply.Health += 25;
		}
	}
}

